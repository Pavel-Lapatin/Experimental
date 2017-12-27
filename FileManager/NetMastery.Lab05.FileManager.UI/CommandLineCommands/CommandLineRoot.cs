using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class CommandLineRoot : CommandLine
    {
        public CommandLineRoot(params CommandLine[] commands)
        {
            Name = "ConsoleArgs";
            Description = "FileInfo Manager";
            HelpOption(CommandLineNames.HelpOption);
            Commands.AddRange(commands);
        }
    }
}
