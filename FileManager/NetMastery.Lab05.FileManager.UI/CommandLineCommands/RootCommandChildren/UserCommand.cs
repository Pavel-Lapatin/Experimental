using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UserCommand : CommandLine
    {
        public UserCommand(RedirectEvent redirectEvent, params CommandLine[] commands) : base(redirectEvent)
        {
            Name = CommandLineNames.UserCommand;
            Commands.AddRange(commands);   
        }
    }
}
