using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
   public class CustomerDiscount :EntityBase
    {
        public long ProducId { get;private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public CustomerDiscount(long producId, int discountRate, DateTime startDate,
            DateTime endDate, string reason)
        {
            ProducId = producId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
        public void Edit(long producId, int discountRate, DateTime startDate,
            DateTime endDate, string reason)
        {
            ProducId = producId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;

        }
    }
}
