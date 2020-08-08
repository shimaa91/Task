using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.ViewModels.IdentityUserViewModel;
using Task.ViewModels.Shared;

namespace Task.Services.IdentityUserServices
{
    public interface IIdentityUserService
    {
        Task<ResultViewModel> RegisterUserAsync(RegisterViewModel model);
        Task<ResultViewModel> LoginrUserAsync(LoginViewModel model);
    }
}
