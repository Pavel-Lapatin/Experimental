﻿
namespace NetMastery.Lab05.FileManager.UI
{
    public interface IUserContext
    {
        int UserId { get; set; }
        string Login { get; set; }
        string CurrentPath { get; set; }
        string RootDirectory { get; set; }
        bool IsAuthenticated { get; }
        string Role { get; set; }
        void Clear();
    }
}
