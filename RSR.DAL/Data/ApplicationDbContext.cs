using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using RSR.DAL.Models.Evaluation;
=======
>>>>>>> origin/master
using RSR.DAL.Models.ProjectGroupModel;
using RSR.DAL.Models.ProjectModel;
using RSR.DAL.Models.SemesterModel;
using RSR.DAL.Models.TaskModel;
<<<<<<< HEAD
using RSR.DAL.Models.User;
using System;
using Task = RSR.DAL.Models.TaskModel.Task;

namespace RSR.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
=======
using RSR.DAL.Models.ThesisModel;
using RSR.DAL.Models.User;
using System;
using System.Collections.Generic;
using Task = RSR.DAL.Models.TaskModel.Task;


namespace RSR.DAL.Data
{
    public  class ApplicationDbContext : IdentityDbContext<ApplicationUser>
>>>>>>> origin/master
    {
        public DbSet<StudentProfile> Students { get; set; }
        public DbSet<SupervisorProfile> Supervisors { get; set; }
        public DbSet<CoordinatorProfile> Coordinators { get; set; }
        public DbSet<ExaminerProfile> Examiners { get; set; }
<<<<<<< HEAD
        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }
=======

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet <Project> Projects { get; set; }
>>>>>>> origin/master

        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSubmission> TaskSubmissions { get; set; }
        public DbSet<TaskSubmissionComment> TaskSubmissionComments { get; set; }

<<<<<<< HEAD
        //  Evaluation Module
        public DbSet<EvaluationForm> EvaluationForms { get; set; }
        public DbSet<EvaluationField> EvaluationFields { get; set; }

        //Evaluation Submission
        public DbSet<EvaluationSubmission> EvaluationSubmissions { get; set; }

        public DbSet<EvaluationSubmissionAnswer> EvaluationSubmissionAnswers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
=======
        public DbSet<Thesis> Thesis { get; set; }
        public DbSet<ThesisVersions> ThesisVersions { get; set; }
        public DbSet<ThesisFeedback> ThesisFeedbacks { get; set; }

        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options)
        {
        
>>>>>>> origin/master
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
<<<<<<< HEAD

=======
>>>>>>> origin/master
            // Change Names of Identity Tables
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
<<<<<<< HEAD
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
=======

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");

>>>>>>> origin/master
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

            // Relations User And Profiles 
            modelBuilder.Entity<StudentProfile>()
                .HasOne(st => st.User)
                .WithOne(u => u.StudentProfile)
                .HasForeignKey<StudentProfile>(st => st.UserId);

            modelBuilder.Entity<SupervisorProfile>()
                .HasOne(s => s.User)
                .WithOne(u => u.SupervisorProfile)
<<<<<<< HEAD
                .HasForeignKey<SupervisorProfile>(s => s.UserId);

            modelBuilder.Entity<CoordinatorProfile>()
                .HasOne(c => c.User)
                .WithOne(u => u.CoordinatorProfile)
                .HasForeignKey<CoordinatorProfile>(c => c.UserId);
=======
                .HasForeignKey<SupervisorProfile>(s=>s.UserId);

            modelBuilder.Entity<CoordinatorProfile>()
                .HasOne(c => c.User)
                .WithOne(u=>u.CoordinatorProfile)
                .HasForeignKey<CoordinatorProfile>(c=>c.UserId);

>>>>>>> origin/master

            modelBuilder.Entity<ExaminerProfile>()
                .HasOne(c => c.User)
                .WithOne(u => u.ExaminerProfile)
                .HasForeignKey<ExaminerProfile>(c => c.UserId);
<<<<<<< HEAD
          
            modelBuilder.Entity<EvaluationSubmissionAnswer>()
             .HasOne(a => a.EvaluationField)
             .WithMany()
             .HasForeignKey(a => a.EvaluationFieldId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EvaluationSubmission>()
             .HasOne(es => es.EvaluationForm)
              .WithMany()
             .HasForeignKey(es => es.EvaluationFormId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EvaluationSubmissionAnswer>()
           .HasOne(a => a.EvaluationSubmission)
            .WithMany(s => s.Answers)
            .HasForeignKey(a => a.EvaluationSubmissionId)
          .OnDelete(DeleteBehavior.Cascade);
=======
>>>>>>> origin/master

            // relation with Project - Group  1 : 1
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Project)
                .WithOne(p => p.Group)
                .HasForeignKey<Project>(p => p.GroupId);

            modelBuilder.Entity<Project>()
<<<<<<< HEAD
                .HasIndex(p => p.GroupId)
                .IsUnique();
=======
           .HasIndex(p => p.GroupId)
           .IsUnique();
>>>>>>> origin/master

            // relation with Student - Group   1 : M
            modelBuilder.Entity<StudentProfile>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            // relation with Group - Supervisor 1 : M
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Supervisor)
                .WithMany(s => s.Groups)
<<<<<<< HEAD
                .HasForeignKey(g => g.SupervisorId);

            // relation with Group - semester 
=======
                .HasForeignKey(g=>g.SupervisorId);

            // relation with  semester - Group 1 : M
>>>>>>> origin/master
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SemesterId);

