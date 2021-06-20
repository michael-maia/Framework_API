using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Framework_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listing data");
            return View(await _userRepository.FetchLoggedUser(User));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            // Se o usuário já estiver logado e vai ser deslogado
            if (User.Identity.IsAuthenticated)
            {
                await _userRepository.Logout();
            }

            _logger.LogInformation("Entering register page");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = register.UserName,
                    City = register.City,
                    State = register.State,
                    BirthDate = register.BirthDate,
                    Email = register.UserEmail,
                    CPF = register.CPF,
                    Phone = register.Phone,
                    FullName = register.FullName,
                    PasswordHash = register.Password                    
                };

                _logger.LogInformation("Trying to create a user");
                var result = await _userRepository.SaveUser(user, register.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("New user created");
                    _logger.LogInformation("Assigning role to new user");
                    // Este nível de acesso deve estar criado caso contrário vai dar erro                    
                    var role = "Administrator";

                    await _userRepository.AssignRole(user, role);
                    _logger.LogInformation("Assignment completed");

                    _logger.LogInformation("Login user");
                    await _userRepository.Login(user, false);
                    _logger.LogInformation("User logged with success");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError("Failed to create user");

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description.ToString());
                }
            }
            _logger.LogError("Invalid user data");
            return View(register);
        }
    }
}