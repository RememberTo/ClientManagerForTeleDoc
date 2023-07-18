using ClientManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.DAL.Repository.Interfaces
{
    public interface IShareholderRepository
    {
        Task<Shareholder> GetById(int id);
        Task<List<Shareholder>> GetAll();
        Task Add(Shareholder shareholder);
        Task Update(Shareholder shareholder);
        Task Delete(Shareholder shareholder);
    }
}
