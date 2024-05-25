using LaborExchange.Api.Dto.Vacancies;

namespace LaborExchange.Api;

public static class VacancyMapping
{
    public static VacancyDto ToDto(this CreateVacancyDto newVacancy, int id) =>
    new VacancyDto(
        id,
        newVacancy.FirmName,
        newVacancy.Position,
        newVacancy.WorkingCondition,
        newVacancy.Payment,
        newVacancy.LivingCondition,
        newVacancy.Requirements
    );
    
    public static VacancyDto ToDto(this UpdateVacancyDto newVacancy) =>
    new VacancyDto(
        newVacancy.Id,
        newVacancy.FirmName,
        newVacancy.Position,
        newVacancy.WorkingCondition,
        newVacancy.Payment,
        newVacancy.LivingCondition,
        newVacancy.Requirements
    );
}
