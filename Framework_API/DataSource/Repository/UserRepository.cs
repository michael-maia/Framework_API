using Framework_API.Data;
using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        // Controle as funções de login
        private readonly SignInManager<User> _signInManager;
        // Registro de usuário
        private readonly UserManager<User> _userManager;

        public UserRepository(DBContext context, SignInManager<User> signInManager, UserManager<User> userManager) : base(context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task AssignRole(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<User> FetchLoggedUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<User> FetchUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task Login(User user, bool remind)
        {
            await _signInManager.SignInAsync(user, remind);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> SaveUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}