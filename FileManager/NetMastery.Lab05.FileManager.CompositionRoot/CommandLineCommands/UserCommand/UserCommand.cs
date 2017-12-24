using Autofac;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand
{
    public class UserCommand : CommandLine
    {
        public UserCommand(IContainer container) : base(container)
        {

            Name = CommandLineNames.UserCommand;
            Commands.Add(new InfoUserCommand(container));
            
        }
    }
}
