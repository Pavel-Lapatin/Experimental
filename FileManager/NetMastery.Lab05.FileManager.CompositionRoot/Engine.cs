//using Autofac;
//using NetMastery.Lab05.FileManager.UI.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NetMastery.Lab05.FileManager.CompositionRoot
//{
//    public class Engine
//    {
//        public Type Controller { get; set; }
//        public string Method { get; set; }
//        IContainer _container;
//        public Engine(IContainer container)
//        {
//            Controller = typeof(LoginController);
//            Method = "GetCommand";
//            _container = container;
//        }

//        public void Start()
//        {
//            var arguments = GetArguments();

//        }

//        public string GetArguments()
//        {
//            var obj = _container.Resolve(Controller);
//            var method = Controller.GetMethods().FirstOrDefault(t => t.Name == Method && t.ReturnType == typeof(String));
//            return (string) method.Invoke(obj, null);
//        }
//    }
//}
