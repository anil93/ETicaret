using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Kategori adını yazınız!")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Description { get; set; }

        [MaxLength(500)]
        public string categoryImagePath { get; set; }       

        public List<Product> Products { get; set; }
    }
}