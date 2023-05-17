using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.API.Data.Entities
{
    public class Volume
    {
        public Volume(string title, string moniker)
        {
            Title = title;
            Moniker = moniker;
            Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Moniker { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? LastModified { get; set; } = null;

        public ICollection<M3uFile> M3uFiles { get; set; } = new List<M3uFile>();

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
