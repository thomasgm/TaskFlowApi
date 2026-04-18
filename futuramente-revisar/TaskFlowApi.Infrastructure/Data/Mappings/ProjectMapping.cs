using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlowApi.Domain.Entities;

namespace TaskFlowApi.Infrastructure.Data.Mappings;

public class ProjectMapping : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}