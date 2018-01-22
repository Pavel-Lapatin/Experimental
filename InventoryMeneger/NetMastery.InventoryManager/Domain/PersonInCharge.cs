using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PersonInCharge
    {
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        
        public virtual int SubdivisionId { get; set; }
        public Subdivision Subdivision { get; set; }

        public ICollection<Storage> Storages;
    }
}
