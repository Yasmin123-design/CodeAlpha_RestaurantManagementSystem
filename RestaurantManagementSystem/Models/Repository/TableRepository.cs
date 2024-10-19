namespace RestaurantManagementSystem.Models.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext context;
        public TableRepository(RestaurantContext _context)
        {
            context = _context;
        }

        public void Add(Table table)
        {
            context.Tables.Add(table);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Table table = GetById(id);
            context.Tables.Remove(table);
            context.SaveChanges();
        }

        public List<Table> getAll()
        {
            return context.Tables.ToList();
        }
        public Table GetById(int id)
        {
            return context.Tables.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(int id, Table table)
        {
            Table table1 = GetById(id);
            table1.TableNumber = table.TableNumber;
            table1.Status = table.Status;
            table1.NoOfChairs = table.NoOfChairs;
            context.SaveChanges();
        }
    }
}
