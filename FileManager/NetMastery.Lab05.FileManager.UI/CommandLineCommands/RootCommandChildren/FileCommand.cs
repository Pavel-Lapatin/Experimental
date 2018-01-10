using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class FileCommand : CommandLineApplicationRoot
    {
        public FileCommand(params CommandLineApplication[] commands)
        { 
            Name = "file";
            HelpOption("-?|-h|--help");
            Commands.AddRange(commands);
        }
    }
}
