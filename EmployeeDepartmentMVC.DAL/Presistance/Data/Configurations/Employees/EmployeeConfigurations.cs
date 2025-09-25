using EmployeeDepartment.DAL.Common.Enums;
using EmployeeDepartment.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDepartment.DAL.Presistance.Data.Configurations.Employees
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e=>e.Address).HasColumnType("varchar(100)"); 
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            #region Enum
            builder.Property(e=>e.Gender).HasConversion((gender)=>gender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender),gender));
            builder.Property(e => e.EmployeeType).HasConversion((type) => type.ToString(),
                (EmployeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeType));
            #endregion

        }
    }
}
