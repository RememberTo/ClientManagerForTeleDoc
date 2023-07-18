using ClientManager.DAL.Repository.Interfaces;
using ClientManager.Domain;
using Microsoft.EntityFrameworkCore;
namespace ClientManager.DAL.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientManagerDbContext _dbContext;

        public ClientRepository(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetById(int id)
        {
            return await _dbContext.Clients.Include(c => c.Shareholders).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Client>> GetAll()
        {
            return await _dbContext.Clients.ToListAsync();
        }

        public async Task Add(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
