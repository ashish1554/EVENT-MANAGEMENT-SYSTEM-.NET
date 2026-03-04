using EVENT_MANAGEMENT_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVENT_MANAGEMENT_SYSTEM.Data.Configuration
{
    public class EventConfiguration:IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
   

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventName).IsRequired(true).HasMaxLength(50);
            builder.Property(e=>e.Description).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.EventDate).IsRequired(true);
            builder.Property(e => e.Location).IsRequired(true).HasMaxLength(50);

            builder.Property(e => e.UpdatedBy).HasDefaultValue(null);

            builder.HasMany(r => r.Registrations)
                   .WithOne(e => e.Event)
                   .HasForeignKey(e => e.EventId);

            builder.HasOne(e => e.Creator)
                .WithMany(u => u.CreatedEvents)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Updater)
               .WithMany(u => u.UpdatedEvents)
               .HasForeignKey(e => e.UpdatedBy)
               .OnDelete(DeleteBehavior.SetNull); 

        }
    }
}
