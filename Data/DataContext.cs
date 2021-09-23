using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpDeskApi.Models;
using Microsoft.AspNetCore.Http;

namespace HelpDeskApi.Data
{

    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
<<<<<<< HEAD
        public DbSet<Case> HD_Case { get; set; }   
        public DbSet<Status> Status { get; set; }   

        public DbSet<Branch> Branch { get; set; }   
        public DbSet<Country> Country { get; set; }

        public DbSet<HDSystem> HDSystem { get; set; }     

        public DbSet<Department> Department { get; set; }    

        public DbSet<Module> Module { get; set; }  

        public DbSet<Priority> Priority { get; set; }  

        public DbSet<Topic> Topic{ get; set; }  

        public DbSet<User> User{ get; set; }  

       

        


        



        
       
       
=======
        public DbSet<Case> HD_Case { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<IsmDb> IsmDb { get; set; }
        public DbSet<IncidentCase> IncidentCase { get; set; }
     


>>>>>>> 8318f1ec7da2072c666fde64ec81f69329da7c74
    }
}
