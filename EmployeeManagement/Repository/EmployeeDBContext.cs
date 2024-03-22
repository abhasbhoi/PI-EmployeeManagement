using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repository
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
            
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;

    }
}
