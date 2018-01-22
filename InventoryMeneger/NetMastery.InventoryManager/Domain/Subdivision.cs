using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Subdivision
    {
        public int SubdivisionId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<PersonInCharge> Persons { get; set; }
    }
}
