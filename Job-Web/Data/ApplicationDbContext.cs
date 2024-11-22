using Microsoft.EntityFrameworkCore;
using Job_Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Job_Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
    }
}