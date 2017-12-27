using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class RedirectEventArgs : EventArgs
    {
        public Type ControllerType { get; set; }
        public string Method { get; set; }
        public object[] Parameters { get; set; }
    }
}