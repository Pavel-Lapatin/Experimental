using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    public class RedirectResult : ActionResult
    {
        public string Method { get; set; }
        public object[] Parameters { get; set; }
        public Type ControllerType { get; set; }


        public RedirectResult(Type type, string method, object[] parameters)
        {
            ControllerType = type;
            Method = method;
            Parameters = parameters;
        }
    }
}
