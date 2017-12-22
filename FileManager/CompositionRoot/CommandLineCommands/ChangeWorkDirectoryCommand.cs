using Autofac;
using NetMastery.Lab05.FileManager;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controllers;

namespace CompositionRoot.CommandLineCommands
{
    public class ChangeWorkDirectoryCommand : CommandLineCommand
    {
        public ChangeWorkDirectoryCommand(IContainer container, AppViewModel model) : base(container)
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
                        .ChangeWorkingDirectory(arguments.Values[0], model.CurrentPath);
                    }
                    return 0;
                });
            });
        }
    }
}