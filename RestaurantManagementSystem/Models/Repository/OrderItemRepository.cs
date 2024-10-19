namespace RestaurantManagementSystem.Models.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly RestaurantContext context;
        public OrderItemRepository(RestaurantContext _context)
        {
            context = _context;
        }
        public void Add(OrderItem orderitem)
        {
            context.OrderItems.Add(orderitem);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            OrderItem orderitem = GetById(id);
            context.OrderItems.Remove(orderitem);
            context.SaveChanges();
        }

        public List<OrderItem> getAll()
        {
            return context.OrderItems.ToList();
        }

        public OrderItem GetById(int id)
        {
            return context.OrderItems.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(int id, OrderItem orderitem)
        {
            OrderItem orderitem1 = GetById(id);
            orderitem1.MenuItemId = orderitem.MenuItemId;
            orderitem1.OrderId = orderitem.OrderId;            
            context.SaveChanges();
        }

       
    }
}
