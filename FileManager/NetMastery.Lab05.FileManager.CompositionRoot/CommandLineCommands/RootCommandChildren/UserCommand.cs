using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.RootCommandChildren
{
    public class UserRootChildCommand : CommandLineApplication
    {
        public UserRootChildCommand(params CommandLineApplication[] commands)
        {

            Name = CommandLineNames.UserCommand;
            Commands.AddRange(commands);
            
        }
    }
}
