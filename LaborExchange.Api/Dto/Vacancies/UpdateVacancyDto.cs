using System.ComponentModel.DataAnnotations;

namespace LaborExchange.Api.Dto.Vacancies;

public record class UpdateVacancyDto
(
    [Required] int Id,
    [Required] string FirmName,
    [Required] int ProfessionId,
    string WorkingCondition,
    [Range(0, 120_000_000)] decimal Payment,
    string LivingCondition,
    [Required] string Requirements,
    [Required] string Contacts,
    bool IsArchived
);
