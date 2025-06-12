﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.UserProfile;
using Booking.DAL.Entities;
using Booking.DAL.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Booking.BAL.Service.Repo
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserTranslator _userTranslator;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly JwtHelper _jwtHelper;
        public UserProfileService(IUserProfileRepository userProfileRepository,
            IUserTranslator userTranslator,
            IPasswordHasher<string> passwordHasher,
            JwtHelper jwtHelper)
        {
            _userProfileRepository = userProfileRepository;
            _userTranslator = userTranslator;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }
        public async Task AddUser(UserProfileViewModel model)
        {            
            var getUserObj = _userTranslator.Translate(model);

            getUserObj.SecretKey = Guid.NewGuid();
            getUserObj.Password = _passwordHasher.HashPassword(model.SecretKey.ToString(), model.Password);

            await _userProfileRepository.AddAsync(getUserObj);
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _userProfileRepository.GetUserByEmail(request.Email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user.SecretKey.ToString(), user.Password, request.Password);
            if (result != PasswordVerificationResult.Success) return null;

            var claims = _userTranslator.TranslateClaims(user);

            return _jwtHelper.GenerateToken(claims);
        }
    }
}
