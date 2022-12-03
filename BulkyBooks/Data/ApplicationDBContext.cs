using BulkyBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBooks.Data
{
    //Class made 
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        //Doing this is enough to create the Category table on the DB
        public DbSet<Category> Categories { get; set; }
            
    }
}
