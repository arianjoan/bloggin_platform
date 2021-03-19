using Bloggin_platform.Persistance.Context;
using Bloggin_platform.Persistance.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggin_platform.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _context;

        public UnitOfWork(BaseDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
