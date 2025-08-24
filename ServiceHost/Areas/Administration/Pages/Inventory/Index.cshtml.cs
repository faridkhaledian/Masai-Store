using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventorys;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication ProductApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = ProductApplication;
            _inventoryApplication = inventoryApplication;
        }

        #region OnGet
        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventorys = _inventoryApplication.Search(searchModel);
        }
        #endregion

        #region OnGetCreate
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        #endregion

        #region OnPostCreate
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }
        #endregion

        #region OnGetEdit
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);
        }
        #endregion

        #region OnPostEdit
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        #endregion

        #region OnGetIncrease
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }
        #endregion

        #region OnPostIncrease
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        #endregion

        #region OnGetReduce
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("Reduce", command);
        }
        #endregion

        #region OnPostReduce
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            if (!result.IsSucceddd)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }
        #endregion

        #region OnGetLog
        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);

            return Partial("OperationLog", log);
        }
        #endregion

        #region Delete
        public IActionResult OnGetDelete(long id)
        {
            var result = _inventoryApplication.Delete(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            TempData["Message"] = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
