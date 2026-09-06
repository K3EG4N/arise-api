using Arise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arise.Infrastructure.Persistence.Configuration
{
    public class EmployeeStatusConfiguration : IEntityTypeConfiguration<EmployeeStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatus> builder)
        {
            builder.ToTable("employeeStatuses", schema: "emp");

            builder.HasData(
                new EmployeeStatus
                {
                    EmployeeStatusId = Guid.Parse("F3A1C2D4-8B5E-4F7A-9C6D-2E1B0A3F4E5D"),
                    Code = "ACT",
                    Name = "Active",
                    Color = "#28A745"
                },
                new EmployeeStatus
                {
                    EmployeeStatusId = Guid.Parse("7D2E9F1A-3C4B-4E8F-B2A5-6D1C0E7F8A9B"),
                    Code = "INA",
                    Name = "Inactive",
                    Color = "#DC3545"
                },
                new EmployeeStatus
                {
                    EmployeeStatusId = Guid.Parse("A5B8C3E1-6F2D-4A9E-C7B4-1E3D5F2A8C6B"),
                    Code = "PEN",
                    Name = "Pending",
                    Color = "#FFC107"
                }
            );
        }
    }
}
