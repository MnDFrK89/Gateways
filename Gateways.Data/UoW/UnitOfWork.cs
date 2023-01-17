using Gateways.Data.Entities;
using Gateways.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Data.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CoreDbContext _context;

        public UnitOfWork(CoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            GatewaysRepository ??= new GenericRepository<Gateway>(_context);
            PeripheralsRepository ??= new GenericRepository<Peripheral>(_context);

        }

        public IGenericRepository<Gateway> GatewaysRepository { get; set; }
        public IGenericRepository<Peripheral> PeripheralsRepository { get; set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
