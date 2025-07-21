using Microsoft.EntityFrameworkCore;

namespace TestMVC.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> option) : base(option) { }
    }

}