using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slide
{
  public  interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        //Disable slide
        OperationResult Remove(long id);
        //Delete slide
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
  }
}
