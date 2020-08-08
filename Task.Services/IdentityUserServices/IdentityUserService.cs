using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Task.ViewModels.IdentityUserViewModel;
using Task.ViewModels.Shared;

namespace Task.Services.IdentityUserServices
{
    public  class IdentityUserService:IIdentityUserService
    {
        private UserManager<IdentityUser> _UserManager;
        private IConfiguration _configuration;

        public IdentityUserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _UserManager = userManager;
            _configuration = configuration;
        }
        public async Task<ResultViewModel> LoginrUserAsync(LoginViewModel model)
        {
            var user = await _UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new ResultViewModel
                {
                    Message = "لا   يوجد    مستخدم  بهذا    البريد  اللإلكترونى.",
                    IsSuccess = false
                };
            }
            var result = await _UserManager.CheckPasswordAsync(user, model.Password);

            if (!result)
            {
                return new ResultViewModel
                {
                    Message = "كلمة السر    غير صحيحة.",
                    IsSuccess = false
                };
            }

            var claims = new[]{
                new Claim("Email",model.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResultViewModel { Message = "Logged  Successfully!   ", Data = tokenString, IsSuccess = true };

        }

        public async Task<ResultViewModel> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new NotImplementedException("Register Model    is  Null");
            }
            if (model.Password != model.ConfirmPassword)
                return new ResultViewModel
                {
                    Message = "Confirm   password    dosen't match   password",
                    IsSuccess = false
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var Result = await _UserManager.CreateAsync(identityUser, model.Password);

            if (Result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "User created Successfully ",
                    IsSuccess = true
                };
            }

            return new ResultViewModel
            {
                Message = "User   didnot  create",
                IsSuccess = false,
                Errors = Result.Errors.Select(e => e.Description)
            };
        }
    }
}
