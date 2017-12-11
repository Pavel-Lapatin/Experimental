using System.ComponentModel;
using NetMastery.FileManeger.Controller;
using NetMastery.Lab05.FileManager.DAL.Entities;
using NetMastery.Lab05.FileManager.BL;
using NetMastery.Lab05.FileManager.Repository.Repository;

namespace NetMastery.FileManeger.ConsoleApp
{
   
    class Program
    {
        internal static AccountController AccountController;

        public Program()
        {
            InitializeControllers();
        }

        static void Main(string[] args)
        {

        }

        private static void InitializeControllers()
        {
            AccountController = new AccountController(new AccountRepository());
        }
    }
}
