
namespace RestaurantManagementSystem.Models.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly RestaurantContext context;
        public ReportRepository(RestaurantContext _context)
        {
            context = _context;
        }

        public List<Inventory> GetInventoryReport()
        {
            return context.Inventories.ToList();
        }

        public List<Order> GetOrderReport(DateTime starttime, DateTime endtime)
        {
            return context.Orders.Where(x => x.OrderDate >= starttime && x.OrderDate <= endtime ).ToList();
        }

        public List<Reversation> GetReversationReport(DateTime starttime, DateTime endtime)
        {
            return context.Reversations.Where(x => x.ReversationDate >= starttime && x.ReversationDate <= endtime).ToList();
        }
        
    }
}
