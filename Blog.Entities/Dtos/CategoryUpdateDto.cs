using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş bırakılmamalıdır.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        [MaxLength(200, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        public string Description { get; set; }
        [DisplayName("Kategori Özel Not")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        public string Note { get; set; }
        [DisplayName("Aktiflik Durumu")]
        public bool IsActive { get; set; }
        [DisplayName("Silinme Durumu")]
        public bool IsDeleted { get; set; }
    }
}
