using NetMastery.InventoryManager.Bl.DtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface ISubdivisionService : IBusinessService<SubdivisionDto>
    {
        IEnumerable<SubdivisionDto> GetAll(int subdivisionId);
    }
}
