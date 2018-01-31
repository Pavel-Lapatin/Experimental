using System.ComponentModel.DataAnnotations;


namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int SubdivisionId { get; set; }
    }
}
