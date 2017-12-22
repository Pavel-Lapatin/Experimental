using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;
using UI.Controllers;

namespace CompositionRoot.CommandLineCommands
{
    public class AddDirectoryCommand : CommandLineCommand
    {
        public AddDirectoryCommand(IContainer container, AppViewModel model) : base(container)
        {
            Name = CommandLineNames.DirectoryCommand;

            Command(CommandLineNames.CreateOption, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, true);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        container.Resolve<DirectoryController>()
                        .Add(arguments.Values[0], arguments.Values[1], model.CurrentPath);
                    }
                    return 0;
                });
            }); 
        }
    }
}
