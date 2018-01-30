using NetMastery.InventoryManager.Bl.DtoEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetMastery.InventoryManager.Bl.Servicies.Interfaces
{
    public interface IOrganizationService : IBusinessService
    {

        IEnumerable<OrganizationDto> GetAll(int accountId);
    }
}
