using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Repository.Entities
{
    [Table("Inventory")]
    public class Inventory
    {
        public int InventoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name  { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        //[DefaultValue("Y")]
        [StringLength(1)]
        public string IsActive { get; set; } = "Y";

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]        
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }    
}