<<<<<<< HEAD
            // relation with Task - Group 1 : M 
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Group)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t => t.GroupId);

            // relation with Task - Supervisor 1 : M 
=======

            // relation with  Group - Task 1 : M 
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Group)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t=>t.GroupId);

            // relation with  Supervisor - Task  1 : M 
>>>>>>> origin/master
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Supervisor)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

<<<<<<< HEAD
            // relation with TaskSubmission - Student 1 : M 
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(t => t.Student)
                .WithMany(s => s.TaskSubmissions)
                .HasForeignKey(s => s.StudentId);
=======
            // relation with  TaskSubmission - Student 1 : M 
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(t => t.Student)
                .WithMany(s => s.TaskSubmissions)
                .HasForeignKey(s=>s.StudentId).OnDelete(DeleteBehavior.NoAction);
>>>>>>> origin/master

            // relation with Task - Task Submission  1 : M
            modelBuilder.Entity<TaskSubmission>()
                .HasOne(ts => ts.Task)
                .WithMany(t => t.TaskSubmissions)
<<<<<<< HEAD
                .HasForeignKey(ts => ts.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation with TaskSubmission - TaskSubmissionComment
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c => c.TaskSubmission)
                .WithMany(ts => ts.TaskSubmissionComments)
                .HasForeignKey(c => c.TaskSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // relation with User - TaskSubmissionComment  1 : M
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c => c.User)
                .WithMany(u => u.TaskSubmissionComments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // self relation TaskSubmissionComment - TaskSubmissionComment 
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Evaluation Form - Fields (One-to-Many)
            modelBuilder.Entity<EvaluationForm>()
                .HasMany(f => f.Fields)
                .WithOne(f => f.EvaluationForm)
                .HasForeignKey(f => f.EvaluationFormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
=======
                .HasForeignKey(ts=>ts.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            // relation with TaskSubmission - TaskSubmissionComment 1 : M
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
                .OnDelete(DeleteBehavior.NoAction);

            // self relation TaskSubmissionComment - TaskSubmissionComment 1 : M
            modelBuilder.Entity<TaskSubmissionComment>()
                .HasOne(c=>c.ParentComment)
                .WithMany(c=>c.Replies)
                .HasForeignKey(c=>c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            // relation with Thesis , Group 1 : 1 
            modelBuilder.Entity<Thesis>()
                .HasOne(th => th.Group)
                .WithOne(g => g.Thesis)
                .HasForeignKey<Thesis>(th=>th.GroupId);

            // relation with Thesis , Thesis Version  1 : M 
            modelBuilder.Entity<ThesisVersions>()
                .HasOne(ThV => ThV.Thesis)
                 .WithMany(Th =>Th.ThesisVersions)
                 .HasForeignKey(ThV=>ThV.ThesisId)
                 .OnDelete(DeleteBehavior.NoAction);
            

            // relation with   Student , ThesisVersion 1 : M  
            modelBuilder.Entity<ThesisVersions>()
                .HasOne(ThV => ThV.student)
                .WithMany(s => s.ThesisVersions)
                .HasForeignKey(ThV => ThV.studentId)
                .OnDelete(DeleteBehavior.NoAction);
            

            // relation with ThesisVersion - ThesisFeedBack 1 : M
            modelBuilder.Entity<ThesisFeedback>()
                .HasOne(F => F.ThesisVersion)
                .WithMany(V=>V.thesisFeedbacks)
                .HasForeignKey(F=>F.VersionId)
                .OnDelete(DeleteBehavior.NoAction);
            

            //relation with   User - ThesisFeedBack  1 : M 

            modelBuilder.Entity<ThesisFeedback>()
                .HasOne(F => F.Reviwer)
                .WithMany(u => u.ThesisFeedbacks)
                .HasForeignKey(F=>F.ReviwerId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
>>>>>>> origin/master
