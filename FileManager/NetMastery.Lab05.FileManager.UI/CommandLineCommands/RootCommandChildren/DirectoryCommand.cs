using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DirectoryCommand : CommandLineApplication
    {

        public DirectoryCommand(params CommandLineApplication[] commands)
        { 
            Name = "directory";
            Commands.AddRange(commands);      
        }
    }
}
