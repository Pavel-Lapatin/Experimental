using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.AuthenticateCommand;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.FileCommand;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand;
using System.Collections.Generic;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class CommandLineRoot : CommandLine
    {
        public CommandLineRoot(IContainer container) : base (container)
        {

            Name = "ConsoleArgs";
            Description = "FileInfo Manager";
            HelpOption(CommandLineNames.HelpOption);

            Commands.AddRange(new List<CommandLineApplication>
            {
                new LoginCommand(container),
                new LogoffCommand(container),
                new UserCommand(container),
                new DirectoryCommand(container),
                new FileCommand(container),
                new ExitCommand()
            });
        }
    }
}
