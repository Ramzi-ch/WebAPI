using DataMapping.Models;
using Microsoft.EntityFrameworkCore;

namespace DataMapping.DataContext
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Country> Country { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadCountryData(modelBuilder);
            LoadDepartmentData(modelBuilder);
            LoadEmployeeData(modelBuilder);
        }

        #region Load Some Data Into DB

        private void LoadCountryData(ModelBuilder modelBuilder)
        {
            //Insert data in Country table
            modelBuilder.Entity<Country>().HasData(new Country
            {
                CountryId = 1,
                Name = "Sfax",
                Population = 1000
            }, new Country
            {
                CountryId = 2,
                Name = "Sousse",
                Population = 1000
            }, new Country
            {
                CountryId = 3,
                Name = "Gabes",
                Population = 1200
            });
        }

        private void LoadDepartmentData(ModelBuilder modelBuilder)
        {
            //Insert data in Department table
            modelBuilder.Entity<Department>().HasData(new Department
            {
                DepartmentId = 1,
                DepartmentName = ".Net"
            }, new Department
            {
                DepartmentId = 2,
                DepartmentName = "NodeJs"
            }, new Department
            {
                DepartmentId = 3,
                DepartmentName = "Flutter"
            }, new Department
            {
                DepartmentId = 4,
                DepartmentName = "React"
            }, new Department
            {
                DepartmentId = 5,
                DepartmentName = "WordPress"
            }, new Department
            {
                DepartmentId = 6,
                DepartmentName = "Prestashop"
            }, new Department
            {
                DepartmentId = 7,
                DepartmentName = "RH"
            });
        }
        private void LoadEmployeeData(ModelBuilder modelBuilder)
        {
            //Insert data in Employee table
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Sofien"
            }, new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Ahmed"
            }, new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Khadija"
            }, new Employee
            {
                EmployeeId = 4,
                EmployeeName = "Mahmoud"
            }, new Employee
            {
                EmployeeId = 5,
                EmployeeName = "Majdi"
            }, new Employee
            {
                EmployeeId = 6,
                EmployeeName = "Haitham"
            }, new Employee
            {
                EmployeeId = 7,
                EmployeeName = "Maryam"
            });
        }

        #endregion
        
    }
}
