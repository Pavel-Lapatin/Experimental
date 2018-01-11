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


namespace NetMastery.Lab05.FileManager.UI
{
    public class Engine
    {
        IUserContext _userContext { get; set; }
        CommandLineApplicationRoot _cmd;
        IResultProvider _resultProvider;

        public Engine(IResultProvider resultProvider, IUserContext userContext, CommandLineApplicationRoot cmd)
        {
            _userContext = userContext;
            _cmd = cmd;
            _resultProvider = resultProvider;
        }
        public void StartupUI()
        {
            Log.Logger.Information("UI Successfully initialized. StartupUI begins ...");
            while(true)
            { 
                try
                {
                    if(_resultProvider.Result is ViewResult)
                    {
                        (_resultProvider.Result as ViewResult).ViewModel.RenderViewModel();
                        _resultProvider.Result = null;
                    }
                    else if(_resultProvider.Result is RedirectResult)
                    {
                        _resultProvider.Result = (_resultProvider.Result as RedirectResult).Execute();
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
                    Log.Logger.Information($"Wrong command input");
                    e.Command.ShowHelp();
                    foreach (var options in e.Command.Options)
                    {
                        options.Values.Clear();
                    }
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
