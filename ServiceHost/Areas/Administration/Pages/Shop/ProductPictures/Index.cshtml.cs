using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictures;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPitureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPitureApplication;
        }

        #region OnGet
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        #endregion

        #region OnGetCreate
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }
        #endregion

        #region OnPostCreate
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        #endregion

        #region OnGetEdit
        public IActionResult OnGetEdit(long id)
        {
            var ProductPicture = _productPictureApplication.GetDetails(id);
            ProductPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", ProductPicture);
        }
        #endregion

        #region OnPostEdit
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            //if (ModelState.IsValid) { 
            //}
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }
        #endregion

        #region OnGetRemove
        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        #endregion

        #region OnGetRestore
        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        #endregion

        #region Delete
        public IActionResult OnGetDelete(long id)
        {
            var result = _productPictureApplication.Delete(id);
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
