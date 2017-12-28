using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.events;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UserCommand : CommandLineApplication
    {
        public UserCommand(params CommandLineApplication[] commands) 
        {
            Name = "user";
            Commands.AddRange(commands);   
        }
    }
}
