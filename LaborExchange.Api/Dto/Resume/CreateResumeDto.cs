using System.ComponentModel.DataAnnotations;

namespace LaborExchange.Api.Dto.Resume;

public record class CreateResumeDto
(
    [Required] string FirstName,
    [Required] string LastName,
    string? Profession,
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
