using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        [ForeignKey("Order")]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key]
        [ForeignKey("Product")]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}