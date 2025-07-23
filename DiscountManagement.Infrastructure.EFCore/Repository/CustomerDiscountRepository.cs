using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _Context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext context , ShopContext shopContext) : base(context)
        {
            _Context = context;
            _shopContext = shopContext;
        }
        public EditCustomerDiscount GetDetails(long id)
        {
            return _Context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                ProducId = x.ProducId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(), 
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason,

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products=_shopContext.Products.Select(x=> new {x.Id , x.Name}).ToList();
            var query = _Context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateGr = x.EndDate,
                StartDate = x.StartDate.ToFarsi(),
                StartDateGr = x.StartDate,
                ProducId = x.ProducId,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProducId>0)
            {
                query=query.Where(x=> x.ProducId==searchModel.ProducId);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.StartDate) )
            {             
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime( ));
            }  
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate) )
            {
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime() );
            }
            var discounts=query.OrderByDescending(x=> x.Id).ToList();

            discounts.ForEach(discounts  =>
 
            discounts.Product =products.FirstOrDefault(x=> x.Id == discounts.ProducId)?.Name

            );
            return discounts;

        }
    }
}
