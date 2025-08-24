using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUploader _fileUploader;
        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }
        #region Create
        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            //upload photo
            var pictureName = _fileUploader.Upload(command.Picture, "slides");

            var slide = new Slide(pictureName, command.PictureAlt,
                command.PictureTitle, command.Heading, command.Title, command.Text, command.Link, command.BtnText);

            _slideRepository.Create(slide);
            _slideRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion
        #region Edit
        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //upload photo
            var pictureName = _fileUploader.Upload(command.Picture, "slides");
            slide.Edit(pictureName, command.PictureAlt,
                command.PictureTitle, command.Heading, command.Title, command.Text, command.Link, command.BtnText);
            _slideRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region GetDetails
        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        #endregion

        #region GetList
        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        #endregion

        #region Remove
        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            //search slide with id
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            slide.Removed();
            _slideRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Restore
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            //search slide with id
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Delete
        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();

            if (_slideRepository.Exists(x => x.Id == id))
            {
                _slideRepository.Delete(id);
                _slideRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);

        }
        #endregion
    }
}
