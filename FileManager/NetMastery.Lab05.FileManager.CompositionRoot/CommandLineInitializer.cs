using Autofac;
using CompositionRoot.CommandLineCommands;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.AuthenticateCommand;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.DirectoryCommands;

namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class CommandLineInitializer
    {
        public static CommandLineApplication Initialize(CommandLineApplication cmd, IContainer container)
        {
            cmd.Name = "ConsoleArgs";
            cmd.Description = "FileInfo Manager";
            cmd.HelpOption(CommandLineNames.HelpOption);
   
            cmd.Commands.Add(new LoginCommand(container));
            cmd.Commands.Add(new UserInfoCommand(container));
            cmd.Commands.Add(new DirectoryCommand(container));
            cmd.Commands.Add(new LogoffCommand(container));
            cmd.Commands.Add(new ExitCommand());
            return cmd;
        }
    }
}
