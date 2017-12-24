using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{
    class DirectoryCommand : CommandLine
    {
        public DirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;
            Commands.AddRange(new List<CommandLineApplication>
            {
                new AddDirectoryCommand(container),
                new ChangeWorkDirectoryCommand(container),
                new MoveDirectoryCommand(container),
                new RemoveDirectoryCommand(container),
                new InfoDirectoryCommand(container),
                new SearchDirectoryCommand(container),
                new ListDirectoryCommand(container)
            });      
        }
    }
}
