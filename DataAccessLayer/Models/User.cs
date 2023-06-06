using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{

    public class User : IdentityUser
    {
        // Add any additional properties you need for the user model
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EnumDataType(typeof(Roles))]
        public string RoleAlloted { get; set; }


    }
}
