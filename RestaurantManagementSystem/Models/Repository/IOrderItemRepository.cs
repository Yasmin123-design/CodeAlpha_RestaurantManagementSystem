namespace RestaurantManagementSystem.Models.Repository
{
    public interface IOrderItemRepository
    {
        List<OrderItem> getAll();
        void Add(OrderItem orderItem);
        OrderItem GetById(int id);
        void Update(int id, OrderItem orderItem);
        void Delete(int id);
    }
}
