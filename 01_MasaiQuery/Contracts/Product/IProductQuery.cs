namespace _01_MasaiQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> AmazingProducts();
        List<ProductQueryModel> Search(string value);
    }
}
