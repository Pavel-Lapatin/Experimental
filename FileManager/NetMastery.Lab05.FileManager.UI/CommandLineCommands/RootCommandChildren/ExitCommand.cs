using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class ExitCommand : CommandLine
    {
        public ExitCommand(RedirectEvent redirectEvent) : base(redirectEvent)
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
