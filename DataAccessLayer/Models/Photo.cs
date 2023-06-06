using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("Photos")]
    public class Photo
    {
        public Guid Id { get; set; }

        //[Required]
        //public string PublicId { get; set; }
        public string ImageUrl { get; set; }
        //public bool IsPrimary { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

    }
}
