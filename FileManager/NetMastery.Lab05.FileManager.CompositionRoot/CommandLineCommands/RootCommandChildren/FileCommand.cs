using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{
    class FileRootChildCommand : CommandLineApplication
    {
        public FileRootChildCommand(params CommandLineApplication[] commands)
        {
            Name = CommandLineNames.FileCommand;
            Commands.AddRange(commands);
        }

    }
}
