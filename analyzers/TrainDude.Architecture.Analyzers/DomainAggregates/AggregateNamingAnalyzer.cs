namespace TrainDude.Architecture.Analyzers.DomainAggregates;

using System;
using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class AggregateNamingAnalyzer
    : DiagnosticAnalyzer
{
    internal const string DiagnosticId = "TD001";
    private static readonly LocalizableString Title = "Class with 'Aggregate' suffix must inherit from BaseAggregate";
    private static readonly LocalizableString MessageFormat = "Class '{0}' has the 'Aggregate' suffix but does not inherit from 'BaseAggregate'";
    private const string Category = "Design";
    private static readonly LocalizableString Description = "Classes named with the 'Aggregate' suffix are expected to derive from BaseAggregate to ensure consistent aggregate root behavior.";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true, Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
    {
        var declaration = (ClassDeclarationSyntax)context.Node;
        var name = declaration.Identifier.Text;

        if (!name.EndsWith("Aggregate", StringComparison.Ordinal))
        {
            return;
        }

        if (name.Equals("BaseAggregate", StringComparison.Ordinal))
        {
            return;
        }

        var symbol = context.SemanticModel.GetDeclaredSymbol(declaration);
        if (symbol is null)
        {
            return;
        }

        if (!InheritsFromBaseAggregate(symbol))
        {
            var diagnostic = Diagnostic.Create(Rule, declaration.Identifier.GetLocation(), name);

            context.ReportDiagnostic(diagnostic);
        }
    }

    private static bool InheritsFromBaseAggregate(INamedTypeSymbol symbol)
    {
        var baseType = symbol.BaseType;
        while (baseType is not null)
        {
            if (baseType.Name == "BaseAggregate")
            {
                return true;
            }

            baseType = baseType.BaseType;
        }

        return false;
    }
}