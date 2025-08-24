using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.AppliCation.Contracts.Comment
{

    public class AddComment
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLenght)]
        [EmailAddress(ErrorMessage = "فرمت نوشته شده درست نمی باشد")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(1000, ErrorMessage = ValidationMessages.MaxLenght)]
        public string Message { get; set; }

        public long ProductId { get; set; }
        public string? Website { get; set; }
        public long OwnerRecordId { get; set; }//ProductId Or ArticleId
        public int Type { get; set; } //Product Or Article
        public long ParentId { get; set; }
    }

}
