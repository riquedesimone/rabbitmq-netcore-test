using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserActivity.Domain.Entities;

namespace LoadUserActivity.Data.Mappings
{
    public class ActivityMap: IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity");
        }
    }
}
