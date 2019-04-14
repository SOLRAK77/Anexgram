using Anexgram.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anexgram.PersistenceDBContext.Config
{
    public class CommentsPerPhotoConfig
    {
        public CommentsPerPhotoConfig(EntityTypeBuilder<CommentsPerPhoto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Comment).IsRequired().HasMaxLength(200);
        }
    }
}
