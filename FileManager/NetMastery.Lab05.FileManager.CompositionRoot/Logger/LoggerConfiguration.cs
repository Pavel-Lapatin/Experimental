using Serilog;


namespace NetMastery.Lab05.FileManager.CompositionRoot
{
    public class Logger
    {
        public static void LoggerConfig()
        {
            Log.Logger = new LoggerConfiguration()
                
                .MinimumLevel.Information()
                .WriteTo.File("log-.txt", 
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} " +
                                 "(processId: {ProcessId}) " +
                                 "(threadId: {ThreadId}) " +
                                 "message lvl: [{Level:u3}] " +
                                 "{NewLine}{Message:lj}")
                .MinimumLevel.Debug()
                .Enrich.With(new CommonLogEnricher())
                .WriteTo.File("log-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} | " +
                                 "processId: {ProcessId} | " +
                                 "threadId: {ThreadId} | " +
                                 "message lvl: [{Level:u3}] " +
                                 "{NewLine}stack trace: {StackTrace} " +
                                 "{NewLine}{Message:lj}{NewLine}{Exception}")
                .CreateLogger();            
        }
    }
}
    