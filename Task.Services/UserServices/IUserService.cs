using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.ViewModels.Shared;
using Task.ViewModels.Users;

namespace Task.Services.UserServices
{
    public interface IUserService
    {
        ResultViewModel GetAll();
        ResultViewModel GetUserDetails(int UserId);
        ResultViewModel CreateUser(ManageUserViewModel model);
        ResultViewModel UpdateUser(ManageUserViewModel model);
        ResultViewModel DeleteUser(int UserId);
    }
}
