using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductCategoryApplication(IProductCategoryRepository ProductCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = ProductCategoryRepository;
            _fileUploader = fileUploader;
        }
        #region Create
        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            //convert name to slug
            var slug = command.Slug.slugify();
            //Build the right path
            var picturePath = $"{command.Slug}";
            var PictureName = _fileUploader.Upload(command.Picture, picturePath);

            var productCategory = new ProductCategory(command.Name, command.Description,
                PictureName, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region Edit
        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            //convert name to slug
            var slug = command.Slug.slugify();
            //Build the right path
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            productCategory.Edit(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            _productCategoryRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region GetDetails
        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        #endregion

        #region GetProductCategories
        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        #endregion

        #region Search
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        #endregion

        #region Delete
        public OperationResult Delate(long id)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Id == id))
            {
                _productCategoryRepository.Delete(id);
                _productCategoryRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }

        #endregion
    }
}
