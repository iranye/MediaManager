using System.ComponentModel.DataAnnotations;

namespace MediaManager.API.Model;

public class VolumeForUpsert
{
    [Required(ErrorMessage = "Please provide Title")]
    [MaxLength(50)]
    public string Title { get; set; } = String.Empty;

    [MaxLength(12)]
    public string? Moniker { get; set; }
}
