using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;




namespace TurAfrikb.Models
{
    
    public class Product
    {

        [ScaffoldColumn(false)]

        public int Id { get; set; }
        
        public virtual Category category { get; set; }
        
        public String Name { get; set; }
        public String ModelOrSize { get; set; }
        
        
        public decimal Price { get; set; }
        
        public string ProdImageURL { get; set; }
        public String Brand { get; set; }
        public string Gender { get; set; }
        public string OtherDetails { get; set; }
        
        public virtual Store SellerShop { get; set; }
        public string Country { get; set; }
        public int Quantity { get; set; }
        public bool InPromotion { get; set; }
    }
}
