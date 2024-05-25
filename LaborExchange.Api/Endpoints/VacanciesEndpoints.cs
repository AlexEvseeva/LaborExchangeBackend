using LaborExchange.Api.Dto.Vacancies;

namespace LaborExchange.Api;

public static class VacanciesEndpoints
{
    const string VACANIES_ENDPOINT_NAME = "getVacancies";
    private static List<VacancyDto> vacancies = [
        new (
            1,
            "Dark side",
            "Дегустатор печива",
            "Цілодобово",
            10000,
            "Можливо ночувати в офісі під столом",
            "Здорова система травлення"
        ),
        new (
            2,
            "Light Side",
            "Молодший помічник молодшого двірник",
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
            var vacancy = newVacancy.ToDto(index);
            vacancies.Add(vacancy);

            return Results.CreatedAtRoute(VACANIES_ENDPOINT_NAME, new {id = vacancy.Id}, vacancy);
        });

        group.MapPut("/{id}", (int id, UpdateVacancyDto updatedVacancy) => {
            var index = vacancies.FindIndex(vacancy => vacancy.Id == id);
            var vacancy = vacancies[index];
            if(vacancy == null){
                return Results.NotFound();
            }
            else {
                vacancies[index] = updatedVacancy.ToDto(); 
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
