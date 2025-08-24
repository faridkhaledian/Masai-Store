using CommentManagement.AppliCation.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;

        private readonly ICommentApplication _CommentApplication;

        public IndexModel(ICommentApplication CommentApplication)
        {
            _CommentApplication = CommentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _CommentApplication.Search(searchModel);
        }
        public IActionResult OnGetCancel(long id)
        {

            var result = _CommentApplication.Cancel(id);
            if (result.IsSucceddd)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(long id)
        {
            var result = _CommentApplication.Confirm(id);
            if (result.IsSucceddd)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
