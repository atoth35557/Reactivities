using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Comment
    {
        [Key]
        [Column(TypeName = "varchar(200)")]
        public Guid Id { get; set; }
        public string Body { get; set; }
        public virtual AppUser Author { get; set; }
        public virtual Activity Activity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
}