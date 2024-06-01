using LaborExchange.Api.Dto.Profession;

namespace LaborExchange.Api.Dto.Resume;

public record class ResumeDto
(
    int Id,
    string FirstName,
    string LastName,
    ProfessionDto Profession,
    string? Education,
    string? LastPositionFirm,
    string? LastPositionName,
    string? ReasonOfDismissal,
    string? MartialStatus,
    string? LivingCondition,
    string? Email,
    string? Phone,
    string? Requirements,
    DateOnly DateOfBirth
);