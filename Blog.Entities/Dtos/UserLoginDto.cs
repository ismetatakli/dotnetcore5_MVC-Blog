using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [MaxLength(30, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
