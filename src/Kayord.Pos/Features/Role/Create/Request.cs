using FluentValidation;

namespace Kayord.Pos.Features.Role.Create;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int OutletId { get; set; }
    public int RoleTypeId { get; set; }

}

