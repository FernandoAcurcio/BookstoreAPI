using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISalesItemRepository
    {
        Task<IReadOnlyList<SalesItem>> GetSalesItemsAsync();
        Task<SalesItem> GetSalesItemByIdAsync(int id);
    }
}
