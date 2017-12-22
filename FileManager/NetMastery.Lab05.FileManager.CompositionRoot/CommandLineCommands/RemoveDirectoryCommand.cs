using Autofac;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;


namespace CompositionRoot.CommandLineCommands
{
    public class RemoveDirectoryCommand : CommandLineCommand
    {
        public RemoveDirectoryCommand(IContainer container, AppViewModel model) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;

            Command(CommandLineNames.MoveOption, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<DirectoryController>()
                        .Remove(arguments.Values[0],  model.CurrentPath);
                    }
                    return 0;
                });
            });
        }
    }
}