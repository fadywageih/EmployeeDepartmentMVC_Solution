using EmployeeDepartment.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeDepartment.DAL.Presistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(d => d.Code).IsRequired().HasColumnType("varchar(50)");
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(d => d.LastModificationOn).HasComputedColumnSql("GETDATE()");
            #region For work Relationship
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion

        }
    }
}
