using NetMastery.Lab05.FileManager.UI.Interfaces;
using System;


namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class ExitCommand : CommandLineApplicationRoot
    {
        public ExitCommand()
        {
            Name = "exit";
            Description = "Exit from the application";
            OnExecute(() =>
            {
                Environment.Exit(0);
                return 0;
            });
        }
    }
}
