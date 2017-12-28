using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineApplicationCommands.Interface
{
    public interface ICommandLineApplication
    {
       void Execute(params string[] args);
    }
}
