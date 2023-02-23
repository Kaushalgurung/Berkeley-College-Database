using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace berkeley_collegee.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<FeePayment> FeePayment { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleResult> ModuleResult { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonAddress> PersonAddress { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentAttendence> StudentAttendence { get; set; }
        public virtual DbSet<StudentModule> StudentModule { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherModule> TeacherModule { get; set; }
        public object Module_Result { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
 //               optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));User ID=kaushal;Password=9869164528;Persist Security Info=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "ALKA");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.AddressId)
                    .HasColumnName("ADDRESS_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("CITY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("COUNTRY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceOrState)
                    .HasColumnName("PROVINCE_OR_STATE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasColumnName("STREET_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.StreetNo)
                    .HasColumnName("STREET_NO")
                    .HasColumnType("NUMBER(38)");
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("ASSIGNMENT");

                entity.Property(e => e.AssignmentId)
                    .HasColumnName("ASSIGNMENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AssignmentType)
                    .IsRequired()
                    .HasColumnName("ASSIGNMENT_TYPE")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ASSIGNMENT_DEPARTMENT_FK");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DEPARTMENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentContact)
                    .HasColumnName("DEPARTMENT_CONTACT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_NAME")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FeePayment>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("FEE_PAYMENT_PK");

                entity.ToTable("FEE_PAYMENT");

                entity.Property(e => e.InvoiceNo)
                    .HasColumnName("Invoice_no.")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("NUMBER(15,2)");

                entity.Property(e => e.DepartmentId)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .HasColumnName("PAYMENT_STATUS")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("STUDENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.FeePayment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEE_PAYMENT_DEPARTMENT_FK");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FeePayment)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEE_PAYMENT_STUDENT_FK");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.ModuleCode)
                    .HasName("MODULE_PK");

                entity.ToTable("MODULE");

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("MODULE_CODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreditHours)
                    .HasColumnName("CREDIT_HOURS")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasColumnName("MODULE_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModuleResult>(entity =>
            {
                entity.HasKey(e => new { e.AssignmentId, e.StudentId, e.ModuleCode, e.ResultId })
                    .HasName("MODULE_RESULT_PK");

                entity.ToTable("MODULE_RESULT");

                entity.Property(e => e.AssignmentId)
                    .HasColumnName("ASSIGNMENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .HasColumnName("STUDENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("MODULE_CODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ResultId)
                    .HasColumnName("RESULT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasColumnName("GRADE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.ModuleResult)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MODULE_RESULT_ASSIGNMENT_FK");

                entity.HasOne(d => d.StudentModule)
                    .WithMany(p => p.ModuleResult)
                    .HasForeignKey(d => new { d.StudentId, d.ModuleCode })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MODULE_RESULT_DEPARTMENT_FK");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("PERSON");

                entity.Property(e => e.PersonId)
                    .HasColumnName("PERSON_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasColumnName("CONTACT_NO")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dateofbirth)
                    .HasColumnName("DATEOFBIRTH")
                    .HasColumnType("DATE");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("GENDER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonAddress>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.AddressId })
                    .HasName("PERSON_ADDRESS_PK");

                entity.ToTable("PERSON_ADDRESS");

                entity.Property(e => e.PersonId)
                    .HasColumnName("PERSON_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddressId)
                    .HasColumnName("ADDRESS_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonAddress)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PERSON_ADDRESS_ADDRESS_FK");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddress)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PERSON_ADDRESS_PERSON_FK");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("STUDENT");

                entity.Property(e => e.StudentId)
                    .HasColumnName("STUDENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EnrolledYear)
                    .HasColumnName("ENROLLED_YEAR")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_PERSON_FK");
            });

            modelBuilder.Entity<StudentAttendence>(entity =>
            {
                entity.HasKey(e => e.AttendenceId)
                    .HasName("STUDENT_ATTENDENCE_PK");

                entity.ToTable("STUDENT_ATTENDENCE");

                entity.Property(e => e.AttendenceId)
                    .HasColumnName("ATTENDENCE_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Attendence)
                    .HasColumnName("ATTENDENCE")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasColumnName("STUDENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAttendence)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_ATTENDENCE_STUDENT_FK");
            });

            modelBuilder.Entity<StudentModule>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ModuleCode })
                    .HasName("STUDENT_MODULE_PK");

                entity.ToTable("STUDENT_MODULE");

                entity.Property(e => e.StudentId)
                    .HasColumnName("STUDENT_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("MODULE_CODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ModuleCodeNavigation)
                    .WithMany(p => p.StudentModule)
                    .HasForeignKey(d => d.ModuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_MODULE_MODULE_FK");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentModule)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_MODULE_STUDENT_FK");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("TEACHER");

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TEACHER_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasColumnName("QUALIFICATION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TeacherNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TEACHER_PERSON_FK");
            });

            modelBuilder.Entity<TeacherModule>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.ModuleCode })
                    .HasName("TEACHER_MODULE_PK");

                entity.ToTable("TEACHER_MODULE");

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TEACHER_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("MODULE_CODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ModuleCodeNavigation)
                    .WithMany(p => p.TeacherModule)
                    .HasForeignKey(d => d.ModuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TEACHER_MODULE_MODULE_FK");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherModule)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TEACHER_MODULE_TEACHER_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
