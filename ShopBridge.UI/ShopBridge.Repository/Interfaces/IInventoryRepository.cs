using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridge.Repository.Entities;

namespace ShopBridge.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByIdAsync(int inventoryId);
        Task<IEnumerable<Inventory>> ListAllAsync();
        Task SaveAsync(Inventory inventory);
        Task DeleteAsync(int inventoryId);
        Task<bool> IsExistsAsync(string Name, string isActive = "");
    }
}
