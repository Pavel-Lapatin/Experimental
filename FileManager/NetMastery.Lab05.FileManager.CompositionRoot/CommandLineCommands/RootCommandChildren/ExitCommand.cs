using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{
    public class ExitRootChildCommand : CommandLineApplication
    {
        public ExitRootChildCommand()
        {
            Name = CommandLineNames.ExitCommand;

            HelpOption(CommandLineNames.HelpOption);

            OnExecute(() =>
            {
                Environment.Exit(0);
                return 0;
            });
        }
    }
}
