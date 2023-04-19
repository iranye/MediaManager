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

    [ForeignKey("M3uId")]
    public M3uFile? M3uFile { get; set; }
    public int M3uId { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
