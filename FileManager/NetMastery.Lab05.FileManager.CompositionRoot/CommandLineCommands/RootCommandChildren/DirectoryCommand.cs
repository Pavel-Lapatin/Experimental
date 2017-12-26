using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{
    public class DirectoryRootChildCommand : CommandLineApplication
    {
        public DirectoryRootChildCommand(params CommandLineApplication[] commands)
        {
            Name = CommandLineNames.DirectoryCommand;
            Commands.AddRange(commands);      
        }
    }
}
