using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }
        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(command.Id);

            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && 
            x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }
        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }
        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            productPicture.Remove();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }
        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            productPicture.Restore();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }
    }
}
