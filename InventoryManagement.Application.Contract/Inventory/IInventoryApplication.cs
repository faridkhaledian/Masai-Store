using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
   public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);    
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command); //user in inventory
        OperationResult Reduce(List<ReduceInventory> command); //Customer
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
