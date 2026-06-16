using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementSystem.Models;

public partial class EmployeeContext : DbContext
{
    public EmployeeContext()
    {
    }

    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }


    public virtual DbSet<EmployeeWithSalary> EmpSalary { get; set; }
    public virtual DbSet<EmpProjects> EmpProject { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MQ359FU\\SQLEXPRESS;Database=Employee;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PK__Employee__3E2ED64A0082525B");

            entity.ToTable("Employee");

            entity.Property(e => e.EId).HasColumnName("e_id");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Degree).HasMaxLength(100);
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Nid)
                .HasMaxLength(20)
                .HasColumnName("NId");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Pic).HasMaxLength(255);
            entity.Property(e => e.SId).HasColumnName("s_id");

            entity.HasOne(d => d.SIdNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SId)
                .HasConstraintName("FK_Employee_Salary");
        });

        modelBuilder.Entity<EmployeeWithSalary>().HasNoKey();
        modelBuilder.Entity<EmpProjects>().HasNoKey();


        modelBuilder.Entity<EmployeeLeave>(entity =>
        {
            entity.HasKey(e => e.LId).HasName("PK__employee__4208A4A68E6E125C");

            entity.ToTable("employeeLeave");

            entity.Property(e => e.LId).HasColumnName("L_id");
            entity.Property(e => e.EId).HasColumnName("e_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.EIdNavigation).WithMany(p => p.EmployeeLeaves)
                .HasForeignKey(d => d.EId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_employeeLeave_Employee");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3214EC07F46508C6");

            entity.ToTable("Login");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.PId).HasName("PK__Project__82E06B91277033C2");

            entity.ToTable("Project");

            entity.Property(e => e.PId).HasColumnName("p_id");
            entity.Property(e => e.Duedate).HasColumnName("duedate");
            entity.Property(e => e.EId).HasColumnName("e_id");
            entity.Property(e => e.Mark)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mark");
            entity.Property(e => e.PName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("p_name");
            entity.Property(e => e.PStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("p_status");
            entity.Property(e => e.Subdate).HasColumnName("subdate");

            entity.HasOne(d => d.EIdNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.EId)
                .HasConstraintName("FK__Project__e_id__4D94879B");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SId).HasName("PK__Salary__2F3684F4EB436BBF");

            entity.ToTable("Salary");

            entity.Property(e => e.SId).HasColumnName("s_id");
            entity.Property(e => e.Base)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base");
            entity.Property(e => e.Bonus)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("bonus");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([base]+[bonus])", true)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
