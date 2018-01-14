using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DirectoryCommand : CommandLineApplication
    {
        public DirectoryCommand(params CommandLineApplication[] commands)
        { 
            Name = "directory";
            Description = "Command for interction with directories";
            Commands.AddRange(commands);      
        }
    }
}
