﻿using Autofac;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLines.DirectoryCommand
{

    public class ListDirectoryCommand : CommandLine
    {
        public ListDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.ListCommand;
            var arguments = Argument("path", EnglishLocalisation.DirectoryMoveOptionNote, false);
            OnExecute(() =>
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    container.Resolve<DirectoryController>()
                    .List(arguments.Values[arguments.Values.Count-1]);
                }
                arguments.Values.Clear();
                return 0;
            });
        }
    }
}
