using System;

namespace NetMastery.Lab05.FileManager.Dto
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public DateTime CreationDate { get; set; }
        public long MaxStorageSize { get; set; }
        public long CurentStorageSize { get; set; }
        public string RootDirectory { get; set; }
    }
}
