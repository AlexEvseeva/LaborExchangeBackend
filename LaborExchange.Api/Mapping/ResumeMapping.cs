using LaborExchange.Api.Dto.Profession;
using LaborExchange.Api.Dto.Resume;

namespace LaborExchange.Api.Mapping;

public static class ResumeMapping
{
    public static ResumeDto ToDto(this CreateResumeDto newResume, int id, ProfessionDto profession) =>
    new ResumeDto(
        id,
        newResume.FirstName,
        newResume.LastName,
        profession,
        newResume.Education,
        newResume.LastPositionFirm,
        newResume.LastPositionName,
        newResume.ReasonOfDismissal,
        newResume.MartialStatus,
        newResume.LivingCondition,
        newResume.Email,
        newResume.Phone,
        newResume.Requirements
    );

    public static ResumeDto ToDto(this UpdateResumeDto updateResume, ProfessionDto profession) =>
    new ResumeDto(
        updateResume.Id,
        updateResume.FirstName,
        updateResume.LastName,
        profession,
        updateResume.Education,
        updateResume.LastPositionFirm,
        updateResume.LastPositionName,
        updateResume.ReasonOfDismissal,
        updateResume.MartialStatus,
        updateResume.LivingCondition,
        updateResume.Email,
        updateResume.Phone,
        updateResume.Requirements
    ); 
}
