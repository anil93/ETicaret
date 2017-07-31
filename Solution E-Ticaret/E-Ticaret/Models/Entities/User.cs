using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class User
    {
        public User()
        {
            addresses = new List<Address>();

            Orders = new List<Order>();
        }

        [Key]
        [ForeignKey("LogIn")]
        public int LogInId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]       
        public string FirstName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]        
        public string LastName { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> BirthDate { get; set; }
      
        public virtual LogIn  LogIn { get; set; }

        public List<Address> addresses { get; set; }

        public List<Order> Orders { get; set; }
    }
}