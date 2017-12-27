using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands.Interface
{
    public interface ICommandLine
    {
       void Execute(params string[] args);
    }
}
