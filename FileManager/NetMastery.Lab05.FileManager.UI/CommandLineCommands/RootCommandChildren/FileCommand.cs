using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class FileCommand : CommandLineApplication
    {
        public FileCommand(params CommandLineApplication[] commands)
        { 
            Name = "file";
            Description = "Command for interaction with files";
            Commands.AddRange(commands);
        }
    }
}
