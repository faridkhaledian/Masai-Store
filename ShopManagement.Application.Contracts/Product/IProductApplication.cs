using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct  command);
        OperationResult Edit(EditProduct command);
        OperationResult Delete(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
         List<ProductViewModel> Search(ProductSearchModel searchModel);
 }
}
