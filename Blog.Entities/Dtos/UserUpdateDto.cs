using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [MaxLength(50, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        [MinLength(3, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır.")]
        public string UserName { get; set; }
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [MaxLength(100, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        [MinLength(10, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [MaxLength(13, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        [MinLength(13, ErrorMessage = "{0} alanı minimum {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Profil Resmi")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [DisplayName("Profil Resmi")]
        public string Picture { get; set; }
    }
}
