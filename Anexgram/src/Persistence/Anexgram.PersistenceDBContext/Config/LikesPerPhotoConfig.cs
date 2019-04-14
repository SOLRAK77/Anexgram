using Anexgram.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anexgram.PersistenceDBContext.Config
{
    public class LikesPerPhotoConfig
    {
        public LikesPerPhotoConfig(EntityTypeBuilder<LikesPerPhoto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
