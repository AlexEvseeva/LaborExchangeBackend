using LaborExchange.Api.Dto.Profession;
using LaborExchange.Api.Mapping;
using System.Text.Json;

namespace LaborExchange.Api.Endpoints;

public static class ProfessionsEndpoints
{
    const string fileName = "ProfessionsSaveFile.txt";
    public static List<ProfessionDto> professions = [
        new(1,"Інженер"),
        new(2,"Дизайнер"),
        new (3,"Розробник програмного забезпечення"),
        new(4,"Менеджер"),
        new(5,"Директор"),
        new(6,"Дегустатор печива"),
    ];
    static ProfessionsEndpoints(){
        LoadFromFile();
    }

    public static RouteGroupBuilder MapProfessionsEndpoints(this WebApplication app){
        var group = app.MapGroup("professions").WithParameterValidation();
        
        group.MapGet("/", () => professions);

        group.MapPost("/", (CreateProfessionDto newProf) => {
            foreach (var prof in professions)
            {
                if(prof.Name == newProf.Name){
                    return Results.BadRequest();
                }
            }
            professions.Add(newProf.toDto(professions.Count() + 1));
            SaveToFile();
            return Results.NoContent();
        });

        return group;
    }

    static void SaveToFile() {
            string jsonString = JsonSerializer.Serialize(professions);
            File.WriteAllText(fileName, jsonString);
    }

    static void LoadFromFile(){
        var jsonString = File.ReadAllText(fileName);
        professions = JsonSerializer.Deserialize<List<ProfessionDto>>(jsonString)  ?? [];
    }
}
