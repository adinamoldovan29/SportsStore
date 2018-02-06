﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "Name")]
        public string NameOfCustomer { get; set; }

        [Required(ErrorMessage = "Please enter at least the first line of address")]
        [Display(Name = "Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Line 3")]
        public string AddressLine3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string  City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter the country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
