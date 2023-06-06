using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataContext : DbContext  //inherits the class from  entityframework core package.
    {
        ////We want to pass the DB options because we later on want to send our own connections through the Program.cs
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    //A DB set is a property of DB context class that represents a collection of entities in the database.
    public DbSet<City> Cities { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<FurnishingType> FurnishingTypes { get; set; }

}
}