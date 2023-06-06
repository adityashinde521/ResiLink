using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CityChangesRequestDto
    {
        //[Required(ErrorMessage = "Name is mandatory field")]
        //[StringLength(50, MinimumLength = 2)]
      
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
