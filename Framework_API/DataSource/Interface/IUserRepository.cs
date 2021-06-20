using Framework_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FetchLoggedUser(ClaimsPrincipal user);        
        // IdentityResult => Retorna se a função executada pelo Identity funcionou ou teve falhas
        Task<IdentityResult> SaveUser(User user, string password);
        Task UpdateUser(User user);
        Task AssignRole(User user, string role);
        // Remind => Caso o usuário queira lembrar os dados ao realizar outro login
        Task Login(User user, bool remind);
        Task Logout();
        Task<User> FetchUserByEmail(string email);
    }
}
