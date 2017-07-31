using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class BasketAddressMV
    {
        public BasketAddressMV()
        {
            BasketMVList = new List<BasketMV>();

            Addresses = new List<Address>();
        }

        public List<BasketMV> BasketMVList { get; set; }

        public List<Address> Addresses { get; set; }
        
        public int TotalPrice { get; set; }
    }
}