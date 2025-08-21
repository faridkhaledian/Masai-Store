using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        #region Define
        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x => x.ProducId == command.ProducId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            //define command Discount 
            var customerDiscount = new CustomerDiscount(command.ProducId, command.DiscountRate, startDate, endDate, command.Reason);

            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region Edit
        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _customerDiscountRepository.Get(command.Id);

            if (customerDiscount == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_customerDiscountRepository.Exists(x => x.ProducId == command.ProducId &&
            x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            //edit command discount
            customerDiscount.Edit(command.ProducId, command.DiscountRate, startDate, endDate, command.Reason);
            _customerDiscountRepository.SaveChange();
            return operation.Succedded();
        }

        #endregion

        #region GetDetails
        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        #endregion

        #region Search
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }

        #endregion

        #region Delete
        //Delete physical
        public OperationResult Delete(long id)
        {
            var operation= new OperationResult();
            if (_customerDiscountRepository.Exists(x=> x.Id == id) )
            {
                _customerDiscountRepository.Delete(id);
                _customerDiscountRepository.SaveChange();
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        #endregion

    }
}
