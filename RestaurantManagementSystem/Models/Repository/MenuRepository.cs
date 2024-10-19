
namespace RestaurantManagementSystem.Models.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantContext context;
        public MenuRepository(RestaurantContext _context)
        {
            context = _context;
        }

        public void Add(MenuItem item)
        {
            context.MenuItems.Add(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            MenuItem item = GetById(id);
            context.MenuItems.Remove(item);
            context.SaveChanges();
        }

        public List<MenuItem> getAll()
        {
            return context.MenuItems.ToList();
        }
        public MenuItem GetById(int id)
        {
            return context.MenuItems.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(int id, MenuItem item)
        {
            MenuItem item1 = GetById(id);
            item1.Name = item.Name;
            item1.Price = item.Price;
            item1.Size = item.Size;
            context.SaveChanges();
        }
    }
}
