using LaborExchange.Api.Dto.Profession;

namespace LaborExchange.Api.Dto.Vacancies;

public record class VacancyDto(
    int Id,
    string FirmName,
    ProfessionDto Position,
    string? WorkingCondition,
    decimal Payment,
    string? LivingCondition,
    string Requirements,
    string Contacts,
    bool IsArchived
);