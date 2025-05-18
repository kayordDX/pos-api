using Microsoft.AspNetCore.Authorization;

namespace Kayord.Pos.Features.Auth;

public class RoleTypeRequirement : IAuthorizationRequirement
{
    public RoleTypeRequirement(string roleType) => RoleType = roleType;
    public string RoleType { get; set; } = string.Empty;

}