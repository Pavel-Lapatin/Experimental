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
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
                              IMapper mapper,
                              IUserContext context, RedirectEvent redirect) : base(context, redirect)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public void GetUserInfo()
        {
            if(IsAthenticated())
            {
                var model = _mapper.Map<InfoUserViewModel>(_userService.GetInfoByLogin(_userContext.Login));
                if (model == null) throw new ArgumentNullException();
                model.RenderViewModel();
            } 
        }
    }
}
