using NetMastery.Lab05.FileManager.UI.Controllers;
using System;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI.Results
{
    public class RedirectResult : ActionResult
    {
        private string _method { get; set; }
        private object[] _parameters { get; set; }
        private Type _controllerType;
        private Controller _controller;

        public RedirectResult(Type type, string method, object[] parameters)
        {
            _controllerType = type;
            _method = method;
            _parameters = parameters;
            
        }
        public void SetController(Controller controller)
        {
            _controller = controller;
        }
        public ActionResult Execute()
        {
            if (_controllerType == null)
            {
                return null;
            }
            var method = _parameters == null
                ? _controllerType.GetMethod(_method)
                : _controllerType.GetMethod(_method, _parameters.Select(x => x.GetType()).ToArray());
            if (method != null)
            {
                return method.Invoke(_controller, _parameters) as ActionResult;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
