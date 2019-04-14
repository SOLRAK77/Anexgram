using Anexgram.Model.Domain.DBHelper;
using Microsoft.AspNetCore.Identity;

namespace Anexgram.Model.Domain
{
    public class ApplicationUser : IdentityUser, ISoftDeleted
    {
        public string AboutUs { get; set; }
        public string Image { get; set; }
        public string SeoURL { get; set; }
        public bool Deleted { get; set; }
    }
}
