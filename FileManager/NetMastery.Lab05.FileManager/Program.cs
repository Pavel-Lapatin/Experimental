using System;
using Serilog;
using NetMastery.Lab05.FileManager.Composition;

namespace NetMastery.Lab05.FileManager
{
    class Program
    {
        static void Main()
        {
            try
            {
                CompositionRoot.Initialize().StartupUI();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e.Message);
                Console.WriteLine("Fatal undefined exception. Sorry, call to the support +1111111111");
                Console.ReadKey();
            }
        }
    }
}
