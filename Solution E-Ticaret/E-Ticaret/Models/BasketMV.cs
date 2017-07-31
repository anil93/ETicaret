using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class BasketMV
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int TotalRow
        {
            get{ return UnitPrice * Quantity; }          
        }

        public string ImagePath { get; set; }        
    }
}