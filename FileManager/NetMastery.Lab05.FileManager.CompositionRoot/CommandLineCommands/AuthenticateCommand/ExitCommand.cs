using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.AuthenticateCommand
{
    public class ExitCommand : CommandLineApplication
    {
        public ExitCommand()
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
