using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky.Models
{
    public class InquiryCart
    {
        public int Id { get; set; }

        [Required]
        public int Produtid { get; set; }

        public string User { get; set; }
        public DateTime Datetime { get; set; }
       
    }
}
