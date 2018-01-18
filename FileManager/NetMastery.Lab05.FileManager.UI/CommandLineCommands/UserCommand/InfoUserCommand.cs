using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.Interfaces;

namespace NetMastery.Lab05.FileManager.UI.Commands
{ 
    public class InfoUserCommand : CommandLineApplication
    {
        public Func<UserController> Controller;
        IResultProvider _resultProvider;
        public InfoUserCommand(Func<UserController> getController, IResultProvider resultProvider)
        {
            _resultProvider = resultProvider;
            Controller = getController;
            Name = "info";
            Description = "info about user signed in the system";
            OnExecute(() =>
            {
                using (var controller = Controller())
                {
                    _resultProvider.Result = controller.GetUserInfo();
                }
                return 0;
            });
        }
    }
}
