using NetMastery.InventoryManager.Bl.DtoEntities;
using System.Collections.Generic;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IPersonService : IBusinessService<PersonDto>
    {
        IEnumerable<PersonDto> GetAll(int subdivisionId);
    }
}
