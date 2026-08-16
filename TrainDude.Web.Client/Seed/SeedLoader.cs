// <copyright file="SeedLoader.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Seed;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class SeedLoader
{
    private readonly HttpClient http;
    private readonly IDeserializer deserializer;

    public SeedLoader(HttpClient http)
    {
        this.http = http;
        this.deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
    }

    public async Task<IList<T>> LoadAsync<T>(string resourceNameSuffix, CancellationToken cancellationToken = default)
    {
        var fileName = $"seed/{resourceNameSuffix}";
        var response = await this.http.GetAsync(fileName, cancellationToken);

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        if (stream == null)
        {
            throw new FileNotFoundException("Static resource not found.", fileName);
        }

        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync(cancellationToken);
        return this.deserializer.Deserialize<List<T>>(result);
    }
}