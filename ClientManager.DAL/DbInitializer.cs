
namespace ClientManager.DAL
{
    public class DbInitializer
    {
        public static void Initialize(ClientManagerDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
