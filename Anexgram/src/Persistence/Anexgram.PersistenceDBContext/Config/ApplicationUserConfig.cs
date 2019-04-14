using Anexgram.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anexgram.PersistenceDBContext.Config
{
    public class ApplicationUserConfig
    {
        public ApplicationUserConfig(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.SeoURL).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.AboutUs).HasMaxLength(500);
            entityBuilder.Property(x => x.Image).HasMaxLength(100);

        }
    }
}
