using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Repositories.UnitOfWork
{
    public  interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
