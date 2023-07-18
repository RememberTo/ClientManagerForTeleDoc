using ClientManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.DAL
{
    public class ClientManagerDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Shareholder> Shareholders { get; set; }
        public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>()
            .HasMany(c => c.Shareholders)
            .WithOne(s => s.Client)
            .HasForeignKey(s => s.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);
        }
    }
}
