using NetMastery.Lab05.FileManager.BL.Dto;
using NetMastery.Lab05.FileManeger.Bl.Dto;

namespace NetMastery.Lab05.FileManager.BL
{
    public class FileManagerModel
    {
        public AccountDto Account { get; set; }
        public DirectoryInfoDto DirectoryInfo { get; set; }
        public string Path { get; set; }
        //public byte Rights { get; set; }

    }
}
