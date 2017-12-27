using Microsoft.Extensions.CommandLineUtils;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class ExitCommand : CommandLine
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
