﻿using System.ComponentModel.DataAnnotations;

namespace MediaManager.API.Model;

public class VolumeForUpsertDto
{
    [Required(ErrorMessage = "Please provide Title")]
    [MaxLength(50)]
    public string Title { get; set; } = String.Empty;

    [MaxLength(12)]
    public string? Moniker { get; set; }

    public override string ToString()
    {
        return Title;
    }
}
