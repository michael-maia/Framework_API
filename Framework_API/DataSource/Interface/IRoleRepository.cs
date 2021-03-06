using Framework_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Interface
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<bool> RoleExist(string role);
        Task Update(Role role);
    }
}