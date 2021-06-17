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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly DBContext _context;

        public RoleRepository(DBContext context, RoleManager<Role> roleManager) : base(context)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExist(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }

        public async Task Update(Role role)
        {
            var nv = await _context.Roles.FindAsync(role.Id);
            nv.Name = role.Name;
            nv.NormalizedName = role.NormalizedName;
            nv.Description = role.Description;
            await _roleManager.UpdateAsync(nv);
        }
    }
}