// <copyright file="ClaimsPrincipalExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Extensions;

using System;
using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string GetSubject(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("Missing subject claim.");
    }
}