using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShopBridge.Repository.Entities;
using ShopBridge.UI.Models;
using AutoMapper;

namespace ShopBridge.UI.Controllers
{
    public class InventoryController : BaseController
    {
        private const string APIBASEADDRESS = "ApiBaseAddress";
        private readonly string apiBaseAddress = ConfigurationManager.AppSettings[APIBASEADDRESS]?.ToString();
        private IMapper _mapper;

        public InventoryController()
        {
            initializeAutoMapper();
        }

        /// <summary>
        /// Configure automapper for model
        /// </summary>
        private void initializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inventory, InventoryModel>();
            });

            _mapper = config.CreateMapper();
        }

        public ActionResult Index()
        {
            return View();
        }       

        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Inventory> inventory = null;
            IEnumerable<InventoryModel> inventoryDto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync("inventory/get");

                if (result.IsSuccessStatusCode)
                {
                    inventory = await result.Content.ReadAsAsync<IList<Inventory>>();
                    inventoryDto = _mapper.Map<List<InventoryModel>>(inventory).OrderByDescending(i => i.CreatedDate).ToList<InventoryModel>();
                }
                else
                {
                    inventoryDto = Enumerable.Empty<InventoryModel>();
                }
            }

            return Json(new { data = inventoryDto }, JsonRequestBehavior.AllowGet);
        }

        [Route("Inventory/Get/{inventoryId}")]
        public async Task<ActionResult> GetById(int inventoryId)
        {
            InventoryModel inventoryDto = null;

            if (inventoryId <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory inventory = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"inventory/get/{inventoryId}");

                if (result.IsSuccessStatusCode)
                {
                    inventory = await result.Content.ReadAsAsync<Inventory>();
                    inventoryDto = _mapper.Map<InventoryModel>(inventory);
                }
                else
                {
                    return Json(new InventoryModel(), JsonRequestBehavior.AllowGet);
                }
            }

            if (inventory == null)
            {
                return HttpNotFound();
            }
            return Json(inventoryDto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]       
        public async Task<ActionResult> Save(InventoryModel inventory)
        {
            if (ModelState.IsValid)
            {
                bool isAvailable = await IsInventoryAvailable(inventory.Name);

                if (!isAvailable) //Save
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseAddress);

                        var result = await client.PostAsJsonAsync("inventory/create", inventory);

                        if (!result.IsSuccessStatusCode)
                        {
                            return Json(data: "Failed", behavior: JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(data: "InventoryAvailable", behavior: JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data: "Success", behavior: JsonRequestBehavior.AllowGet);
        }        

        [HttpPost , ActionName("Delete")]
        [Route("Inventory/Delete/{inventoryId}")]
        public async Task<ActionResult> DeleteRecord(int inventoryId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var response = await client.DeleteAsync($"inventory/delete/{inventoryId}");

                if (response.IsSuccessStatusCode)
                {
                    return Json(data: "Success", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: "Failed", behavior: JsonRequestBehavior.AllowGet);
                }
            }
        }
        
        private async Task<bool> IsInventoryAvailable(string Name)
        {
            bool isAvailable = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseAddress);

                var result = await client.GetAsync($"inventory/isinventoryavailable/{Name}");

                if (result.IsSuccessStatusCode)
                {
                    isAvailable = await result.Content.ReadAsAsync<bool>(); 
                }
            }

            return isAvailable;
        }
    }
}