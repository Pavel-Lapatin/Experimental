using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{

    public class LogoffCommand : CommandLineCommand
    {
        public LogoffCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.LogoffCommand;

            HelpOption(CommandLineNames.HelpOption);


            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<LoginController>().Signoff();
                }
                return 0;
            });
        }
    }
}
