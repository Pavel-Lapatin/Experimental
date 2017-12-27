using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UserCommand : CommandLine
    {
        public UserCommand(params CommandLine[] commands)
        {
            Name = CommandLineNames.UserCommand;
            Commands.AddRange(commands);   
        }
    }
}
