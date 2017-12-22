﻿using Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NetMastery.FileManeger.Bl.Interfaces;
using NetMastery.Lab05.FileManager.Dto;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.ViewModels;
using NetMastery.Lab05.FileManagerCompositionRoot;

namespace NetMastery.Lab05.FileManager.CompositionRoot.CommandLineCommands
{
    public class InfoDirectoryCommand : CommandLineCommand
    {
        public InfoDirectoryCommand(IContainer container) : base(container)
        {
            Name = CommandLineNames.UserCommand;

            Command(CommandLineNames.InfoCommand, c =>
            {
                var arguments = c.Argument("path", EnglishLocalisation.DirectoryCreateOptionNote, false);
                c.OnExecute(() =>
                {
                    using (var scope = _container.BeginLifetimeScope())
                    {
                        var userService = container.Resolve<DirectoryController>();
                        var writer = container.Resolve<IInfoWriter<DirectoryStructureDto>>();
                        writer.WriteInfo(userService.GetDirectoryByPath(arguments.Value));
                    }
                    return 0;
                });
            });
            
        }
    }
}
