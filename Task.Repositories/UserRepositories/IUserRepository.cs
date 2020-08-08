using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.Models;
using Task.Repositories.GenericRepositories;

namespace Task.Repositories.UserRepositories
{
    public  interface IUserRepository : IGRepository<User>
    {    
    }
}
