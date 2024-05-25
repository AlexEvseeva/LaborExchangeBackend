using LaborExchange.Api.Dto.Profession;
using LaborExchange.Api.Dto.Resume;
using LaborExchange.Api.Endpoints;
using LaborExchange.Api.Mapping;

namespace LaborExchange.Api;

public static class ResumeEndpoints
{
    const string VACANIES_ENDPOINT_NAME = "getResume";
    private static List<ResumeDto> resume = [
        new (
            1,
            "Джон",
            "Доу",
            ProfessionsEndpoints.professions[1],
            "ХНУРЕ, програмна інженерія",
            null,
            null,
            null,
            "Одружений",
            "В наявності",
            "john.Dow@gmail.com",
            "+30675554433",
            "Full stack, backend"
        ),
        new (
            2,
            "Джейн",
            "Доу",
            ProfessionsEndpoints.professions[3],
            "ХНУРЕ, програмна інженерія",
            "Dell",
            "Android developer",
            "Сварилася із чужими дітьми",
            "Одружений",
            "В наявності",
            "Jane.Dow@gmail.com",
            "+30676664422",
            "Full stack, backend"
        )
    ];
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
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id}", (int id) => {
            var Resume = resume.Find(Resume => Resume.Id == id);
            if(Resume != null){
                resume.Remove(Resume);
            }
            return Results.NoContent();
        });

        return group;
    }   


}
