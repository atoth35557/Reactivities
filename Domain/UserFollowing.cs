using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserFollowing
    {
        [Column(TypeName = "varchar(200)")]
        public string ObserverId { get; set; }
        public virtual AppUser Observer { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TargetId { get; set; }
        public virtual AppUser Target { get; set; }
    }
}