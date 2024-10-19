namespace RestaurantManagementSystem.Models.Repository
{
    public interface IMenuRepository
    {
        List<MenuItem> getAll();
        void Add(MenuItem item);
        MenuItem GetById(int id);
        void Update(int id, MenuItem item);
        void Delete(int id);
    }
}
