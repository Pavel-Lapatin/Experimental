using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class ExitCommand : CommandLineApplication
    {
        public ExitCommand()
        {
            Name = "exit";
            HelpOption("-?|-h|--help");
            OnExecute(() =>
            {
                Environment.Exit(0);
                return 0;
            });
        }
    }
}
