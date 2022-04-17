using Microsoft.EntityFrameworkCore;
using StudentMgtMVC.Models;

namespace StudentMgtMVC.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<Student> Student { get; set; }

        DbSet<StudentMarks> stuMarks { get; set; }
    }
}
