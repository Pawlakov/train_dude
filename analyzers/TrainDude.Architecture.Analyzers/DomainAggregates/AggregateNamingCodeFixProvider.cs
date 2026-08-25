// <copyright file="AggregateNamingCodeFixProvider.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Analyzers.DomainAggregates;

using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Simplification;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AggregateNamingCodeFixProvider))]
[Shared]
public class AggregateNamingCodeFixProvider
    : CodeFixProvider
{
    private const string Title = "Inherit from BaseAggregate";

    public override ImmutableArray<string> FixableDiagnosticIds => [AggregateNamingAnalyzer.DiagnosticId];

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document
            .GetSyntaxRootAsync(context.CancellationToken)
            .ConfigureAwait(false);

        if (root is null)
        {
            return;
        }

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var classDeclaration = root
            .FindToken(diagnosticSpan.Start).Parent?
            .AncestorsAndSelf()
            .OfType<ClassDeclarationSyntax>()
            .FirstOrDefault();

        if (classDeclaration is null)
        {
            return;
        }

        context.RegisterCodeFix(CodeAction.Create(Title, ct => AddBaseAggregateAsync(context.Document, classDeclaration, ct), Title), diagnostic);
    }

    private static async Task<Document> AddBaseAggregateAsync(Document document, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken)
    {
        var editor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);
        var semanticModel = editor.SemanticModel;
        var generator = editor.Generator;

        var baseAggregateSymbol = semanticModel.Compilation
            .GetSymbolsWithName(name => name == "BaseAggregate", SymbolFilter.Type, cancellationToken)
            .OfType<INamedTypeSymbol>()
            .FirstOrDefault();

        SyntaxNode baseTypeNode = baseAggregateSymbol is not null
            ? generator.TypeExpression(baseAggregateSymbol).WithAdditionalAnnotations(Simplifier.Annotation)
            : generator.IdentifierName("BaseAggregate");

        var newClassDeclaration = generator.AddBaseType(classDeclaration, baseTypeNode);
        editor.ReplaceNode(classDeclaration, newClassDeclaration);

        var newDocument = editor.GetChangedDocument();

        if (baseAggregateSymbol is not null)
        {
            newDocument = await ImportAdder.AddImportsAsync(newDocument, Simplifier.Annotation, cancellationToken: cancellationToken).ConfigureAwait(false);

            newDocument = await Simplifier.ReduceAsync(newDocument, Simplifier.Annotation, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        return newDocument;
    }
}