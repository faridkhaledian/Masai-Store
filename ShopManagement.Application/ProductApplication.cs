using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        #region Create
        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            //convert name to slug
            var slug = command.Slug.slugify();
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"{categorySlug}//{slug}";
            //Upload selected photo 
            var picturePath = _fileUploader.Upload(command.Picture, path);
            //Populating a Product object
            var product = new Product(command.Name, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt, command.PictureTitle,
                command.CategoryId, slug, command.KeyWords, command.MetaDescription
                );
            _productRepository.Create(product);
            _productRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Edit
        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            //convert name to slug
            var slug = command.Slug.slugify();
            var path = $"{product.Category.Slug}/{slug}";

            var picturePath = _fileUploader.Upload(command.Picture, path);
            //edit product
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt, command.PictureTitle,
                command.CategoryId, slug, command.KeyWords, command.MetaDescription
                );
            _productRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region GetDetails
        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
        #endregion

        #region GetProducts
        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        #endregion

        #region Search
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
        #endregion

        #region Delete
        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Id == id))
            {
                _productRepository.Delete(id);

                _productRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);

        }
        #endregion
    }
}
