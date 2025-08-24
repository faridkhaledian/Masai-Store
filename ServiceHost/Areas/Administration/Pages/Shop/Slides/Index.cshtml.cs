using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<SlideViewModel> Slides;
        private readonly ISlideApplication _slideApplication;
        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        #region OnGet
        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }
        #endregion

        #region OnGetCreate
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        #endregion

        #region OnPostCreate
        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        #endregion

        #region OnGetEdit
        public IActionResult OnGetEdit(long id)
        {
            var slide = _slideApplication.GetDetails(id);
            return Partial("Edit", slide);
        }

        #endregion

        #region OnPostEdit
        public JsonResult OnPostEdit(EditSlide command)
        {
            //if (ModelState.IsValid)
            //{
            //}
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        #endregion

        #region OnGetRemove
        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        #endregion

        #region OnGetRestore
        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion

        #region Delete
        public IActionResult OnGetDelete(long id)
        {
            var result = _slideApplication.Delete(id);
            if (result.IsSucceddd)
            {
                return RedirectToPage("./Index");
            }
            TempData["Message"] = result.Message;
            return RedirectToPage("./Index");
        }
        #endregion

    }
}
