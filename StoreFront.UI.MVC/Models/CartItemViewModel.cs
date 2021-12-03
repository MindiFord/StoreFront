using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF; // Added for access to the Domain/Entity models
using System.ComponentModel.DataAnnotations; // Added for validation/display of metadata

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1, byte.MaxValue, ErrorMessage = " * Quantity must be between 1 and 255")]
        public int Qty { get; set; }

        public Product Product { get; set; }

        // FQ ctor
        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}