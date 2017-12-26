using Autofac;
using Microsoft.Extensions.CommandLineUtils;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines
{
    public class CommandLine : CommandLineApplication
    {
        public IContainer _container;

        public CommandLine(IContainer container)
        {
            _container = container;          
        }
        
        
    }
}
