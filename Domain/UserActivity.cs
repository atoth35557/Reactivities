using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserActivity
    {
        [Column(TypeName = "varchar(200)")]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        [Column(TypeName = "varchar(200)")]
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}