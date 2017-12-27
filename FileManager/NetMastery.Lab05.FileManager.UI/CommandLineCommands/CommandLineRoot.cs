using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class CommandLineRoot : CommandLine
    {
        public CommandLineRoot(RedirectEvent redirectEvent, params CommandLine[] commands) : base(redirectEvent)
        {
            Name = "ConsoleArgs";
            Description = "FileInfo Manager";
            HelpOption(CommandLineNames.HelpOption);
            Commands.AddRange(commands);
        }
    }
}
