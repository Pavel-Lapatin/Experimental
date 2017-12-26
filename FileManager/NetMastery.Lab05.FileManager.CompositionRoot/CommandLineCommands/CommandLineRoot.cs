using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;

using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand;

using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class CommandLineRoot : CommandLineApplication
    {
        public CommandLineRoot(params CommandLineApplication[] commands)
        {
            Name = "ConsoleArgs";
            Description = "FileInfo Manager";
            HelpOption(CommandLineNames.HelpOption);
            Commands.AddRange(commands);
        }
    }
}
