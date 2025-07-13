using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data{
    public class ApplicationDbcontext : DbContext{
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> option) : base(option){}
        public DbSet<Person> Person {get; set;}
        public DbSet<DemoMVC.Models.DaiLy> DaiLy { get; set; } = default!;
        public DbSet<DemoMVC.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;

              
    }
}