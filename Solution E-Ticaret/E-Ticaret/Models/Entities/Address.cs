using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Adres adını yazınız!")]
        [StringLength(50, ErrorMessage = "Adres adı en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Adres detayını yazınız!")]
        [StringLength(500, ErrorMessage = "Adres detayı en fazla {1} karakter uzunluğunda olmalıdır!")]
        public string Detail { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}