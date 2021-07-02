using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.ViewModels
{
    public class RentViewModel
    {
        public int BookId { get; set; }
        
        [Required(ErrorMessage = "This field can't be empty")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        public DateTime EndDate { get; set; }
    }
}
