using Framework_API.Data;
using Framework_API.DataSource.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.ViewComponents
{
    public class RentedBooksViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly DBContext _context;

        public RentedBooksViewComponent(IUserRepository userRepository, DBContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedUser = await _userRepository.FetchLoggedUser(HttpContext.User);

            return View(await _context.Rents.Include(r => r.Book).Where(r => r.UserId == loggedUser.Id).ToListAsync());
        }
    }
}
