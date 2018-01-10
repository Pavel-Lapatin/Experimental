using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    public class RedirectResult : ActionResult
    {
        string _method { get; set; }
        object[] _parameters { get; set; }
        IControllerFactory factory;

        public RedirectResult(Func<Type, Controller> getController)
        {
            _getController = getController;
        }

        public void SetActionMethod(string method, object[] parameters)
        {
            if(method == null)
            {
                throw new ArgumentNullException();
            }
            _method = method;
            _parameters = parameters;
        }

        public ActionResult Execute()
        {
            var method = _parameters == null
                ? _controller.GetMethod(_method)
                : _controller.GetMethod(_method, _parameters.Select(x => x.GetType()).ToArray());
            if (method != null)
            {
                var Controller = Activator.CreateInstance(_controller);
                return method.Invoke(Controller, _parameters) as ActionResult;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
