//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using WebApplication4.Data;

//namespace WebApplication4.Data2;

//public partial class DbcoursesContext : DbContext
//{
//    public DbcoursesContext()
//    {
//    }

//    public DbcoursesContext(DbContextOptions<DbcoursesContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Course> Courses { get; set; }

//    public virtual DbSet<Photo> Photos { get; set; }

//    public virtual DbSet<Student> Students { get; set; }

//    public virtual DbSet<Teacher> Teachers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-9DBVOBO\\SQLEXPRESS; Database=DBCourses; Trusted_Connection=True; TrustServerCertificate=True;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Course>(entity =>
//        {
//            entity.HasIndex(e => e.Id, "UQ_Courses_id").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("ID");
//            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
//            entity.Property(e => e.Description).HasMaxLength(255);
//            entity.Property(e => e.DifficultyLevelId).HasColumnName("DifficultyLevelID");
//            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
//            entity.Property(e => e.TeachersId).HasColumnName("TeachersID");
//            entity.Property(e => e.Title).HasMaxLength(255);

//            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Courses)
//                .HasForeignKey(d => d.PhotoUrlId)
//                .HasConstraintName("FK_Courses_Photo");

//            entity.HasOne(d => d.Teachers).WithMany(p => p.Courses)
//                .HasForeignKey(d => d.TeachersId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Courses_Teachers");
//        });

//        modelBuilder.Entity<Photo>(entity =>
//        {
//            entity.ToTable("Photo");

//            entity.Property(e => e.Id).HasColumnName("ID");
//            entity.Property(e => e.PhotoUrl).HasMaxLength(255);
//        });

//        modelBuilder.Entity<Student>(entity =>
//        {
//            entity.Property(e => e.Id)
//                .ValueGeneratedNever()
//                .HasColumnName("ID");
//            entity.Property(e => e.CourseId).HasColumnName("CourseID");
//            entity.Property(e => e.Email).HasMaxLength(255);
//            entity.Property(e => e.FirstName).HasMaxLength(255);
//            entity.Property(e => e.LastName).HasMaxLength(255);
//            entity.Property(e => e.Password).HasMaxLength(255);
//            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
//            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
//            entity.Property(e => e.Username).HasMaxLength(255);

//            entity.HasOne(d => d.Course).WithMany(p => p.Students)
//                .HasForeignKey(d => d.CourseId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Students_Courses");

//            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Students)
//                .HasForeignKey(d => d.PhotoUrlId)
//                .HasConstraintName("FK_Students_Photo");
//        });

//        modelBuilder.Entity<Teacher>(entity =>
//        {
//            entity.Property(e => e.Id).HasColumnName("ID");
//            entity.Property(e => e.Email).HasMaxLength(255);
//            entity.Property(e => e.FirstName).HasMaxLength(255);
//            entity.Property(e => e.LastName).HasMaxLength(255);
//            entity.Property(e => e.Password).HasMaxLength(255);
//            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
//            entity.Property(e => e.PhotoUrlId).HasColumnName("PhotoUrlID");
//            entity.Property(e => e.Username).HasMaxLength(255);

//            entity.HasOne(d => d.PhotoUrl).WithMany(p => p.Teachers)
//                .HasForeignKey(d => d.PhotoUrlId)
//                .HasConstraintName("FK_Teachers_Photo");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
