using FluentValidation;
namespace Kayord.Pos.Features.Role.Create
{
    public class Request
    {
     public string Name { get; set; }
     public string Description { get; set; }
    }
}