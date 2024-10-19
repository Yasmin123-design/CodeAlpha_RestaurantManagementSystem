namespace RestaurantManagementSystem.Models.Repository
{
    public interface IReportRepository
    {
        List<Order> GetOrderReport(DateTime starttime, DateTime endtime);
        List<Reversation> GetReversationReport(DateTime starttime, DateTime endtime);
        List<Inventory> GetInventoryReport();
    }
}
