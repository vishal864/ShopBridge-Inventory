using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ShopBridge.UI.Models
{
    public class InventoryModel
    {        
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [Remote("IsInventoryAvailable","Inventory", ErrorMessage = "Inventory already taken. Please try.")]
        public string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        public string IsActive { get; set; } = "Y";
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}