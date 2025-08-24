using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.AppliCation.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {

        private readonly CommentContext _Context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _Context = context;
        }
        #region Searach
        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            //get all comments
            var query = _Context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Website = x.Website,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    IsConfirmed = x.IsConfirmed,
                    IsCanceled = x.IsCanceled,
                    CommentDate = x.CreationDate.ToFarsi(),

                });
            //set filter
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();

        }
        #endregion
    }
}
