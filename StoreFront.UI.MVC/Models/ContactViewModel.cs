using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = " ** Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = " ** Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public string HardCodedSubject { get { return "Email Sent From StoreFront"; } }

        [UIHint("MultilineText")]
        [Required(ErrorMessage = " ** Message is required")]
        public string Message { get; set; }
    }
}