using Framework_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Interface
{
    public interface IRentRepository : IGenericRepository<Rent>
    {
        Task<bool> CheckReservation(string userId, int bookId, DateTime startDate, DateTime endDate);
    }
}
