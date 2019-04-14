using Anexgram.Model.Domain.DBHelper;

namespace Anexgram.Model.Domain
{
    public class Photo : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public bool Deleted { get; set; }
    }
}
