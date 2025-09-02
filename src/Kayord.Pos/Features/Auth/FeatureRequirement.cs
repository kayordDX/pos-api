using Microsoft.AspNetCore.Authorization;

namespace Kayord.Pos.Features.Auth;

public class FeatureRequirement : IAuthorizationRequirement
{
    public FeatureRequirement(string feature) => Feature = feature;
    public string Feature { get; set; } = string.Empty;

}