using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data.DataContext;
using Task.Data.Enums;
using Task.Data.Models;
using Task.Repositories.UnitOfWork;
using Task.Repositories.UserRepositories;
using Task.ViewModels.Shared;
using Task.ViewModels.Users;

namespace Task.Services.UserServices
{
    //public interface IUserService
    //{
    //    ResultViewModel CreateUser();
    //}
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        // private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            //_mapper = mapper;
        }
        public ResultViewModel CreateUser(ManageUserViewModel model)
        {
            if (model != null)
            {
                //       var user = _mapper.Map<User>(model);
                var user = new Data.Models.User { FullName = model.FullName, Birthdate = model.Birthdate, Gender = (Gender)model.Gender };
                _userRepository.Add(user);
                //new Data.Models.User { FullName = model.FullName, Birthdate = model.Birthdate, Gender = model.Gender });
                if (user != null)
                {
                    int save = _unitOfWork.Commit();
                    if (save == 0)
                        return new ResultViewModel { IsSuccess = false, Message = "Error" };
                    return new ResultViewModel { IsSuccess = true, Message = "User Added   Successfully." };
                }
                return new ResultViewModel { IsSuccess = false, Message = "" };

            }
            return new ResultViewModel { IsSuccess = false, Message = "Some Properties  is  invalid." };
        }

        public ResultViewModel DeleteUser(int UserId)
        {
            var user = _userRepository.GetAll(u => u.UserID == UserId).FirstOrDefault();
            if (user != null)
            {
                _userRepository.Remove(user);
                _unitOfWork.Commit();
                return new ResultViewModel { IsSuccess = true, Message = "User  deleted Successfully" };
            }
            return new ResultViewModel { IsSuccess = false, Message = "User  not found" };
        }

        public ResultViewModel GetAll()
        {
            var users = _userRepository.GetAll().Select(u => new UserViewModel()
            {
                UserID = u.UserID,
                FullName = u.FullName,
                Birthdate = u.Birthdate,
                Gender = u.Gender,
                Age = (int)(DateTime.Now.Year - u.Birthdate.Year)
            }).ToList();

            return new ResultViewModel { IsSuccess = true, Message = "Data loaded  Successfully.", Data = users };
        }

        public ResultViewModel GetUserDetails(int UserId)
        {
            var User = _userRepository.GetAll(u => u.UserID == UserId).Select(u => new ManageUserViewModel()
            {
                UserID = u.UserID,
                FullName = u.FullName,
                Birthdate = u.Birthdate,
                Gender = (int)u.Gender
            }).FirstOrDefault();
            if (User != null)
            {
                return new ResultViewModel { IsSuccess = true, Message = "Data loaded  Successfully.", Data = User };
            }
            return new ResultViewModel { IsSuccess = false, Message = "User  not found" };
        }

        public ResultViewModel UpdateUser(ManageUserViewModel model)
        {
            if (model != null)
            {
                //       var user = _mapper.Map<User>(model);
                var user = _userRepository.GetAll(u => u.UserID == model.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Birthdate = model.Birthdate;
                    user.Gender = (Gender)model.Gender;
                    _userRepository.Update(user);
                    int save = _unitOfWork.Commit();
                    if (save == 0)
                        return new ResultViewModel { IsSuccess = false, Message = "Error" };
                    return new ResultViewModel { IsSuccess = true, Message = "User Updated   Successfully." };
                }
                return new ResultViewModel { IsSuccess = false, Message = "" };

            }
            return new ResultViewModel { IsSuccess = false, Message = "Some Properties  is  invalid." };
        }
    }
}
