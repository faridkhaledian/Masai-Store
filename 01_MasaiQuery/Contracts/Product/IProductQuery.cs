namespace _01_LampshadeQuery.Contracts.Product
{
   public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> AmazingProducts();
        List<ProductQueryModel> Search(string value);
    }
}
