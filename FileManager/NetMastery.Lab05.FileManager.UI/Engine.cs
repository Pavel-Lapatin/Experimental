using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Controllers;
using NetMastery.Lab05.FileManager.UI.Interfaces;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using Serilog;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace NetMastery.Lab05.FileManager.UI
{
    public class Engine
    {
        private readonly IUserContext _userContext;
        private readonly CommandLineApplicationRoot _cmd;
        private readonly IResultProvider _resultProvider;
        private readonly Func<Type, Controller> _controller;

        public Engine(IResultProvider resultProvider, 
                      IUserContext userContext, 
                      CommandLineApplicationRoot cmd,
                      Func<Type, Controller> controller)
        {
            _userContext = userContext;
            _cmd = cmd;
            _resultProvider = resultProvider;
            _controller = controller;
            _resultProvider.Result = new RedirectResult(
                typeof(StartController),
                nameof(StartController.Start),
                null);
        }

        public void StartupUI()
        {
            Log.Logger.Information("UI Successfully initialized. StartupUI begins ...");
            while (true)
            {
                try
                {
                    if (_resultProvider.Result is ViewResult)
                    {
                        (_resultProvider.Result as ViewResult).View.RenderView();
                        _resultProvider.Result = null;
                    }
                    else if (_resultProvider.Result is RedirectResult)
                    {

                        _resultProvider.Result = Redirect(_resultProvider.Result as RedirectResult);
                    }
                    else
                    {
                        Console.Write(_userContext.CurrentPath + "-->");
                        var arguments = UIHelpers.ParseArguemts(Console.ReadLine());
                        _cmd.Execute(arguments);
                    }
                }
                catch (CommandParsingException e)
                {
                    e.Command.ShowHelp();
                    ClearCmd(e.Command);
                }
                catch (BusinessException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (UIException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        private ActionResult Redirect(RedirectResult result)
        {
            if (result.ControllerType == null)
            {
                Log.Logger.Debug("Execute controller type is null");
                throw new ArgumentNullException();
            }
            var controller = _controller(result.ControllerType);
            var executedMethod = result.Parameters == null
                ? result.ControllerType.GetMethod(result.Method)
                : result.ControllerType.GetMethod(result.Method, result.Parameters
                                                  .Select(x => x.GetType()).ToArray());

            if (executedMethod == null)
            {
                Log.Logger.Debug("Execute controller method is null");
                throw new ArgumentNullException();
            }
            return executedMethod.Invoke(controller, result.Parameters) as ActionResult;
        }
        private void ClearCmd(CommandLineApplication e)
        {
            foreach (var argument in e.Arguments)
            {
                argument.Values.Clear();
            }
            foreach (var option in e.Options)
            {
                option.Values.Clear();
            }
        }
    }
}
