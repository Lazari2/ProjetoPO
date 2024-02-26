using StoriesAPI.Infrastruture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoriesAPI.Infrastruture.Configuration
{
    public class StoryEntityConfiguration: IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Story", StoryContext.DEFAULT_SCHEMA);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Title)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                .IsRequired(true)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Departament)
                .IsRequired(true)
                .HasMaxLength(30)
                .IsUnicode(true);
        }
    }
}
