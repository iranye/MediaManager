using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaManager.API.Data.Entities;

public sealed class M3uFile
{
    public M3uFile(string title)
    {
        Title = title;
        Created = DateTime.Now;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = String.Empty;

    public int TotalMegaBytes { get; set; } = 42;

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? LastModified { get; set; } = null;

    public ICollection<FileEntry> FilesInM3U { get; set; } = new List<FileEntry>();

    [ForeignKey("VolumeId")]
    public Volume? Volume { get; set; }
    public int? VolumeId { get; set; }

    public override string ToString()
    {
        return Title;
    }
}
