﻿namespace NetMastery.InventoryManager.Domain
{
    public class InventoryInCard
    {
        public virtual int CardId { get; set; }
        public virtual Card Card { get; set; }
        public virtual int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
        public int Quantity { get; set; }
    }
}