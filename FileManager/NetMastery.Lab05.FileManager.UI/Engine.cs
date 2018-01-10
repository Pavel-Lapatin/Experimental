using Microsoft.Extensions.CommandLineUtils;
using NetMastery.Lab05.FileManager.Bl.Exceptions;
using NetMastery.Lab05.FileManager.Helpers;
using NetMastery.Lab05.FileManager.UI.Commands;
using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using Serilog;
using System;
using System.Data;


namespace NetMastery.Lab05.FileManager.UI
{
    public class Engine
    {
        ActionResult _result { get; set; }
        IUserContext _userContext { get; set; }
        CommandLineApplicationRoot _cmd;

        public Engine(ActionResult result, IUserContext userContext, CommandLineApplicationRoot cmd)
        {
            _userContext = userContext;
            _result = result;
            _cmd = cmd;
        }
        public void StartupUI()
        {
            Log.Logger.Information("UI Successfully initialized. StartupUI begins ...");
            while(true)
            { 
                try
                {
                    if(_result is ViewResult)
                    {
                        (_result as ViewResult).ViewModel.RenderViewModel();
                    }
                    else if(_result is RedirectResult)
                    {
                        _result = (_result as RedirectResult).Execute();
                    }
                    else
                    {
                        Console.Write(_userContext.CurrentPath + "-->");
                        var arguments = UIHelpers.ParseArguemts(Console.ReadLine());
                        _cmd.Execute(arguments);
                        _result = _cmd.Result;
                    }
                }
                catch (CommandParsingException e)
                {
                    Log.Logger.Information($"Wrong command input");
                    e.Command.ShowHelp();
                    Console.WriteLine();
                }
                catch (ServiceArgumentException e)
                {
                    Log.Logger.Debug(e.Message);
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
                catch (ArgumentException e)
                {
                    Log.Logger.Debug(e.Message);
                    Console.WriteLine();
                    Console.WriteLine("Operation is failed");
                    Console.WriteLine();
                }
                catch (DataException e)
                {
                    Log.Logger.Debug(e.Message);
                    Console.WriteLine();
                    Console.WriteLine("Operation is failed");
                    Console.WriteLine();
                }
            }
        }   
    }
}
