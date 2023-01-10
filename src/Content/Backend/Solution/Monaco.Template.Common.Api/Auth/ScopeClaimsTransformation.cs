﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Monaco.Template.Common.Api.Auth;

/// <summary>
/// Transforms the single claim of type "scope" coming from KeyCloak that contains the multiple scopes selected and splits it into multiple individual scope claims.
/// </summary>
public class ScopeClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst(AuthExtensions.ScopeClaimType);
        if (principal.Identity is ClaimsIdentity identity && (claim?.Value.Contains(' ') ?? false))
        {
            var scopes = claim.Value.Split(' ');
            identity.AddClaims(scopes.Select(s => new Claim(AuthExtensions.ScopeClaimType, s, claim.ValueType, claim.Issuer)));
            identity.RemoveClaim(claim);
        }

        return Task.FromResult(principal);
    }
}