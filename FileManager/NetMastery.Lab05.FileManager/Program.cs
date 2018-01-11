using System;
using Serilog;

namespace NetMastery.Lab05.FileManager
{
   
    class Program
    {
        static void Main()
        {
            try
            {
                CompositionRoot.CompRoot.Initialize().StartupUI();
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
