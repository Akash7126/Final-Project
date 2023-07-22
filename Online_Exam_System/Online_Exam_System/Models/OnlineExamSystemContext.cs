﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Online_Exam_System.Models
{
    public partial class OnlineExamSystemContext : DbContext
    {
        public OnlineExamSystemContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoading(behavior: LazyLoadingBehavior.Proxy);
        //}
        public OnlineExamSystemContext(DbContextOptions<OnlineExamSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addministrator> Addministrators { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentBatch> DepartmentBatches { get; set; } = null!;
        public virtual DbSet<DepartmentTeacher> DepartmentTeachers { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentRegistration> StudentRegistrations { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addministrator>(entity =>
            {
                entity.HasKey(e => e.User);

                entity.ToTable("addministrator");

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch");

                entity.Property(e => e.BatchName).HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseCode).HasMaxLength(50);

                entity.Property(e => e.CourseTittle).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Department");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<DepartmentBatch>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartmentBatch");

                entity.HasOne(d => d.Batch)
                    .WithMany()
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentBatch_Batch");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentBatch_Department");
            });

            modelBuilder.Entity<DepartmentTeacher>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DepartmentTeacher");

                entity.HasOne(d => d.Department)
                    .WithMany()
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentTeacher_Department");

                entity.HasOne(d => d.Teacher)
                    .WithMany()
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentTeacher_Teacher");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Sex).HasMaxLength(50);

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.StudentPassword).HasMaxLength(50);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Batch");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Student");
            });

            modelBuilder.Entity<StudentRegistration>(entity =>
            {
                entity.ToTable("StudentRegistration");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentRegistrations)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentRegistration_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentRegistrations)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentRegistration_Student");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Contact).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.JobTitle).HasMaxLength(50);

                entity.Property(e => e.Sex).HasMaxLength(50);

                entity.Property(e => e.TeacherName).HasMaxLength(50);

                entity.Property(e => e.TeacherPassword).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}