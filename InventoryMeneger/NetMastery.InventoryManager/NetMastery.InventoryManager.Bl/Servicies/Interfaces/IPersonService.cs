using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IPersonService : IBusinessService<PersonDto>
    {
        IEnumerable<PersonDto> GetAll(int subdivisionId);
    }
}
