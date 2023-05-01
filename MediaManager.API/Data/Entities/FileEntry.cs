using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MediaManager.API.Data.Entities;

public class FileEntry
{
    public FileEntry(string name) 
    {
        Name = name;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<M3uFile> M3uFiles { get; set; } = new List<M3uFile>();

    public override string ToString()
    {
        return Name;
    }
}
