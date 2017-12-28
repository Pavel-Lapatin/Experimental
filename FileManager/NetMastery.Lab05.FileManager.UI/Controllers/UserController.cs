using AutoMapper;
using NetMastery.Lab05.FileManager.Bl.Interfaces;
using NetMastery.Lab05.FileManager.UI.events;
using NetMastery.Lab05.FileManager.UI.ViewModels;
using System;

namespace NetMastery.Lab05.FileManager.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService, IUserContext context, RedirectEvent redirect) : base(context, redirect)
        {
            _userService = userService;
        }

        public void GetUserInfo()
        {
            if(IsAthenticated())
            {
                var model = Mapper.Instance.Map<InfoUserViewModel>(_userService.GetInfoByLogin(_userContext.Login));
                if (model == null) throw new NullReferenceException();
                model.RenderViewModel();
            } 
        }
    }
}
