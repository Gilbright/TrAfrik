using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurAfrikb.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }//  CONSİDERATİON OF THE AUTHENTİCATİON SERVİCES OF .NET MVC 5
        public string Address { get; set; }
        public User Manager { get; set; }
        public string Country { get; set; }
        public List<Product> StoreProductList { get; set; }

    }
}