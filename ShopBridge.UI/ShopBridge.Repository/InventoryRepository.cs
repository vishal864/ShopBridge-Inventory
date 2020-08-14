using ShopBridge.Repository.Entities;
using ShopBridge.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using System.Reflection;

namespace ShopBridge.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ShopBridgeDBContext dbContext = new ShopBridgeDBContext();
        
        public async Task<Inventory> GetByIdAsync(int inventoryId)
        {
            Inventory inventory = null;

            try
            {
                await Task.Delay(1000);
                inventory = await dbContext.Inventories.FindAsync(inventoryId);

                if (inventory == null)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                inventory = new Inventory();
                LogException.Log(ex);
            }

            return inventory;
        }

        public async Task<IEnumerable<Inventory>> ListAllAsync()
        {
            IEnumerable<Inventory> inventory = null;

            try
            {
                inventory = await dbContext.Inventories.ToListAsync();
                await Task.Delay(1000);
                return inventory.AsQueryable();
            }
            catch (Exception ex)
            {
                inventory = new List<Inventory>();
                LogException.Log(ex);
            }
            return inventory;
        }

        public async Task SaveAsync(Inventory inventory)
        {
            try
            {
                dbContext.Inventories.Add(inventory);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogException.Log(ex);
            }
        }

        public async Task DeleteAsync(int inventoryId)
        {
            {
                try
                {
                    Inventory inventory = await dbContext.Inventories.FindAsync(inventoryId);
                    dbContext.Inventories.Remove(inventory);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    LogException.Log(ex);
                }
            }
        }

        private bool IsInventoryExists(string inventoryName)
        {
            return dbContext.Inventories.Count(e => e.Name == inventoryName) > 0;
        }

        public async Task<bool> IsExistsAsync(string inventoryName, string isActive = "")
        {
            await Task.Delay(1000);
            return IsInventoryExists(inventoryName);
        }
    }
}
