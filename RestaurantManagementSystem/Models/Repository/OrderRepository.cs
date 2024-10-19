
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagementSystem.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantContext context;
        public OrderRepository(RestaurantContext _context)
        {
            context = _context;
        }
        public void Add(Order order)
        {
            
            context.Orders.Add(order);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            Order order = GetById(id);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public List<Order> getAll()
        {
            return context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return context.Orders.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(int id, Order order)
        {
            Order order1 = GetById(id);
            order1.OrderDate = order.OrderDate;
            order1.StatusOrder = order.StatusOrder;
            order1.Items = order.Items;
            context.SaveChanges();
        }
    }
}
