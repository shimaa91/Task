using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Task.Data.DataContext;

namespace Task.Repositories.UnitOfWork
{
    public  class UnitOFWork : IUnitOfWork
    {
        public TaskDbContext _dbContext { get; }
        
        private bool disposed = false;

        public UnitOFWork(TaskDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
   
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

