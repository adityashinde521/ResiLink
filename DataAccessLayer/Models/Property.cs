using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public Guid FurnishingTypeId { get; set; }
        public FurnishingType FurnishingType { get; set; }
        public int Price { get; set; }
        public int BHK { get; set; }
        public int BuiltArea { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public bool ReadyToMove { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloors { get; set; }
        public string MainEntrance { get; set; }
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }

        //public ICollection<Photo> Photos { get; set; }
        //public DateTime PostedOn { get; set; } = DateTime.Now;

    }
}
