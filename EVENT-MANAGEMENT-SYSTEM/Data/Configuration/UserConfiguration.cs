using EVENT_MANAGEMENT_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVENT_MANAGEMENT_SYSTEM.Data.Configuration
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(n => n.Name).IsRequired(true).HasMaxLength(50);

            builder.Property(e => e.Email).IsRequired(true);
            builder.HasIndex(e => e.Email).IsUnique(true);

            builder.Property(p => p.PasswordHash).IsRequired(true);



            builder.HasMany(re => re.Registrations)
                   .WithOne(u => u.User)
                   .HasForeignKey(r => r.UserId);


           // builder.HasMany(c => c.CreatedEvents)
           //         .WithOne(cre => cre.Creator)
           //         .HasForeignKey(u => u.CreatedBy);

           //builder.HasMany(e=>e.UpdatedEvents)
           //       .WithOne(e=>e.Updater)
           //       .HasForeignKey(e=>e.UpdatedBy)
           //       .OnDelete(DeleteBehavior.SetNull);
                    

        }
    }
}
