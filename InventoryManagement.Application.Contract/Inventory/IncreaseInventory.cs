using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
   public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        [Required(ErrorMessage =ValidationMessages.IsRequired) ]
        public long Count { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
    }
}
