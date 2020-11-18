using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Photo
    {
        [Key]
        [Column(TypeName = "varchar(200)")]
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}