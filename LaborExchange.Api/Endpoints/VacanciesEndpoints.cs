using LaborExchange.Api.Dto.Vacancies;
using LaborExchange.Api.Endpoints;
using LaborExchange.Api.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LaborExchange.Api;

public static class VacanciesEndpoints
{
    const string fileName = "VacanciesSaveFile.txt";
    static VacanciesEndpoints(){
        LoadFromFile();
    }
    
    const string VACANIES_ENDPOINT_NAME = "getVacancies";
    private static List<VacancyDto> vacancies = new();

    public static RouteGroupBuilder MapVacanciesEndpoints(this WebApplication app){
        var group = app.MapGroup("vacancies").WithParameterValidation();

        group.MapGet("/", () => vacancies);

        group.MapGet("/search", (HttpRequest request) => 
        {
            if(!request.Query.ContainsKey("query") || string.IsNullOrEmpty(request.Query["query"])){
                return new List<VacancyDto>();
            }
            var query = request.Query["query"][0];

            if(query != "")
            {
                List<VacancyDto> filtered = new();
                ArgumentNullException.ThrowIfNullOrEmpty(query);
                foreach (var vacancy in vacancies)
                {
                    if (vacancy.FirmName.Contains(query, StringComparison.OrdinalIgnoreCase) 
                        || vacancy.Position.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                        || vacancy.WorkingCondition?.Contains(query, StringComparison.OrdinalIgnoreCase) == true
                        || vacancy.LivingCondition?.Contains(query, StringComparison.OrdinalIgnoreCase) == true
                        || vacancy.Requirements.Contains(query, StringComparison.OrdinalIgnoreCase)    
                    )
                    {
                        filtered.Add(vacancy);
                    }
                }    
                return filtered; 
            } else
            {
                return [];
            }
            
        });

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

                SaveToFile();
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
                SaveToFile();
                return Results.NoContent();
            }
        });

        group.MapDelete("/{id}", (int id) => {
            var vacancy = vacancies.Find(vacancy => vacancy.Id == id);
            if(vacancy != null){
                vacancies.Remove(vacancy);
                SaveToFile();
            }
            return Results.NoContent();
        });

        return group;
    }   

    static void SaveToFile() {
            string jsonString = JsonSerializer.Serialize(vacancies);
            File.WriteAllText(fileName, jsonString);
    }

    static void LoadFromFile(){
        var jsonString = File.ReadAllText(fileName);
        vacancies = JsonSerializer.Deserialize<List<VacancyDto>>(jsonString)  ?? [];
    }
}
