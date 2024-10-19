namespace RestaurantManagementSystem.Models.Repository
{
    public interface ITableRepository
    {
        List<Table> getAll();
        void Add(Table table);
        Table GetById(int id);
        void Update(int id, Table table);
        void Delete(int id);
    }
}
