using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Models.TaskModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using Task = RSR.DAL.Models.TaskModel.Task;


namespace RSR.DAL.Data
{
    public  class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<StudentProfile> Students { get; set; }
        public DbSet<SupervisorProfile> Supervisors { get; set; }
        public DbSet<CoordinatorProfile> Coordinators { get; set; }
        public DbSet<ExaminerProfile> Examiners { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet <Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSubmission> TaskSubmissions { get; set; }
        public DbSet<TaskSubmissionComment> TaskSubmissionComments { get; set; }


        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Change Names of Identity Tables
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");

            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            // Relations User And Profiles 
            modelBuilder.Entity<StudentProfile>()
                .HasOne(st => st.User)
                .WithOne(u => u.StudentProfile)
                .HasForeignKey<StudentProfile>(st => st.UserId);

            modelBuilder.Entity<SupervisorProfile>()
                .HasOne(s => s.User)
                .WithOne(u => u.SupervisorProfile)
                .HasForeignKey<SupervisorProfile>(s=>s.UserId);

            modelBuilder.Entity<CoordinatorProfile>()
                .HasOne(c => c.User)
                .WithOne(u=>u.CoordinatorProfile)
                .HasForeignKey<CoordinatorProfile>(c=>c.UserId);


            modelBuilder.Entity<ExaminerProfile>()
                .HasOne(c => c.User)
                .WithOne(u => u.ExaminerProfile)
                .HasForeignKey<ExaminerProfile>(c => c.UserId);

            // relation with Project - Group  1 : 1
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Project)
                .WithOne(p => p.Group)
                .HasForeignKey<Project>(p => p.GroupId);

            modelBuilder.Entity<Project>()
           .HasIndex(p => p.GroupId)
           .IsUnique();

            // relation with Student - Group   1 : M
            modelBuilder.Entity<StudentProfile>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            // relation with Group - Supervisor 1 : M
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Supervisor)
                .WithMany(s => s.Groups)
                .HasForeignKey(g=>g.SupervisorId);

            // relation with Group - semester 
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SemesterId);


            // relation with Task - Group 1 : M 
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Group)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t=>t.GroupId);

            // relation with Task - Supervisor 1 : M 
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Supervisor)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation with  TaskSubmission - Student 1 : M 
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(t => t.Student)
                .WithMany(s => s.TaskSubmissions)
                .HasForeignKey(s=>s.StudentId);

            // relation with Task - Task Submission  1 : M
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(ts => ts.Task)
                .WithMany(t => t.TaskSubmissions)
                .HasForeignKey(ts=>ts.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation with TaskSubmission - TaskSubmissionComment
            modelBuilder.Entity<TaskSubmissionComment>()
                 .HasOne(c => c.TaskSubmission)
                 .WithMany(ts => ts.TaskSubmissionComments)
                 .HasForeignKey(c=>c.TaskSubmissionId)
                 .OnDelete(DeleteBehavior.Cascade); ;

            // relation with User -  TaskSubmissionComment  1 : M
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c => c.User)
                .WithMany(u => u.TaskSubmissionComments)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            // self relation TaskSubmissionComment - TaskSubmissionComment 
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c=>c.ParentComment)
                .WithMany(c=>c.Replies)
                .HasForeignKey(c=>c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
