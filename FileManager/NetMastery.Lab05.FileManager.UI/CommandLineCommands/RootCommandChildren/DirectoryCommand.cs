using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DirectoryCommand : CommandLine
    {
        public DirectoryCommand(params CommandLine[] commands)
        {
            Name = CommandLineNames.DirectoryCommand;
            Commands.AddRange(commands);      
        }
    }
}
