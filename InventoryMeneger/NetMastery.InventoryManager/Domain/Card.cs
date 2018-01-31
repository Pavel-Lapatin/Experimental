﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetMastery.InventoryManager.Domain
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
        public ICollection<Inventory> Inventories { get; set; }
    }
}
