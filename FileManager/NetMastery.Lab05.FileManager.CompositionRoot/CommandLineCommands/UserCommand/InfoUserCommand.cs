using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.UserCommand
{
    class InfoUserCommand : CommandLine
    {
        public InfoUserCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.InfoCommand;
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<UserController>().GetUserInfo(); ;
                }
                return 0;
            });
        }
       
    }
}
