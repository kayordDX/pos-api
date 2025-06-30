using FluentValidation;

namespace Kayord.Pos.Features.Role.Update
{
    public class Request
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OutletId { get; set; }
        public int RoleTypeId { get; set; }

    }


}