using LaborExchange.Api.Dto.Profession;
using LaborExchange.Api.Dto.Vacancies;

namespace LaborExchange.Api.Mapping;

public static class VacancyMapping
{
    public static VacancyDto ToDto(this CreateVacancyDto newVacancy, int id, ProfessionDto profession) =>
    new VacancyDto(
        id,
        newVacancy.FirmName,
        profession,
        newVacancy.WorkingCondition,
        newVacancy.Payment,
        newVacancy.LivingCondition,
        newVacancy.Requirements,
        newVacancy.IsArchived
    );

    public static VacancyDto ToDto(this UpdateVacancyDto newVacancy, ProfessionDto profession) =>
    new VacancyDto(
        newVacancy.Id,
        newVacancy.FirmName,
        profession,
        newVacancy.WorkingCondition,
        newVacancy.Payment,
        newVacancy.LivingCondition,
        newVacancy.Requirements,
        newVacancy.IsArchived
    );
}
