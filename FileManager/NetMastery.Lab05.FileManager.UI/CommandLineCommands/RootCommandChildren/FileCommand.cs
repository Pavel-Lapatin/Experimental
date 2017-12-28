using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class FileCommand : CommandLineApplication
    {
        public FileCommand(params CommandLineApplication[] commands)
        { 
            Name = "file";
            Commands.AddRange(commands);
        }

    }
}
