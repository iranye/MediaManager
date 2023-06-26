﻿using System.ComponentModel.DataAnnotations;

namespace MediaManager.API.Controllers
{
    public class AuthenticationRequestBody
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? UserName { get; set; } = String.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? Email { get; set; } = String.Empty;

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string? Password { get; set; } = String.Empty;
    }
}
