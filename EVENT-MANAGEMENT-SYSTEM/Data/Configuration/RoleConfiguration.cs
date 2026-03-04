using EVENT_MANAGEMENT_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVENT_MANAGEMENT_SYSTEM.Data.Configuration
{
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleName).IsRequired(true).HasMaxLength(50);

            builder.HasMany(u => u.Users)
                   .WithOne(r => r.Role)
                   .HasForeignKey(r => r.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
                   

        }
    }
}
