using Microsoft.Extensions.CommandLineUtils;


namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class FileCommand : CommandLine
    {
        public FileCommand(params CommandLine[] commands)
        {
            Name = CommandLineNames.FileCommand;
            Commands.AddRange(commands);
        }

    }
}
