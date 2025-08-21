using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }
        #region Create
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.ProductId);
            //Build the right path
            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            //Populating a productPicture object
            var productPicture = new ProductPicture(command.ProductId, picturePath,
                command.PictureAlt, command.PictureTitle);

            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region Edit
        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //Build the right path
            var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);

            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion
        
        #region GetDetails
        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        #endregion
        
        #region Remove
        //Disabled
        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            //find productPicture
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion
        
        #region Search
        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        #endregion
        
        #region Restore
        //Activation
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            //find productPicture
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Delete
        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exists(x=> x.Id==id) )
            {
                _productPictureRepository.Delete(id);
                _productPictureRepository.SaveChange() ;
                return operation.Succedded() ;
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);
           
        }
        #endregion
    }
}
