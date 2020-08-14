using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridge.Repository.Entities;
using ShopBridge.Service.Interfaces;
using System;
using ShopBridge.Repository.Interfaces;
using ShopBridge.Repository;

namespace ShopBridge.Service.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await _inventoryRepository.GetByIdAsync(id);
        }

        public async Task SaveAsync(Inventory inventory)
        {
            await _inventoryRepository.SaveAsync(inventory);
        }

        public async Task DeleteAsync(int inventoryId)
        {
            await _inventoryRepository.DeleteAsync(inventoryId);
        }

        public async Task<IEnumerable<Inventory>> ListAllAsync()
        {
            return await _inventoryRepository.ListAllAsync();
        }

        public async Task<bool> IsExistsAsync(string Name, string isActive = "")
        {
            return await _inventoryRepository.IsExistsAsync(Name);
        }
    }
}