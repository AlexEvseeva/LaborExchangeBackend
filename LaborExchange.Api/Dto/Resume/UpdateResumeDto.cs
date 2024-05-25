using System.ComponentModel.DataAnnotations;

namespace LaborExchange.Api.Dto.Resume;

public record class UpdateResumeDto
(
    [Required] int Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] int ProfessionId,
    string? Education,
    string? LastPositionFirm,
    string? LastPositionName,
    string? ReasonOfDismissal,
    string? MartialStatus,
    string? LivingCondition,
    string? Email,
    string? Phone,
    string? Requirements
);
