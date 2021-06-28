using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Framework_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
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

            // Utiliza uma propriedade da classe ClaimsPrincipal como parametro
            return View(await _userRepository.FetchLoggedUser(User));
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                // Criando um objeto da classe User para utiliza-lo como parametro na função SaveUser()
                var user = new User
                {
                    UserName = register.UserName,                    
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

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userRepository.Logout();               
            }
            _logger.LogInformation("Entering login page");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            // Caso os dados enviados pelo usuário sejam válidos
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Fetching user by email");
                var user = await _userRepository.FetchUserByEmail(login.Email);

                // Instanciando objeto da classe PasswordHasher para usar suas funções
                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                if (user != null)
                {
                    _logger.LogInformation("Checking user data");

                    // Comparando a senha do usuário com a que foi enviada na tela de login
                    if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Correct data. Logging user");
                        await _userRepository.Login(user, false);

                        return RedirectToAction("Index", "Users");
                    }

                    _logger.LogError("Invalid data");
                    ModelState.AddModelError("", "Invalid password and/or email");
                }

                _logger.LogError("Invalid data");
                ModelState.AddModelError("", "Invalid password and/or email");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();

            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string userId)
        {
            _logger.LogInformation("Checking if user exists");

            // Pega as informações do usuário baseado no ID e retorna na View
            var user = await _userRepository.FetchById(userId);
            var updateViewModel = new UpdateViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                CPF = user.CPF,
                Phone = user.Phone,
                FullName = user.FullName                
            };
            _logger.LogInformation("Update user");
            return View(updateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel updateViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.FetchById(updateViewModel.Id);

                // Atualiza as informações do usuário baseado no objeto da classe UpdateViewModel criado anteriormente no método GET do Update
                user.FullName = updateViewModel.FullName;
                user.CPF = updateViewModel.CPF;
                user.UserName = updateViewModel.UserName;
                user.Email = updateViewModel.Email;
                user.Phone = updateViewModel.Phone;
                user.BirthDate = updateViewModel.BirthDate;

                await _userRepository.UpdateUser(user);
                _logger.LogInformation("Updating user");

                return RedirectToAction("Index", "Users");
            }
            _logger.LogError("Invalid information");

            return View(updateViewModel);
        }
    }
}