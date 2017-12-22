using Autofac;
using CompositionRoot.CommandLineCommands;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;
using System.Resources;


namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public static class CommandLineInitializer
    {
        public static void AddCommands(CommandLineApplication cmd, IContainer container, AppViewModel model)
        {
            var rm = new ResourceManager(typeof(EnglishLocalisation));
            cmd.Name = "ConsoleArgs";
            cmd.Description = "FileInfo Manager";
            cmd.HelpOption(CommandLineNames.HelpOption);

            #region login command

            cmd.Commands.Add(new LoginCommand(container, model));
            cmd.Commands.Add(new UserInfoCommand(container, model));
            cmd.Commands.Add(new AddDirectoryCommand(container, model));
            cmd.Commands.Add(new MoveDirectoryCommand(container, model));
            cmd.Commands.Add(new RemoveDirectoryCommand(container, model));
            cmd.Commands.Add(new SearchDirectoryCommand(container, model));
            cmd.Commands.Add(new ChangeWorkDirectoryCommand(container, model));

            #endregion
        }
    }
}
