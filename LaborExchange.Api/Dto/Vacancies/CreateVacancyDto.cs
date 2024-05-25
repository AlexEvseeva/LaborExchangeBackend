using System.ComponentModel.DataAnnotations;

namespace LaborExchange.Api.Dto.Vacancies;

public record class CreateVacancyDto
(
    [Required] string FirmName,
    [Required] string Position,
    string WorkingCondition,
    [Range(0, 120_000_000)] decimal Payment,
    string LivingCondition,
    [Required] string Requirements
);
