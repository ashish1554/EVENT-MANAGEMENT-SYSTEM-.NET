using EVENT_MANAGEMENT_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVENT_MANAGEMENT_SYSTEM.Data.Configuration
{
    public class RegisterConfiguration:IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(r => r.RegistrationId);
            builder.Property(s => s.Status).IsRequired(true).HasDefaultValue("Active");
            builder.HasIndex(r => new { r.UserId, r.EventId })
                   .IsUnique();
        }
    }
}
