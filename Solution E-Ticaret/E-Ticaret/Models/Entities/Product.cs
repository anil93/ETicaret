using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Ürün adını yazınız!")]
        [StringLength(50, ErrorMessage = "Ürün adı en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Kategorisini belirleyiniz!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Fiyat giriniz!")]        
        public int UnitPrice { get; set; }

        [Required(ErrorMessage = "Stok adedi giriniz!")]
        public int UnitInStock { get; set; }

        public bool Discontinued { get; set; }

        [MaxLength(500)]
        public string productImagePath { get; set; }

        public virtual Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}