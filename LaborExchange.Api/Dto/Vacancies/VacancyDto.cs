namespace LaborExchange.Api.Dto.Vacancies;

public record class VacancyDto(
    int Id,
    string FirmName,
    string Position,
    string? WorkingCondition,
    decimal Payment,
    string? LivingCondition,
    string Requirements
);