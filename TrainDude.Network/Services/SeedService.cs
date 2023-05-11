// <copyright file="SeedService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using TrainDude.Network.Models.Seed;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

internal class SeedService
{
    private readonly IDeserializer deserializer;

    public SeedService()
    {
        this.deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
    }

    public async Task<IList<StationSeed>> GetStationsSeed()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fileName = assembly.GetManifestResourceNames().Where(x => x.EndsWith("stations_seed.yml")).Single();

        using (var stream = assembly.GetManifestResourceStream(fileName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException("Embedded resource not found.", fileName);
            }

            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                var list = this.deserializer.Deserialize<List<StationSeed>>(result);
                return await Task.FromResult(list);
            }
        }
    }

    public async Task<IList<RouteSeed>> GetRoutesSeed()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fileName = assembly.GetManifestResourceNames().Where(x => x.EndsWith("routes_seed.yml")).Single();

        using (var stream = assembly.GetManifestResourceStream(fileName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException("Embedded resource not found.", fileName);
            }

            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                var list = this.deserializer.Deserialize<List<RouteSeed>>(result);
                return await Task.FromResult(list);
            }
        }
    }
}