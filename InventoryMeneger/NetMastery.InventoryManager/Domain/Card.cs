using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Card
    {
        public int CardId { get; set; }
        [Required]
        DateTime TimeOfRegistration{ get; set; }
        DateTime TimeOfDecommission { get; set; }
        [Required]
        public decimal FullCost { get; set; }
        [Required]
        public decimal WearCost { get; set; }
        [Required]
        public decimal ResidiualCost { get; set; }

    }
}
