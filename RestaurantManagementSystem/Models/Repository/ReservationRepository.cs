using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace RestaurantManagementSystem.Models.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantContext context;
        public ReservationRepository(RestaurantContext _context)
        {
            context = _context;
        }
        public void Add(int tableid,string userid)
        {
            Reversation reversation = new Reversation()
            { ApplicationUserId = userid,
                TableId = tableid };

            context.Reversations.Add(reversation);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            Reversation reversation = GetById(id);
            context.Reversations.Remove(reversation);
            context.SaveChanges();
        }

        public List<Reversation> getAll()
        {
            return context.Reversations.Include(x => x.Table).Include(x => x.ApplicationUser).ToList();
        }

        public Reversation GetById(int id)
        {
            return context.Reversations.Include(x => x.Table).Include(x => x.ApplicationUser).Where(x => x.Id == id).FirstOrDefault();
        }

        public void Update(int id, Reversation reversation)
        {
            Reversation reversation1 = GetById(id);
            reversation1.TableId = reversation.TableId;
            reversation1.ReversationDate = reversation.ReversationDate;
            reversation1.ApplicationUserId = reversation.ApplicationUserId;
            context.SaveChanges();
        }
    }
}
