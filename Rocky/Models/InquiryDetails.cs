using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Models
{
    public class InquiryDetails
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Produtid { get; set; }
        public double price { get; set; }
        public string user { get; set; }
        public string status { get; set; }
        public DateTime Datetime { get; set;}
    }
}
