using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.Models;

namespace Task.Data.DataContext
{
    public  class TaskDbContext:IdentityDbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
    }
}
