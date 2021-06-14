using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Enum
{
    public enum RentStatus : int
    {
        RETURNED = 0,
        RENTED = 1,
        OVERDUE = 2
    }
}