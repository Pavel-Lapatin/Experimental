//using System.Linq;
//using NetMastery.Lab05.FileManager.DAL.Entities;
//using NetMastery.Lab05.FileManager.DAL.Interfacies;

//namespace NetMastery.Lab05.FileManager.DAL.Repository
//{
    
//    public class AccountRepository : Repository<Account>, IAccountRepository
//    {
        
//        public AccountRepository(FileManagerDbContext context) : base(context)
//        {
//        }

//        public string GetPasswordByLogin(string login)
//        {
//            return ((FileManagerDbContext)Context).Accounts.FirstOrDefault(x => x.Login == login)?.Password;
//        }
//    }
//}
