// <copyright file="SeedLoader.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Seed;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

internal static class SeedLoader
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

    public static IList<T> Load<T>(string resourceNameSuffix)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fileName = assembly.GetManifestResourceNames()
            .Single(x => x.EndsWith(resourceNameSuffix));

        using var stream = assembly.GetManifestResourceStream(fileName);
        if (stream == null)
        {
            throw new FileNotFoundException("Embedded resource not found.", fileName);
        }

        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        return Deserializer.Deserialize<List<T>>(result);
    }
}
