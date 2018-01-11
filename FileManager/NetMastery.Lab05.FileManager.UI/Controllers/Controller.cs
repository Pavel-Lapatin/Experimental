using NetMastery.Lab05.FileManager.UI.Results;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{   
    public class Controller
    {
        protected readonly IUserContext _userContext;
        protected readonly Func<Type, string, object[], RedirectResult> _redirect;
        protected readonly Func<ViewModel, ViewResult> _viewResult;

        protected Controller(IUserContext userContext,
                          Func<Type, string, object[], RedirectResult> redirect,
                          Func<ViewModel, ViewResult> viewResult)
        {
            _userContext = userContext;
            _redirect = redirect;
            _viewResult = viewResult;
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
