using NetMastery.Lab05.FileManager.UI.Views;
using System;

namespace NetMastery.Lab05.FileManager.UI.ViewModels
{
    public class SigninGetView : View
    {
        public override void RenderView()
        {
            Console.WriteLine("Please, signin in the system");
            Console.WriteLine("Command: login -l <userName> -p <password>");
        }
    }
}
