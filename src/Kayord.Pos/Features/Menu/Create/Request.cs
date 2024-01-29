using FluentValidation;
using Kayord.Pos.Common.Enums;

namespace Kayord.Pos.Features.Menu.Create
{
    public class Request
    {
        public int OutletId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Division Division { get; set; } 
        public int MenuSectionId { get; set; } 
    }

}
