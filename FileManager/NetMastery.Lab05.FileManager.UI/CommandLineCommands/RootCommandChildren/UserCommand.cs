﻿using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UserCommand : CommandLineApplication
    {
        public UserCommand(params CommandLineApplication[] commands) 
        {
            Name = "user";
            Commands.AddRange(commands);
            Description = "Command for interaction with users";
        }
    }
}
