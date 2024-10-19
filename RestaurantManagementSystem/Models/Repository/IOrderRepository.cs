namespace RestaurantManagementSystem.Models.Repository
{
    public interface IOrderRepository
    {
        List<Order> getAll();
        void Add(Order order);
        Order GetById(int id);
        void Update(int id, Order order);
        void Delete(int id);
    }
}
