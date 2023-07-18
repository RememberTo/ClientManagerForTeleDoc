using ClientManager.DAL.Repository.Interfaces;
using ClientManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.DAL.Repository
{
    public class ShareholderRepository : IShareholderRepository
    {
        private readonly ClientManagerDbContext _dbContext;

        public ShareholderRepository(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shareholder> GetById(int id)
        {
            return await _dbContext.Shareholders.Include(s => s.Client).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Shareholder>> GetAll()
        {
            return await _dbContext.Shareholders.ToListAsync();
        }

        public async Task Add(Shareholder shareholder)
        {
            await _dbContext.Shareholders.AddAsync(shareholder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Shareholder shareholder)
        {
            _dbContext.Shareholders.Update(shareholder);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Shareholder shareholder)
        {
            _dbContext.Shareholders.Remove(shareholder);
            await _dbContext.SaveChangesAsync();
        }
    }
}
