using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ShopBridge.Model;
using ShopBridge.Repository.Entities;
using ShopBridge.Service.Interfaces;

namespace ShopBridge.API.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [Route("api/Inventory/Get")]
        public async Task<IEnumerable<Inventory>> GetAll()
        {
            var result = await _inventoryService.ListAllAsync();
            return result;
        }

        [HttpGet]
        [Route("api/Inventory/Get/{inventoryId}")]
        public async Task<Inventory> GetById(int inventoryId)
        {
            var result = await _inventoryService.GetByIdAsync(inventoryId);

            if (result == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Inventory doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            return result;
        }      

        [HttpPost]
        [Route("api/Inventory/Create")]      
        public async Task SaveAsync([FromBody]Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                await _inventoryService.SaveAsync(inventory);
            }
        }

        [HttpDelete]
        [Route("api/Inventory/Delete/{inventoryId}")]
        public async Task DeleteAsync(int inventoryId)
        {
            await _inventoryService.DeleteAsync(inventoryId);
        }

        [HttpGet]
        [Route("api/Inventory/IsInventoryAvailable/{Name}")]
        public async Task<bool> IsExistsAsync(string Name, string isActive = "")
        {
            bool isAvailable = false;
            isAvailable = await _inventoryService.IsExistsAsync(Name);
            return isAvailable;
        }
    }
}
