using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }
        public OperationResult Define(DefineColleagueDiscount command)
        {  
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x=> x.ProductId == command.ProductId && x.DiscountRate==command.DiscountRate)   )
            {
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            }
            var colleagueDiscount=new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChange();
            return operation.Succedded();
        }
        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount=_colleagueDiscountRepository.Get(command.Id);
            if (colleagueDiscount==null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }

            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            }

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChange();
            return operation.Succedded();
        }
        public EditColleagueDiscount GetDetails(long id)
        {
           return _colleagueDiscountRepository.GetDetails(id);
        }
        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChange();
            return operation.Succedded();
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChange();
            return operation.Succedded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
           return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
