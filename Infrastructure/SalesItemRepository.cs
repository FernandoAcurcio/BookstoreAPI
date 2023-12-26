using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SalesItemRepository : ISalesItemRepository
    {
        public Task<SalesItem> GetSalesItemByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<SalesItem>> GetSalesItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
