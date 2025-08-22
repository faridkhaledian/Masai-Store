using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        #region  Create
        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            //create inventory
            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Edit
        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            //Edit command inventory
            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region GetDetails
        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        #endregion

        #region GetOperationLog
        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }

        #endregion

        #region Increase
        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                operation.Failed(ApplicationMessage.RecordNotFound);

            const long OperatorId = 1;
            //Increase the number of product
            inventory.Increase(command.Count, OperatorId, command.Description);
            _inventoryRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Reduce
        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                operation.Failed(ApplicationMessage.RecordNotFound);

            const long operatorId = 1;
            //Reduce the number of product for one product
            var success = inventory.Reduce(command.Count, operatorId, command.Description, 0);
            if (!success)
               return operation.Failed(ApplicationMessage.RecordNotNegative);

            _inventoryRepository.SaveChange();
            return operation.Succedded();

        }

        #endregion

        #region Reduce
        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            try
            {
                const long operatorId = 1;
                //Reduce the number of product for list<product>
                foreach (var item in command)
                {
                    var inventory = _inventoryRepository.GetBy(item.ProductId);
                    inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
                }
                _inventoryRepository.SaveChange();
                return operation.Succedded();

            }
            catch (InvalidOperationException ex)
            {
                return operation.Failed(ex.Message);// Pass error back to client
            }
        }

        #endregion

        #region Search
        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        #endregion

        #region Delete
        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.Id == id))
            {
                _inventoryRepository.Delete(id);
                _inventoryRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        #endregion

    }
}
