using System;
namespace NetMastery.Lab05.FileManager.Bl.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
