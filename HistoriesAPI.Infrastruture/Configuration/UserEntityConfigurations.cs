using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoriesAPI.Infrastruture.Models;

namespace StoriesAPI.Infrastruture.Configuration
{
    public class UserEntityConfigurations : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", StoryContext.DEFAULT_SCHEMA);

            builder.Property(e => e.Id)
           .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);
        }
    }
}
