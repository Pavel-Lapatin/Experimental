﻿

using System;

namespace NetMastery.Lab05.FileManager.UI.Implementation
{
    public class UserContext : IUserContext
    {
        public int UserId { get ; set ; }
        public string Login { get; set ; }
        public string CurrentPath { get ; set; }
        public string RootDirectory { get; set; }
        public bool IsAuthenticated => Login != null;
        public string Role { get ; set ; }

        public void Clear()
        {
            UserId = 0;
            Login = null;
            CurrentPath = null;
            Role = null;
            RootDirectory = null;
        } 
    }
}