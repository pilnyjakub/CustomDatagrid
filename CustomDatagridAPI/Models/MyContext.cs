using Microsoft.EntityFrameworkCore;

namespace CustomDatagridAPI.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Person> TbPeople { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseMySQL("server=localhost;database=DbData;user=root;password=");
        }
    }
}
