using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.FileCommand
{
    class FileCommand : CommandLine
    {
        public FileCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.FileCommand;
            Commands.AddRange(new List<CommandLineApplication>
            {
                new UploadFileCommand(container),
                new DownloadFileCommand(container),
                new MoveFileCommand(container),
                new RemoveFileCommand(container),
                new InfoFileCommand(container)
            });
        }

    }
}
