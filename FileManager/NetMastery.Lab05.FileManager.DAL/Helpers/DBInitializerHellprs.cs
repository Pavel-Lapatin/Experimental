using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.DAL.Helpers
{
    public delegate string HashPassword(string password);
    public static class DBInitializerHellprs
    {
        public static event HashPassword OnHashPassword;
        public static string GetHashPassword(string password)
        {
            return OnHashPassword.Invoke(password);

        }
    }
}
