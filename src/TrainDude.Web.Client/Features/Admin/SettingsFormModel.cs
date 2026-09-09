// <copyright file="SettingsFormModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin;

using TrainDude.Features.Shared.Contracts.Enums;

public class SettingsFormModel
{
    public NamingPolicy Policy { get; set; }
}