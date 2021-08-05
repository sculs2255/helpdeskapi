using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpDeskApi.Models;
using Microsoft.AspNetCore.Http;

namespace HelpDeskApi.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Case> HD_Case { get; set; }   
       
    }
}
