using Framework_API.Data;
using Framework_API.DataSource.Interface;
using Framework_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Repository
{
    public class RentRepository : GenericRepository<Rent>, IRentRepository
    {
        private readonly DBContext _context;

        public RentRepository(DBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckReservation(string userId, int bookId, DateTime startDate, DateTime endDate)
        {
            return await _context.Rents.AnyAsync(r => r.UserId == userId && r.BookId == bookId && DateTime.Equals(r.StartDate, startDate) && DateTime.Equals(r.EndDate, endDate));
        }
    }
}
