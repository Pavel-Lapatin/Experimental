
namespace NetMastery.InventoryManager.Bl.DtoEntities
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string RoleName { get; set; }
    }
}
