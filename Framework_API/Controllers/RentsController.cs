using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Framework_API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
    public class RentsController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private readonly IAccoun _contaRepositorio;
        private readonly IRentRepository _rentRepository;
        private readonly ILogger<RentsController> _logger;
        //private readonly IEmail _email;

        public RentsController(IUserRepository userRepository, IRentRepository rentRepository, ILogger<RentsController> logger)
        {
            _userRepository = userRepository;
            //_contaRepositorio = contaRepositorio;
            _rentRepository = rentRepository;
            _logger = logger;
            //_email = email;
        }

        public IActionResult Rent(int bookId)
        {
            _logger.LogInformation("Starting book rent");

            var rent = new RentViewModel
            {
                BookId = bookId              
            };

            return View(rent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent(RentViewModel rent)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.FetchLoggedUser(User);
                //var saldo = _contaRepositorio.PegarSaldoPeloId(usuario.Id);

                if (await _rentRepository.CheckReservation(user.Id, rent.BookId, rent.StartDate, rent.EndDate))
                {
                    _logger.LogInformation("User already have a reservation");
                    TempData["Warning"] = "You already have this reservation";
                    return View(rent);
                }                
                else
                {
                    Rent r = new Rent
                    {
                        UserId = user.Id,
                        BookId = rent.BookId,
                        StartDate = rent.StartDate,
                        EndDate = rent.EndDate                       
                    };

                    //_logger.LogInformation("Enviando email com detalhes da reserva");
                    //string assunto = "Reserva concluída com sucesso";

                    string message = string.Format("Your book is ready. You can take it on {0}" +
                        " and return it on day {1}. Enjoy it!!! ", rent.StartDate, rent.EndDate);

                    //await _email.EnviarEmail(usuario.Email, assunto, mensagem);

                    await _rentRepository.Insert(r);
                    _logger.LogInformation("Reserva feita");          
                    return RedirectToAction("Index", "Books");
                }
            }
            _logger.LogInformation("Invalid information");
            return View(rent);
        }
    }
}
