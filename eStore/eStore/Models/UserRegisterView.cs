using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Models
{
    public class UserRegisterView
    {
        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Street")]
        public string ShippingAddressStreet { get; set; }

        [Display(Name = "Post Code")]
        public string ShippingAddressPostCode { get; set; }

        [Display(Name = "City")]
        public string ShippingAddressCity { get; set; }

        [Display(Name = "Country")]
        public string ShippingAddressCountry { get; set; }

         [Display(Name = "Street")]
        public string BillingAddressStreet { get; set; }

         [Display(Name = "Post Code")]
        public string BillingAddressPostCode { get; set; }

         [Display(Name = "City")]
        public string BillingAddressCity { get; set; }

         [Display(Name = "Country")]
        public string BillingAddressCountry { get; set; }

    }
}
