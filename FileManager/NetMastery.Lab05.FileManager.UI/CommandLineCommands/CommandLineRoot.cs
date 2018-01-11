using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Results;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class CommandLineApplicationRoot : CommandLineApplication
    {
        public CommandLineApplicationRoot(params CommandLineApplication[] commands)
        {
            Name = "ConsoleArgs";
            Description = "FileInfo Manager";
            HelpOption("-?|-h|--help");
            Commands.AddRange(commands);
        }
        public ActionResult Result { get; set; }
        
    }
}
