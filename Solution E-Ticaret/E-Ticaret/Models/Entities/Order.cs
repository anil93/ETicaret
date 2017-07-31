using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey("User")]
        public int UserId { get; set; }
       
        public DateTime OrderDate { get; set; }

        public string ShipAddress { get; set; }

        public virtual User User { get; set; }
    }
}