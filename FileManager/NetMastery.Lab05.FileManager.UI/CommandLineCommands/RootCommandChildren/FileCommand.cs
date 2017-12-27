using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class FileCommand : CommandLine
    {
        public FileCommand(RedirectEvent redirectEvent, params CommandLine[] commands) : base(redirectEvent)
        {
            Name = CommandLineNames.FileCommand;
            Commands.AddRange(commands);
        }

    }
}
