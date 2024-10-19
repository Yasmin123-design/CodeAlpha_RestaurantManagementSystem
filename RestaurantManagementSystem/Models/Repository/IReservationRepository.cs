namespace RestaurantManagementSystem.Models.Repository
{
    public interface IReservationRepository
    {
        List<Reversation> getAll();
        void Add(int tableid , string userid);
        Reversation GetById(int id);
        void Update(int id, Reversation reversation);
        void Delete(int id);
    }
}
