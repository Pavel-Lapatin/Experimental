﻿using NetMastery.FileManeger.Bl.Interfaces;
using System;

namespace NetMastery.Lab05.FileManager.BLModel.DtoClasses
{
    public class UserController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public UserController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        public string Singin(string login, string password)
        {
            if(login == null || password == null)
            {
                throw new NullReferenceException();
            }

            return _authenticationService.Signin(login, password).Login;
        }

        public void Singup(string login, string password)
        {
            if (login == null || password == null)
            {
                throw new NullReferenceException();
            }
            _authenticationService.Singup(login, password);
        }

      public UserInfo GetUserInfo(string login)
        {
            if (login == null)
                throw new NullReferenceException("user hasn't been registred yet");
            var userInfo = _userService.GetInfoByLogin(login);
            if (userInfo == null)
                throw new NullReferenceException($"There is no user with login: {login}");
            return userInfo;
        }
    }
}
