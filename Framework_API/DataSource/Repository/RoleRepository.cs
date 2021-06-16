using Framework_API.Data;
using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// TODO = COLOCAR COMENTÁRIOS SOBRE "PADRÃO REPOSITÓRIO"
// https://docs.microsoft.com/pt-br/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application    

namespace Framework_API.DataSource.Repository
{   
    public class IRoleRepository : GenericRepository<Role>, IRole
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DBContext _context;

        public IRoleRepository(DBContext context, RoleManager<Role> roleManager) : base(context)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExist(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}