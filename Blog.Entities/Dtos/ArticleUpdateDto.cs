using Blog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Dtos
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Makale Başlığı")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string Title { get; set; }
        [DisplayName("Makale İçeriği")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MinLength(20, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string Content { get; set; }
        [DisplayName("Makale Resmi")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(250, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string Thumbnail { get; set; }
        [DisplayName("Makale Tarihi")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("SEO Yazar")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string SeoAuthor { get; set; }
        [DisplayName("SEO Açıklama")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string SeoDescription { get; set; }
        [DisplayName("SEO Etiketleri")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır")]
        [MinLength(0, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string SeoTags { get; set; }
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} boş olmamalıdır")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayName("Aktiflik Durumu")]
        public bool IsActive { get; set; }
        [DisplayName("Silinme Durumu")]
        public bool IsDeleted { get; set; }
    }
}
