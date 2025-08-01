using _01_MasaiQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

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
        public void OnGet(string id)
        {
            ProductCategory = _productCategoryQuery.GetProductCategoryWithProductsBy(id);
            ListProductCategories = _productCategoryQuery.GetProductCategories();
        }
    }
}
