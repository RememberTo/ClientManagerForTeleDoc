using ClientManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.DAL.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetById(int id);
        Task<List<Client>> GetAll();
        Task Add(Client client);
        Task Update(Client client);
        Task Delete(Client client);
    }
}
