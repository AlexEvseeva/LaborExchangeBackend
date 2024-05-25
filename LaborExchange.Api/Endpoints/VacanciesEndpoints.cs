using LaborExchange.Api.Dto.Vacancies;
using LaborExchange.Api.Endpoints;
using LaborExchange.Api.Mapping;

namespace LaborExchange.Api;

public static class VacanciesEndpoints
{
    const string VACANIES_ENDPOINT_NAME = "getVacancies";
    private static List<VacancyDto> vacancies = [
        new (
            1,
            "Dark side",
            ProfessionsEndpoints.professions[5],
            "Цілодобово",
            10000,
            "Можливо ночувати в офісі під столом",
            "Здорова система травлення"
        ),
        new (
            2,
            "Light Side",
            ProfessionsEndpoints.professions[4],
            null,
            0,
            null,
            "Мати власту лопату"
        )
    ];
    public static RouteGroupBuilder MapVacanciesEndpoints(this WebApplication app){
        var group = app.MapGroup("vacancies").WithParameterValidation();
        group.MapGet("/", () => vacancies);

        group.MapGet("/{id}", (int id) => 
            {
                var vacancy = vacancies.Find(vacancy => vacancy.Id == id);
                return vacancy is null ? Results.NotFound() : Results.Ok(vacancy);
            }
        ).WithName(VACANIES_ENDPOINT_NAME);

        group.MapPost("/", (CreateVacancyDto newVacancy) => {
            var index = vacancies.Max(vacancy => vacancy.Id) + 1;
            var profession = ProfessionsEndpoints.professions.Find( prof => prof.Id == newVacancy.ProfessionId);
            
            if(profession != null){
                var vacancy = newVacancy.ToDto(index, profession);
                vacancies.Add(vacancy);

                return Results.CreatedAtRoute(VACANIES_ENDPOINT_NAME, new {id = vacancy.Id}, vacancy);
            } else {
                return Results.NotFound();
            }
            
        });

        group.MapPut("/{id}", (int id, UpdateVacancyDto updatedVacancy) => {
            var index = vacancies.FindIndex(vacancy => vacancy.Id == id);
            var profession = ProfessionsEndpoints.professions.Find( prof => prof.Id == updatedVacancy.ProfessionId);
            var vacancy = vacancies[index];
            if(vacancy == null || profession == null) {
                return Results.NotFound();
            }
            else {
                vacancies[index] = updatedVacancy.ToDto(profession); 
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id}", (int id) => {
            var vacancy = vacancies.Find(vacancy => vacancy.Id == id);
            if(vacancy != null){
                vacancies.Remove(vacancy);
            }
            return Results.NoContent();
        });

        return group;
    }   


}
