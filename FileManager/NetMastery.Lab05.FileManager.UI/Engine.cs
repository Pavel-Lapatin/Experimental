using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Forms;
using Serilog;
using System;
using System.Linq;


namespace NetMastery.Lab05.FileManager.UI
{
    public class Engine
    {
        Type _controller = typeof(StartController);
        string _method = "Start";
        object[] _parameters = new object[] { new StartForm("~\\") };

        public Func<Type, Controller> _getController;
        CommandLineApplication _cmd;

        public Engine(Func<Type, Controller> getController, CommandLineApplication cmd)
        {
            _getController = getController;
            _cmd = cmd;
        }
        public void StartupUI()
        {
            Log.Logger.Information("UI Successfully initialized. StartupUI begins ...");
            while(true)
            {
                var arguments = ExecuteControllerMethod();
                if(arguments != null)
                {
                    var args = UIHelpers.ParseArguemts(arguments);
                    try
                    {
                        _cmd.Execute(args);
                    }
                    catch(CommandParsingException e)
                    {
                        Log.Logger.Information($"Wrong command input: {arguments}");
                        e.Command.ShowHelp();
                        Console.WriteLine();
                        Redirect(typeof(CommandController), "CommandGet", null);
                    }
                    catch (ServiceArgumentException e)
                    {
                        Log.Logger.Debug(e.Message);
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                        Redirect(typeof(CommandController), "CommandGet", null);
                    }
                }
            }
        }

        public string ExecuteControllerMethod()
        {
            var method = _parameters == null 
                ? _controller.GetMethod(_method) 
                : _controller.GetMethod(_method, _parameters.Select(x=>x.GetType()).ToArray());
            if(method != null)
            {
                var Controller = _getController(_controller);
                var parametrsType = _parameters?.Select(x => x.GetType()).ToArray();
                return method.Invoke(Controller, _parameters) as string;
            }
            else
            {
                Redirect(typeof(CommandController), "GetCommand", null);
                return null;
            }
                  
        }

        private void Redirect(Type controller, string method, object[] parameters)
        {
            _controller = controller;
            _method = method;
            _parameters = parameters;
        }

        public void RedirecteRedirectEventHandler(object sender, RedirectEventArgs e)
        {
            Redirect(e.ControllerType, e.Method, e.Parameters);
        }

    }
}
