using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long ProductId { get; private set; }
        public long OwnerRecordId { get; private set; } // product Id
        public int Type { get; private set; } //Product Or Article
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public Comment()
        {

        }
        public Comment(string name, string email, string website, string message, long ownerRecordId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
        }
        public void Confirm()
        {
            IsConfirmed = true;
            IsCanceled = false;
        }
        public void Cancel()
        {
            IsCanceled = true;
            IsConfirmed = false;
        }
    }
}
