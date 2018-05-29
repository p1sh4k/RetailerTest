using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailerApi.Model
{
    public class RetailViewModel
    {
        [Required]
        public string Name { get; set; }

        public string GroupId { get; set; }

        public string RetailId { get; set; }
    }
}
