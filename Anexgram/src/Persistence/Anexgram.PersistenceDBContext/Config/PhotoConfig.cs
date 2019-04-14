using Anexgram.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anexgram.PersistenceDBContext.Config
{
    public class PhotoConfig
    {
        public PhotoConfig(EntityTypeBuilder<Photo> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Url).IsRequired().HasMaxLength(100);
        }
    }
}
