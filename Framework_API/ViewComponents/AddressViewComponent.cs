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
    public class AddressViewComponent : ViewComponent
    {
        private readonly DBContext _context;
        private readonly IUserRepository _userRepository;

        public AddressViewComponent(DBContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userRepository.FetchLoggedUser(HttpContext.User);
            return View(await _context.Addresses.Where(e => e.UserId == user.Id).ToListAsync());
        }
    }
}