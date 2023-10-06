using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Entities;

namespace Stereograph.TechnicalTest.Api.Models;

public partial class TesttechniqueContext : DbContext
{
    public TesttechniqueContext()
    {
    }

    public TesttechniqueContext(DbContextOptions<TesttechniqueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("TEXT (50)")
                .HasColumnName("name");
            entity.Property(e => e.Step).HasColumnName("step");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
