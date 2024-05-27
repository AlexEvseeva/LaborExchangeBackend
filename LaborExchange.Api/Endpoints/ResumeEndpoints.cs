using LaborExchange.Api.Dto.Profession;
using LaborExchange.Api.Dto.Resume;
using LaborExchange.Api.Endpoints;
using LaborExchange.Api.Mapping;
using System.Text.Json;

namespace LaborExchange.Api;

public static class ResumeEndpoints
{
    const string VACANIES_ENDPOINT_NAME = "getResume";
    const string fileName = "ResumeSaveFile.txt";

    static ResumeEndpoints(){
        LoadFromFile();
    }
    private static List<ResumeDto> resume = new();
    
    public static RouteGroupBuilder MapResumeEndpoints(this WebApplication app){
        var group = app.MapGroup("Resume").WithParameterValidation();
        group.MapGet("/", () => resume);

        group.MapGet("/{id}", (int id) => 
            {
                var Resume = resume.Find(Resume => Resume.Id == id);
                return Resume is null ? Results.NotFound() : Results.Ok(Resume);
            }
        ).WithName(VACANIES_ENDPOINT_NAME);

        group.MapPost("/", (CreateResumeDto newResume) => {
            var index = resume.Max(Resume => Resume.Id) + 1;
            var profession = ProfessionsEndpoints.professions.Find( prof => prof.Id == newResume.ProfessionId);
            if(profession != null){
                var Resume = newResume.ToDto(index, profession);
                resume.Add(Resume);    
                SaveToFile();
                return Results.CreatedAtRoute(VACANIES_ENDPOINT_NAME, new {id = Resume.Id}, Resume);
            } else {
                return Results.NotFound();
            }

            
        });

        group.MapPut("/{id}", (int id, UpdateResumeDto updatedResume) => {
            var index = resume.FindIndex(Resume => Resume.Id == id);
            var profession = ProfessionsEndpoints.professions.Find( prof => prof.Id == updatedResume.ProfessionId);
            var Resume = resume[index];
            if(Resume == null || profession == null){
                return Results.NotFound();
            } else {
                resume[index] = updatedResume.ToDto(profession); 
                SaveToFile();
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id}", (int id) => {
            var Resume = resume.Find(Resume => Resume.Id == id);
            if(Resume != null){
                resume.Remove(Resume);
                SaveToFile();
            }
            return Results.NoContent();
        });

        return group;
    }   

    static void SaveToFile() {
            string jsonString = JsonSerializer.Serialize(resume);
            File.WriteAllText(fileName, jsonString);
    }

    static void LoadFromFile(){
        var jsonString = File.ReadAllText(fileName);
        resume = JsonSerializer.Deserialize<List<ResumeDto>>(jsonString)  ?? [];
    }
}
