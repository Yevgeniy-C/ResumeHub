﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ResumeHub.ViewModels
{
	public class LoginViewModel
	{
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}

