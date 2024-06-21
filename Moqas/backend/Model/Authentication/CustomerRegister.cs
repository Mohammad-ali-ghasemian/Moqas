﻿using System.ComponentModel.DataAnnotations;

namespace Moqas.Model.Authentication
{
    public class CustomerRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.Url)]
        public string WebsiteLink { get; set; } = string.Empty;
    }
}
