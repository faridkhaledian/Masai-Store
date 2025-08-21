using _01_LampshadeQuery.Contracts.Product;
using _01_MasaiQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductCategoryQueryModel> ListProductCategories;
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;
        public SearchModel(IProductQuery productQuery,IProductCategoryQuery productCategoryQuery)
        {
            _productQuery = productQuery;
            _productCategoryQuery = productCategoryQuery;
        }
        #region OnGet
        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);
            ListProductCategories = _productCategoryQuery.GetProductCategories();
        }
        #endregion
    }
}
