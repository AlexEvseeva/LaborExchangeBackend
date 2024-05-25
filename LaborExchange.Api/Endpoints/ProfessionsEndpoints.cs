using LaborExchange.Api.Dto.Profession;

namespace LaborExchange.Api.Endpoints;

public static class ProfessionsEndpoints
{
    public static List<ProfessionDto> professions = [
        new(1,"Інженер"),
        new(2,"Дизайнер"),
        new (3,"Розробник програмного забезпечення"),
        new(4,"Менеджер"),
        new(5,"Директор"),
        new(6,"Дегустатор печива"),
    ];

    public static RouteGroupBuilder MapProfessionsEndpoints(this WebApplication app){
        var group = app.MapGroup("professions").WithParameterValidation();
        
        group.MapGet("/", () => professions);

        return group;
    }
}
