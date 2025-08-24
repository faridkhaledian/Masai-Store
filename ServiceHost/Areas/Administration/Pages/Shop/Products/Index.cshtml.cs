using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;


namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;
        public SelectList ProductCategories;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }
        #region OnGet
        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            Products = _productApplication.Search(searchModel);
        }
        #endregion

        #region OnGetCreate
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        #endregion

        #region OnPostCreate
        public JsonResult OnPostCreate(CreateProduct command)
        {
            //if (!ModelState.IsValid)
            //{
            //    return new JsonResult("error");
            //}
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }
        #endregion

        #region OnGetEdit
        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", product);
        }

        #endregion

        #region OnPostEdit
        public JsonResult OnPostEdit(EditProduct command)
        {
            //if (ModelState.IsValid)
            //{

            //}
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }
        #endregion

        #region Delete
        public IActionResult OnGetDelete(long id)
        {
            var result = _productApplication.Delete(id);
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
