using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Models.Entities
{
    public class LogIn
    {       
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [StringLength(50, ErrorMessage = "EMail alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Required(ErrorMessage = "EMail alanı gereklidir!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                        ErrorMessage = "EMail adresi geçersiz!")]
        public string EMail { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "Kullanıcı Adı alanı en az 2 karakter uzunluğunda olmalıdır!", MinimumLength = 2)]             
        public string UserName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez!")]
        [StringLength(50, ErrorMessage = "Şifre alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [MaxLength(50)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "İki şifre eşleşmiyor!")]       
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public virtual User User { get; set; }
    }
}