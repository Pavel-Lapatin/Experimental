using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class DirectoryCommand : CommandLine
    {
        public DirectoryCommand(RedirectEvent redirectEvent, params CommandLine[] commands) : base(redirectEvent)
        {
            Name = CommandLineNames.DirectoryCommand;
            Commands.AddRange(commands);      
        }
    }
}
