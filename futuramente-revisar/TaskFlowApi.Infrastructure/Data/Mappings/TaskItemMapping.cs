using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlowApi.Domain.Entities;

namespace TaskFlowApi.Infrastructure.Data.Mappings;

public class TaskItemMapping : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("taskitems");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.ProjectId)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();
    }
}