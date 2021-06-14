using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public int Edition { get; set; }
        public int Year { get; set; }
        public int NumberOfPages { get; set; }
        public string Photo { get; set; }
        
        // Chave estrangeira
        public ICollection<Rent> Rents { get; set; }
    }
}