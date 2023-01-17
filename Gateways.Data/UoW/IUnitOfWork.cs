using Gateways.Data.Entities;
using Gateways.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Data.UoW
{
    public interface IUnitOfWork
    {
        IGenericRepository<Gateway> GatewaysRepository { get; set; }
        IGenericRepository<Peripheral> PeripheralsRepository { get; set; }

        Task<int> CommitAsync();
    }
}
