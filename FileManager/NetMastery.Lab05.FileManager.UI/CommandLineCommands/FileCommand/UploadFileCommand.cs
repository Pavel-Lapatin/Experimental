﻿using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.events;
using System;

namespace NetMastery.Lab05.FileManager.UI.Commands
{
    public class UploadFileCommand : CommandLine
    {
        public Func<FileController> Controller;

        public UploadFileCommand(Func<FileController> getController, RedirectEvent redirectEvent) : base(redirectEvent)
        {
            Controller = getController;
            Name = CommandLineNames.UploadCommand;

            var arguments = Argument("path", "Paths", true);
            OnExecute(() =>
            {
                Controller()
                .Upload(arguments.Values[arguments.Values.Count - 2], arguments.Values[arguments.Values.Count - 1]);
                return 0;
            });
        }
    }
}
