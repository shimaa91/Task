using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.Models;
using Task.ViewModels.Users;

namespace Task.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Contact
            CreateMap<User, ManageUserViewModel>().ReverseMap();
            #endregion

        }
    }
}
