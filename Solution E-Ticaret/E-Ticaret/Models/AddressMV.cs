using E_Ticaret.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class AddressMV
    {
        public Address Address { get; set; }

        public List<Address> Addresses { get; set; }
    }
}