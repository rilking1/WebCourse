﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name ="Підтвердження паролю")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
