using Autofac;
using Microsoft.Extensions.CommandLineUtils;


namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public abstract class CommandLineCommand : CommandLineApplication
    {
        protected IContainer _container;

        protected CommandLineCommand(IContainer container)
        {
            _container = container;
        }
    }
}
