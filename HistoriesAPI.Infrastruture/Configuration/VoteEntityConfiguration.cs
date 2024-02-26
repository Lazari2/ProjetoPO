using StoriesAPI.Infrastruture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace StoriesAPI.Infrastruture.Configuration
{
    public class VoteEntityConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Vote", StoryContext.DEFAULT_SCHEMA);

            builder.Property(e => e.Id)
           .ValueGeneratedOnAdd();

            builder.Property(e => e.Voted)
                .IsRequired(true)
                .HasMaxLength(5)
                .IsUnicode(true);
        }
    }
}
