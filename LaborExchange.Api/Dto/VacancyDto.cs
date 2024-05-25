namespace LaborExchange.Api.Dto;

public record class VacancyDto (
    int Id,
    string FirmName,
    string Position,
    string? WorkingCondition,
    decimal Payment,
    string? LivingCondition,
    string Requirements
);