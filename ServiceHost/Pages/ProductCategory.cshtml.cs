using _01_MasaiQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {

        public ProductCategoryQueryModel ProductCategory;
        public List<ProductCategoryQueryModel> ListProductCategories;
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }
        #region OnGet
        public void OnGet(string id)
        {
            ProductCategory = _productCategoryQuery.GetProductCategoryWithProductsBy(id);
            ListProductCategories = _productCategoryQuery.GetProductCategories();
        }
        #endregion

    }
}
