namespace NetMastery.Lab05.FileManager.UnitTests.UITests.CommandLineTests
{
    public static class CommandLineArguments
    {
        public static object[] Empty =
        {
            new string[] { }
        };
        public static object[] OneArg =
        {
            new string[] { "arg1" }
        };
        public static object[] TwoArgs =
        {
            new string[] { "arg1", "arg2" }
        };
        public static object[] ThreeArgs =
        {
            new string[] { "arg1", "arg2", "arg3" }
        };
        public static object[] LoginRightCmd =
        {
            new string[] { "-l", "admin", "-p", "admin" }
        };
        public static object[] LoginWrongCmd =
        {
            new string[] { },
            new string[] { "-l" },
            new string[] { "-p" },
            new string[] { "-l", "admin" },
            new string[] { "-p", "admin" },
            new string[] { "-l", "-p", "admin" },
            new string[] { "-l", "admin", "-p" },
            new string[] { "arg1", "arg2", "arg3", "arg4" },
            new string[] { "-l", "admin", "-p", "admin", "arg1" },
        };
        
    }
}
