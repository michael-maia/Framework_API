using Framework_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.ViewComponents
{
    public class RentViewComponent : ViewComponent
    {
        private readonly DBContext _context;

        public RentViewComponent(DBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int BookId)
        {
            return View(await _context.Books.FirstOrDefaultAsync(b => b.BookId == BookId));
        }
    }
}
