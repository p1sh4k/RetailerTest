using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailerApi.Model
{
    public class GroupViewModel
    {
        public string GroupId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
