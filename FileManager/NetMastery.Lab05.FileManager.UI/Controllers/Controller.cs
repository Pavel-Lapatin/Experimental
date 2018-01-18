using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{   
    public abstract class Controller : IDisposable
    {
        protected readonly IUserContext _userContext;
        
        protected bool disposed = false;
        protected Controller(IUserContext userContext)
        {
            _userContext = userContext;
        }

        protected abstract void Dispose(bool disposing);
        
        public void Dispose()
        {
            Dispose(true);
                
            GC.SuppressFinalize(this);
        }

        public string GetCurrentPath()
        {
            if (_userContext.CurrentPath != null)
            {
                return _userContext.CurrentPath;
            }
            throw new ArgumentNullException();
        }
    }
}
