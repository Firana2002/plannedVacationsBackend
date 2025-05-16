using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VacationPlanner.Api.Models;

public partial class VacationPlannerDbContext : DbContext
{
    public VacationPlannerDbContext()
    {
    }

    public VacationPlannerDbContext(DbContextOptions<VacationPlannerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalVacationDay> AdditionalVacationDays { get; set; }
  
    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentRestriction> DepartmentRestrictions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeVacationDay> EmployeeVacationDays { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }
    public virtual DbSet<Role> Roles { get; set; }


    public virtual DbSet<PlannedVacation> PlannedVacations { get; set; }

    public DbSet<PastVacations> PastVacations { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Restriction> Restrictions { get; set; }

    public virtual DbSet<VacationType> VacationTypes { get; set; }

    public virtual DbSet<VacationStatus> VacationStatuses { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<EmployeeVacationDay>(builder =>
    {
        builder.Property(evd => evd.StartDate)
            .HasConversion(new DateOnlyConverter());
    });
        modelBuilder.Entity<AdditionalVacationDay>(entity =>
        {
            entity.HasKey(e => e.AdditionalVacationDaysId).HasName("PK__Addition__609164D8D44CE74C");
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role"); // Настройка первичного ключа
            entity.Property(e => e.NameRole).IsRequired().HasMaxLength(50); // Настройка свойства NameRole
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDA328D9C5");

            entity.HasOne(d => d.Organization).WithMany(p => p.Departments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__Organ__4BAC3F29");
        });

        modelBuilder.Entity<DepartmentRestriction>(entity =>
        {
            entity.HasKey(e => e.DepartmentRestrictionId).HasName("PK__Departme__ABCAF3CDD5199EFE");

            entity.HasOne(d => d.Department).WithMany(p => p.DepartmentRestrictions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__Depar__6383C8BA");

            entity.HasOne(d => d.Restriction).WithMany(p => p.DepartmentRestrictions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__Restr__6477ECF3");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1107E6E4F2");

            entity.Property(e => e.HasDisabledChild).HasDefaultValue(false);
            entity.Property(e => e.IsHonorDonor).HasDefaultValue(false);
            entity.Property(e => e.IsMultipleChildren).HasDefaultValue(false);
            entity.Property(e => e.IsVeteran).HasDefaultValue(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Depar__5441852A");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Posit__5535A963");
            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Roles");
        });

        modelBuilder.Entity<EmployeeVacationDay>(entity =>
        {
            entity.HasKey(e => e.EmployeeVacationDaysId).HasName("PK__Employee__2D6E62F473F37097");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeVacationDays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeV__Emplo__59FA5E80");

            entity.HasOne(d => d.VacationType).WithMany(p => p.EmployeeVacationDays)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeV__Vacat__5AEE82B9");
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PK__Holidays__2D35D57AEAB79E08");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B12B6A1759C");
        });

        modelBuilder.Entity<PlannedVacation>(entity =>
        {
            entity.HasKey(e => e.PlannedVacationId).HasName("PK__PlannedV__1C230134C01B9D4D");

            entity.HasOne(d => d.Employee).WithMany(p => p.PlannedVacations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlannedVa__Emplo__6754599E");

            entity.HasOne(d => d.VacationType).WithMany(p => p.PlannedVacations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlannedVa__Vacat__68487DD7");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A7994A6A7E7");
        });

        modelBuilder.Entity<Restriction>(entity =>
        {
            entity.HasKey(e => e.RestrictionId).HasName("PK__Restrict__529D86BA45717F5E");
        });

        modelBuilder.Entity<VacationType>(entity =>
        {
            entity.HasKey(e => e.VacationTypeId).HasName("PK__Vacation__22E546763BF5943B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}