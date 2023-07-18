using ClientManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientManager.DAL.EntityTypeConfiguration
{
    internal class ShareholderConfiguration : IEntityTypeConfiguration<Shareholder>
    {
        public void Configure(EntityTypeBuilder<Shareholder> builder)
        {
            builder.ToTable("Shareholders"); 

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.INN).IsRequired().HasMaxLength(12);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired();

            builder.HasOne(s => s.Client)
                .WithMany(c => c.Shareholders)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
