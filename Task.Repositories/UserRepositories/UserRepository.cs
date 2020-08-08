using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.DataContext;
using Task.Data.Models;
using Task.Repositories.GenericRepositories;

namespace Task.Repositories.UserRepositories
{
    public  class UserRepository:GRepository<User>,IUserRepository
    {
        private readonly TaskDbContext _taskDbContext;
        public UserRepository(TaskDbContext taskDbContext) : base(taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }
    }
}
