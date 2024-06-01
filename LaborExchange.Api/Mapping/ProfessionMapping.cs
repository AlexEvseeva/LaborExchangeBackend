using LaborExchange.Api.Dto.Profession;

namespace LaborExchange.Api.Mapping;

public static class ProfessionMapping
{
    public static ProfessionDto toDto(this CreateProfessionDto newProfession, int id) => 
    new ProfessionDto(
        id,
        newProfession.Name
    );
}
