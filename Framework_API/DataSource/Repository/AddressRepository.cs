using Framework_API.Data;
using Framework_API.DataSource.Interface;
using Framework_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.DataSource.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(DBContext context) : base(context)
        {
        }
    }
}