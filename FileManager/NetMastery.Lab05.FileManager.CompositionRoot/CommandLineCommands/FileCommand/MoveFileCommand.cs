using Autofac;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    class MoveFileCommand : CommandLine
    {
        public MoveFileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.InfoCommand;

            var arguments = Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var fileService = container.Resolve<FileController>();
                }
                return 0;
            });
        }
    }
}
