using _0_Framework.Application;
using CommentManagement.AppliCation.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        #region Add
        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Email, command.Website, command.Message
             , command.OwnerRecordId, command.Type, command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region Accept the comment
        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Confirm();
            _commentRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region Cancel the comment
        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Cancel();
            _commentRepository.SaveChange();
            return operation.Succedded();
        }
        #endregion

        #region Search
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
        #endregion

        #region Delete
        public OperationResult Delete(long id)
        {
            var operation = new OperationResult();

            if (_commentRepository.Exists(x => x.Id == id))
            {
                _commentRepository.Delete(id);
                _commentRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        #endregion

    }
}
