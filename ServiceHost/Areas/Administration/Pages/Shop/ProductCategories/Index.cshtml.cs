using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;

        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }


        #region OnGet
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        #endregion

        #region OnGetCreate
        public IActionResult OnGetCreate()
        {

            return Partial("./Create", new CreateProductCategory());
        }

        #endregion

        #region OnPostCreate
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        #endregion

        #region OnGetEdit
        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", productCategory);
        }

        #endregion

        #region OnPostEdit
        public JsonResult OnPostEdit(EditProductCategory command)
        {

            //if (ModelState.IsValid)
            //{
            //}

            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

        #endregion

        #region Delete
        public IActionResult OnGetDelete(long id)
        {
            var result = _productCategoryApplication.Delate(id);
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
