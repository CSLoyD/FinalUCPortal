using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UCPortal.DatabaseEntities.Models
{
    public partial class UCOnlinePortalContext : DbContext
    {
        public UCOnlinePortalContext()
        {
        }

        public UCOnlinePortalContext(DbContextOptions<UCOnlinePortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdviserSection> AdviserSections { get; set; }
        public virtual DbSet<AssessmentBe> AssessmentBes { get; set; }
        public virtual DbSet<AssessmentCl> AssessmentCls { get; set; }
        public virtual DbSet<AssessmentSh> AssessmentShes { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<ContactAddress> ContactAddresses { get; set; }
        public virtual DbSet<CoreBe> CoreBes { get; set; }
        public virtual DbSet<CoreSh> CoreShes { get; set; }
        public virtual DbSet<CourseList> CourseLists { get; set; }
        public virtual DbSet<Curriculum> Curricula { get; set; }
        public virtual DbSet<Equivalence> Equivalences { get; set; }
        public virtual DbSet<ExamPromissory> ExamPromissories { get; set; }
        public virtual DbSet<FamilyInfo> FamilyInfos { get; set; }
        public virtual DbSet<GradeEvaluation> GradeEvaluations { get; set; }
        public virtual DbSet<GradeEvaluationBe> GradeEvaluationBes { get; set; }
        public virtual DbSet<GradesBe> GradesBes { get; set; }
        public virtual DbSet<GradesCl> GradesCls { get; set; }
        public virtual DbSet<GradesSh> GradesShes { get; set; }
        public virtual DbSet<LoginInfo> LoginInfos { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Oenrp> Oenrps { get; set; }
        public virtual DbSet<Ostsp> Ostsps { get; set; }
        public virtual DbSet<Promissory> Promissories { get; set; }
        public virtual DbSet<RequestSchedule> RequestSchedules { get; set; }
        public virtual DbSet<Requisite> Requisites { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<SchedulesBe> SchedulesBes { get; set; }
        public virtual DbSet<SchoolInfo> SchoolInfos { get; set; }
        public virtual DbSet<StudentInfo> StudentInfos { get; set; }
        public virtual DbSet<StudentRequest> StudentRequests { get; set; }
        public virtual DbSet<SubjectInfo> SubjectInfos { get; set; }
        public virtual DbSet<TeacherDept> TeacherDepts { get; set; }
        public virtual DbSet<TmpLogin> TmpLogins { get; set; }
        public virtual DbSet<_212assessmentBe> _212assessmentBes { get; set; }
        public virtual DbSet<_212assessmentCl> _212assessmentCls { get; set; }
        public virtual DbSet<_212assessmentSh> _212assessmentShes { get; set; }
        public virtual DbSet<_212attachment> _212attachments { get; set; }
        public virtual DbSet<_212contactAddress> _212contactAddresses { get; set; }
        public virtual DbSet<_212coreBe> _212coreBes { get; set; }
        public virtual DbSet<_212coreSh> _212coreShes { get; set; }
        public virtual DbSet<_212examPromissory> _212examPromissories { get; set; }
        public virtual DbSet<_212familyInfo> _212familyInfos { get; set; }
        public virtual DbSet<_212gradesBe> _212gradesBes { get; set; }
        public virtual DbSet<_212gradesCl> _212gradesCls { get; set; }
        public virtual DbSet<_212gradesSh> _212gradesShes { get; set; }
        public virtual DbSet<_212oenrp> _212oenrps { get; set; }
        public virtual DbSet<_212ostsp> _212ostsps { get; set; }
        public virtual DbSet<_212promissory> _212promissories { get; set; }
        public virtual DbSet<_212schedule> _212schedules { get; set; }
        public virtual DbSet<_212schoolInfo> _212schoolInfos { get; set; }
        public virtual DbSet<_212studentInfo> _212studentInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADMIN-PC\\SQLEXPRESS;Database=UCOnlinePortal;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdviserSection>(entity =>
            {
                entity.HasKey(e => e.SectionAdId);

                entity.ToTable("adviser_section");

                entity.Property(e => e.SectionAdId).HasColumnName("section_ad_id");

                entity.Property(e => e.ActiveTerm)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.Instructor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("instructor");

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");
            });

            modelBuilder.Entity<AssessmentBe>(entity =>
            {
                entity.HasKey(e => e.AssessBeId);

                entity.ToTable("assessment_be");

                entity.Property(e => e.AssessBeId).HasColumnName("assess_be_id");

                entity.Property(e => e.ActiveTerm)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMiscOthers).HasColumnName("fee_misc_others");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTotal).HasColumnName("fee_total");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.StudShare).HasColumnName("stud_share");

                entity.Property(e => e.StudShareBal).HasColumnName("stud_share_bal");

                entity.Property(e => e.TotalDue).HasColumnName("total_due");
            });

            modelBuilder.Entity<AssessmentCl>(entity =>
            {
                entity.HasKey(e => e.AssessClId);

                entity.ToTable("assessment_cl");

                entity.Property(e => e.AssessClId).HasColumnName("assess_cl_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AdjustmentCredit).HasColumnName("adjustment_credit");

                entity.Property(e => e.AdjustmentDebit).HasColumnName("adjustment_debit");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.AssessTotal).HasColumnName("assess_total");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.ExcessPayment).HasColumnName("excess_payment");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMisc).HasColumnName("fee_misc");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<AssessmentSh>(entity =>
            {
                entity.HasKey(e => e.AssessShId);

                entity.ToTable("assessment_sh");

                entity.Property(e => e.AssessShId).HasColumnName("assess_sh_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AdjustmentCredit).HasColumnName("adjustment_credit");

                entity.Property(e => e.AdjustmentDebit).HasColumnName("adjustment_debit");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.ExcessPayment).HasColumnName("excess_payment");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMiscOthers).HasColumnName("fee_misc_others");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTotal).HasColumnName("fee_total");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.GovernmentSubsidy).HasColumnName("government_subsidy");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.StudShare).HasColumnName("stud_share");

                entity.Property(e => e.StudShareBal).HasColumnName("stud_share_bal");

                entity.Property(e => e.TotalDue).HasColumnName("total_due");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("attachments");

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("filename");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.ToTable("config");

                entity.Property(e => e.ConfigId).HasColumnName("config_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.ActiveTerms)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("active_terms");

                entity.Property(e => e.BasicEnd).HasColumnName("basic_end");

                entity.Property(e => e.BasicStart).HasColumnName("basic_start");

                entity.Property(e => e.CampusId).HasColumnName("campus_id");

                entity.Property(e => e.CampusLms)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("campus_lms");

                entity.Property(e => e.Final)
                    .HasColumnType("datetime")
                    .HasColumnName("final");

                entity.Property(e => e.Grade1Due)
                    .HasColumnType("datetime")
                    .HasColumnName("grade1_due");

                entity.Property(e => e.Grade2Due)
                    .HasColumnType("datetime")
                    .HasColumnName("grade2_due");

                entity.Property(e => e.IdYear).HasColumnName("id_year");

                entity.Property(e => e.Midterm)
                    .HasColumnType("datetime")
                    .HasColumnName("midterm");

                entity.Property(e => e.PermitCutoff).HasColumnName("permit_cutoff");

                entity.Property(e => e.Prelim)
                    .HasColumnType("datetime")
                    .HasColumnName("prelim");

                entity.Property(e => e.Semifinal)
                    .HasColumnType("datetime")
                    .HasColumnName("semifinal");

                entity.Property(e => e.Sequence).HasColumnName("sequence");
            });

            modelBuilder.Entity<ContactAddress>(entity =>
            {
                entity.HasKey(e => e.AddConId);

                entity.ToTable("contact_address");

                entity.Property(e => e.AddConId).HasColumnName("add_con_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.CBarangay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_barangay");

                entity.Property(e => e.CCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_city");

                entity.Property(e => e.CProvince)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_province");

                entity.Property(e => e.CStreet)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("c_street");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("facebook");

                entity.Property(e => e.Landline)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("landline");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.PBarangay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_barangay");

                entity.Property(e => e.PCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_city");

                entity.Property(e => e.PCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_country");

                entity.Property(e => e.PProvince)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_province");

                entity.Property(e => e.PStreet)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("p_street");

                entity.Property(e => e.PZip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("p_zip");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<CoreBe>(entity =>
            {
                entity.ToTable("core_be");

                entity.Property(e => e.CoreBeId).HasColumnName("core_be_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Alignment)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("alignment");

                entity.Property(e => e.Camaraderie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("camaraderie");

                entity.Property(e => e.Exam)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.Excellence)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("excellence");

                entity.Property(e => e.Innovation1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_1");

                entity.Property(e => e.Innovation2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_2");

                entity.Property(e => e.Innovation3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_3");

                entity.Property(e => e.Respect)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("respect");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<CoreSh>(entity =>
            {
                entity.ToTable("core_sh");

                entity.Property(e => e.CoreShId).HasColumnName("core_sh_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Alignment)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("alignment");

                entity.Property(e => e.Camaraderie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("camaraderie");

                entity.Property(e => e.Exam)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.Excellence)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("excellence");

                entity.Property(e => e.Innovation1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_1");

                entity.Property(e => e.Innovation2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_2");

                entity.Property(e => e.Innovation3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_3");

                entity.Property(e => e.Respect)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("respect");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<CourseList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("course_list");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.AdjustmentEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("adjustment_end");

                entity.Property(e => e.AdjustmentStart)
                    .HasColumnType("datetime")
                    .HasColumnName("adjustment_start");

                entity.Property(e => e.CourseAbbr)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_abbr");

                entity.Property(e => e.CourseActive).HasColumnName("course_active");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.CourseDepartment)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("course_department");

                entity.Property(e => e.CourseDepartmentAbbr)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_department_abbr");

                entity.Property(e => e.CourseDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("course_description");

                entity.Property(e => e.CourseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("course_id");

                entity.Property(e => e.CourseYearLimit).HasColumnName("course_year_limit");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.EnrollmentOpen).HasColumnName("enrollment_open");
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.HasKey(e => e.CurrId);

                entity.ToTable("curriculum");

                entity.Property(e => e.CurrId).HasColumnName("curr_id");

                entity.Property(e => e.IsDeployed).HasColumnName("isDeployed");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Equivalence>(entity =>
            {
                entity.HasKey(e => e.EquivalId);

                entity.ToTable("equivalence");

                entity.Property(e => e.EquivalId).HasColumnName("equival_id");

                entity.Property(e => e.EquivalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("equival_code");

                entity.Property(e => e.InternalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");
            });

            modelBuilder.Entity<ExamPromissory>(entity =>
            {
                entity.HasKey(e => e.ExamPromiId)
                    .HasName("PK_ExamPromissory");

                entity.ToTable("exam_promissory");

                entity.Property(e => e.ExamPromiId).HasColumnName("exam_promi_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.FinalPromiId).HasColumnName("final_promi_id");

                entity.Property(e => e.MidtermPromiId).HasColumnName("midterm_promi_id");

                entity.Property(e => e.PrelimPromiId).HasColumnName("prelim_promi_id");

                entity.Property(e => e.RequestFinal).HasColumnName("request_final");

                entity.Property(e => e.RequestFinalAmount).HasColumnName("request_final_amount");

                entity.Property(e => e.RequestFinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_final_date");

                entity.Property(e => e.RequestMidterm).HasColumnName("request_midterm");

                entity.Property(e => e.RequestMidtermAmount).HasColumnName("request_midterm_amount");

                entity.Property(e => e.RequestMidtermDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_midterm_date");

                entity.Property(e => e.RequestPrelim).HasColumnName("request_prelim");

                entity.Property(e => e.RequestPrelimAmount).HasColumnName("request_prelim_amount");

                entity.Property(e => e.RequestPrelimDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_prelim_date");

                entity.Property(e => e.RequestSemi).HasColumnName("request_semi");

                entity.Property(e => e.RequestSemiAmount).HasColumnName("request_semi_amount");

                entity.Property(e => e.RequestSemiDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_semi_date");

                entity.Property(e => e.SemiPromiId).HasColumnName("semi_promi_id");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<FamilyInfo>(entity =>
            {
                entity.ToTable("family_info");

                entity.Property(e => e.FamilyInfoId).HasColumnName("family_info_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.FatherContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("father_contact");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("father_name");

                entity.Property(e => e.FatherOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("father_occupation");

                entity.Property(e => e.GuardianContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("guardian_contact");

                entity.Property(e => e.GuardianName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("guardian_name");

                entity.Property(e => e.GuardianOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("guardian_occupation");

                entity.Property(e => e.MotherContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mother_contact");

                entity.Property(e => e.MotherName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("mother_name");

                entity.Property(e => e.MotherOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mother_occupation");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<GradeEvaluation>(entity =>
            {
                entity.HasKey(e => e.GradeEvalId);

                entity.ToTable("grade_evaluation");

                entity.Property(e => e.GradeEvalId).HasColumnName("grade_eval_id");

                entity.Property(e => e.FinalGrade)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("final_grade");

                entity.Property(e => e.IntCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("int_code");

                entity.Property(e => e.MidtermGrade)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("midterm_grade");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("term");
            });

            modelBuilder.Entity<GradeEvaluationBe>(entity =>
            {
                entity.HasKey(e => e.GradeEvalId);

                entity.ToTable("grade_evaluation_be");

                entity.Property(e => e.GradeEvalId).HasColumnName("grade_eval_id");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.Grade3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_3");

                entity.Property(e => e.Grade4)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_4");

                entity.Property(e => e.IntCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("int_code");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Term)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("term");
            });

            modelBuilder.Entity<GradesBe>(entity =>
            {
                entity.ToTable("grades_be");

                entity.Property(e => e.GradesBeId).HasColumnName("grades_be_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.Grade3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_3");

                entity.Property(e => e.Grade4)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_4");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<GradesCl>(entity =>
            {
                entity.ToTable("grades_cl");

                entity.Property(e => e.GradesClId).HasColumnName("grades_cl_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<GradesSh>(entity =>
            {
                entity.ToTable("grades_sh");

                entity.Property(e => e.GradesShId).HasColumnName("grades_sh_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<LoginInfo>(entity =>
            {
                entity.HasKey(e => e.CinfoId);

                entity.ToTable("login_info");

                entity.Property(e => e.CinfoId).HasColumnName("cinfo_id");

                entity.Property(e => e.AllowedUnits).HasColumnName("allowed_units");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthdate");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.CurrYear).HasColumnName("curr_year");

                entity.Property(e => e.Dept)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("facebook");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Mi)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("mi");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sex");

                entity.Property(e => e.StartTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("start_term");

                entity.Property(e => e.StudId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("suffix");

                entity.Property(e => e.Token)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.UserType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("user_type");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotifId)
                    .HasName("PK_212notification");

                entity.ToTable("notification");

                entity.Property(e => e.NotifId).HasColumnName("notif_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.NotifRead).HasColumnName("notif_read");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<Oenrp>(entity =>
            {
                entity.ToTable("oenrp");

                entity.Property(e => e.OenrpId).HasColumnName("oenrp_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.AdjustmentBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("adjustment_by");

                entity.Property(e => e.AdjustmentCount).HasColumnName("adjustment_count");

                entity.Property(e => e.AdjustmentOn)
                    .HasColumnType("datetime")
                    .HasColumnName("adjustment_on");

                entity.Property(e => e.ApprovedAcctg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_acctg");

                entity.Property(e => e.ApprovedAcctgOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_acctg_on");

                entity.Property(e => e.ApprovedCashier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_cashier");

                entity.Property(e => e.ApprovedCashierOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_cashier_on");

                entity.Property(e => e.ApprovedDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_dean");

                entity.Property(e => e.ApprovedDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_dean_on");

                entity.Property(e => e.ApprovedRegDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_reg_dean");

                entity.Property(e => e.ApprovedRegDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_reg_dean_on");

                entity.Property(e => e.ApprovedRegRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_reg_registrar");

                entity.Property(e => e.ApprovedRegRegistrarOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_reg_registrar_on");

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("classification");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.DisapprovedDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_dean");

                entity.Property(e => e.DisapprovedDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_dean_on");

                entity.Property(e => e.DisapprovedRegDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_reg_dean");

                entity.Property(e => e.DisapprovedRegDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_reg_dean_on");

                entity.Property(e => e.DisapprovedRegRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_reg_registrar");

                entity.Property(e => e.DisapprovedRegRegistrarOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_reg_registrar_on");

                entity.Property(e => e.EnrollmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("enrollment_date");

                entity.Property(e => e.Mdn)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.NeededPayment).HasColumnName("needed_payment");

                entity.Property(e => e.PromiPay).HasColumnName("promi_pay");

                entity.Property(e => e.RegisteredOn)
                    .HasColumnType("datetime")
                    .HasColumnName("registered_on");

                entity.Property(e => e.RequestDeblock).HasColumnName("request_deblock");

                entity.Property(e => e.RequestOverload).HasColumnName("request_overload");

                entity.Property(e => e.RequestPromissory).HasColumnName("request_promissory");

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.SubmittedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("submitted_on");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.Property(e => e.YearLevel).HasColumnName("year_level");
            });

            modelBuilder.Entity<Ostsp>(entity =>
            {
                entity.HasKey(e => e.StsId);

                entity.ToTable("ostsp");

                entity.Property(e => e.StsId).HasColumnName("sts_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.AdjustedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("adjusted_on");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<Promissory>(entity =>
            {
                entity.HasKey(e => e.PromiId);

                entity.ToTable("promissory");

                entity.Property(e => e.PromiId).HasColumnName("promi_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.PromiDate)
                    .HasColumnType("datetime")
                    .HasColumnName("promi_date");

                entity.Property(e => e.PromiMessage)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("promi_message");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<RequestSchedule>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("request_schedule");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.Days)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("days");

                entity.Property(e => e.InternalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.MTimeEnd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m_time_end");

                entity.Property(e => e.MTimeStart)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m_time_start");

                entity.Property(e => e.Mdn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.Rtype).HasColumnName("rtype");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.SplitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_code");

                entity.Property(e => e.SplitType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_name");

                entity.Property(e => e.TimeEnd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("time_start");
            });

            modelBuilder.Entity<Requisite>(entity =>
            {
                entity.ToTable("requisite");

                entity.Property(e => e.RequisiteId).HasColumnName("requisite_id");

                entity.Property(e => e.InternalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.RequisiteCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("requisite_code");

                entity.Property(e => e.RequisiteType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("requisite_type");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedules");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("days");

                entity.Property(e => e.Deployed).HasColumnName("deployed");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Instructor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor");

                entity.Property(e => e.Instructor2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor_2");

                entity.Property(e => e.InternalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.IsGened).HasColumnName("is_gened");

                entity.Property(e => e.MDays)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_days");

                entity.Property(e => e.MTimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_end");

                entity.Property(e => e.MTimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_start");

                entity.Property(e => e.MaxSize).HasColumnName("max_size");

                entity.Property(e => e.Mdn)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.OfficialEnrolled).HasColumnName("official_enrolled");

                entity.Property(e => e.PendingEnrolled).HasColumnName("pending_enrolled");

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("room");

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.SplitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_code");

                entity.Property(e => e.SplitType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sub_type");

                entity.Property(e => e.TimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_start");

                entity.Property(e => e.Units).HasColumnName("units");
            });

            modelBuilder.Entity<SchedulesBe>(entity =>
            {
                entity.HasKey(e => e.ScheduleBeId)
                    .HasName("PK_be_schedules");

                entity.ToTable("schedules_be");

                entity.Property(e => e.ScheduleBeId).HasColumnName("schedule_be_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("days");

                entity.Property(e => e.Deployed).HasColumnName("deployed");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Instructor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor");

                entity.Property(e => e.Instructor2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor_2");

                entity.Property(e => e.InternalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.IsGened).HasColumnName("is_gened");

                entity.Property(e => e.MDays)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_days");

                entity.Property(e => e.MTimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_end");

                entity.Property(e => e.MTimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_start");

                entity.Property(e => e.MaxSize).HasColumnName("max_size");

                entity.Property(e => e.Mdn)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.OfficialEnrolled).HasColumnName("official_enrolled");

                entity.Property(e => e.PendingEnrolled).HasColumnName("pending_enrolled");

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("room");

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.SplitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_code");

                entity.Property(e => e.SplitType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sub_type");

                entity.Property(e => e.TimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_start");

                entity.Property(e => e.Units).HasColumnName("units");
            });

            modelBuilder.Entity<SchoolInfo>(entity =>
            {
                entity.ToTable("school_info");

                entity.Property(e => e.SchoolInfoId).HasColumnName("school_info_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.ColCourse)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("col_course");

                entity.Property(e => e.ColLastYear).HasColumnName("col_last_year");

                entity.Property(e => e.ColName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("col_name");

                entity.Property(e => e.ColYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("col_year");

                entity.Property(e => e.ElemEscSchoolId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_esc_school_id");

                entity.Property(e => e.ElemEscStudentId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_esc_student_id");

                entity.Property(e => e.ElemLastYear).HasColumnName("elem_last_year");

                entity.Property(e => e.ElemLrnNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_lrn_no");

                entity.Property(e => e.ElemName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("elem_name");

                entity.Property(e => e.ElemType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_type");

                entity.Property(e => e.ElemYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("elem_year");

                entity.Property(e => e.SecEscSchoolId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_esc_school_id");

                entity.Property(e => e.SecEscStudentId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_esc_student_id");

                entity.Property(e => e.SecLastStrand)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sec_last_strand");

                entity.Property(e => e.SecLastYear).HasColumnName("sec_last_year");

                entity.Property(e => e.SecLrnNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_lrn_no");

                entity.Property(e => e.SecName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sec_name");

                entity.Property(e => e.SecType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_type");

                entity.Property(e => e.SecYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("sec_year");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.HasKey(e => e.StudInfoId);

                entity.ToTable("student_info");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");

                entity.Property(e => e.ActiveTerm)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("active_term");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("birth_place");

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("classification");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_updated");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Mdn)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Religion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("religion");

                entity.Property(e => e.StartTerm).HasColumnName("start_term");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("suffix");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.YearLevel).HasColumnName("year_level");
            });

            modelBuilder.Entity<StudentRequest>(entity =>
            {
                entity.HasKey(e => e.StudRequestId);

                entity.ToTable("student_request");

                entity.Property(e => e.StudRequestId).HasColumnName("stud_request_id");

                entity.Property(e => e.InternalCode)
                    .HasMaxLength(10)
                    .HasColumnName("internal_code")
                    .IsFixedLength(true);

                entity.Property(e => e.StudId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<SubjectInfo>(entity =>
            {
                entity.HasKey(e => e.SubInfoId);

                entity.ToTable("subject_info");

                entity.Property(e => e.SubInfoId).HasColumnName("sub_info_id");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.CurriculumYear).HasColumnName("curriculum_year");

                entity.Property(e => e.Descr1)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descr_1");

                entity.Property(e => e.Descr2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descr_2");

                entity.Property(e => e.InternalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.Semester).HasColumnName("semester");

                entity.Property(e => e.SplitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_code");

                entity.Property(e => e.SplitType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_type");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("subject_name");

                entity.Property(e => e.SubjectType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("subject_type");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.Property(e => e.YearLevel).HasColumnName("year_level");
            });

            modelBuilder.Entity<TeacherDept>(entity =>
            {
                entity.ToTable("teacher_dept");

                entity.Property(e => e.TeacherDeptId).HasColumnName("teacher_dept_id");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("id_number");
            });

            modelBuilder.Entity<TmpLogin>(entity =>
            {
                entity.ToTable("tmp_login");

                entity.Property(e => e.TmpLoginId).HasColumnName("tmp_login_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<_212assessmentBe>(entity =>
            {
                entity.HasKey(e => e.AssessBeId);

                entity.ToTable("212assessment_be");

                entity.Property(e => e.AssessBeId).HasColumnName("assess_be_id");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMiscOthers).HasColumnName("fee_misc_others");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTotal).HasColumnName("fee_total");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.StudShare).HasColumnName("stud_share");

                entity.Property(e => e.StudShareBal).HasColumnName("stud_share_bal");

                entity.Property(e => e.TotalDue).HasColumnName("total_due");
            });

            modelBuilder.Entity<_212assessmentCl>(entity =>
            {
                entity.HasKey(e => e.AssessClId);

                entity.ToTable("212assessment_cl");

                entity.Property(e => e.AssessClId).HasColumnName("assess_cl_id");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AdjustmentCredit).HasColumnName("adjustment_credit");

                entity.Property(e => e.AdjustmentDebit).HasColumnName("adjustment_debit");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.AssessTotal).HasColumnName("assess_total");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.ExcessPayment).HasColumnName("excess_payment");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMisc).HasColumnName("fee_misc");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212assessmentSh>(entity =>
            {
                entity.HasKey(e => e.AssessShId);

                entity.ToTable("212assessment_sh");

                entity.Property(e => e.AssessShId).HasColumnName("assess_sh_id");

                entity.Property(e => e.Adjustment).HasColumnName("adjustment");

                entity.Property(e => e.AdjustmentCredit).HasColumnName("adjustment_credit");

                entity.Property(e => e.AdjustmentDebit).HasColumnName("adjustment_debit");

                entity.Property(e => e.AmountDue).HasColumnName("amount_due");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Exam)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.ExcessPayment).HasColumnName("excess_payment");

                entity.Property(e => e.FeeLab).HasColumnName("fee_lab");

                entity.Property(e => e.FeeMiscOthers).HasColumnName("fee_misc_others");

                entity.Property(e => e.FeeReg).HasColumnName("fee_reg");

                entity.Property(e => e.FeeTotal).HasColumnName("fee_total");

                entity.Property(e => e.FeeTuition).HasColumnName("fee_tuition");

                entity.Property(e => e.GovernmentSubsidy).HasColumnName("government_subsidy");

                entity.Property(e => e.OldAccount).HasColumnName("old_account");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.StudShare).HasColumnName("stud_share");

                entity.Property(e => e.StudShareBal).HasColumnName("stud_share_bal");

                entity.Property(e => e.TotalDue).HasColumnName("total_due");
            });

            modelBuilder.Entity<_212attachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.ToTable("212attachments");

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.Acknowledged).HasColumnName("acknowledged");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("filename");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<_212contactAddress>(entity =>
            {
                entity.HasKey(e => e.AddConId);

                entity.ToTable("212contact_address");

                entity.Property(e => e.AddConId).HasColumnName("add_con_id");

                entity.Property(e => e.CBarangay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_barangay");

                entity.Property(e => e.CCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_city");

                entity.Property(e => e.CProvince)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("c_province");

                entity.Property(e => e.CStreet)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("c_street");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("facebook");

                entity.Property(e => e.Landline)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("landline");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.PBarangay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_barangay");

                entity.Property(e => e.PCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_city");

                entity.Property(e => e.PCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_country");

                entity.Property(e => e.PProvince)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("p_province");

                entity.Property(e => e.PStreet)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("p_street");

                entity.Property(e => e.PZip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("p_zip");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<_212coreBe>(entity =>
            {
                entity.HasKey(e => e.CoreBeId);

                entity.ToTable("212core_be");

                entity.Property(e => e.CoreBeId).HasColumnName("core_be_id");

                entity.Property(e => e.Alignment)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("alignment");

                entity.Property(e => e.Camaraderie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("camaraderie");

                entity.Property(e => e.Exam)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.Excellence)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("excellence");

                entity.Property(e => e.Innovation1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_1");

                entity.Property(e => e.Innovation2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_2");

                entity.Property(e => e.Innovation3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_3");

                entity.Property(e => e.Respect)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("respect");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212coreSh>(entity =>
            {
                entity.HasKey(e => e.CoreShId);

                entity.ToTable("212core_sh");

                entity.Property(e => e.CoreShId).HasColumnName("core_sh_id");

                entity.Property(e => e.Alignment)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("alignment");

                entity.Property(e => e.Camaraderie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("camaraderie");

                entity.Property(e => e.Exam)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("exam");

                entity.Property(e => e.Excellence)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("excellence");

                entity.Property(e => e.Innovation1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_1");

                entity.Property(e => e.Innovation2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_2");

                entity.Property(e => e.Innovation3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("innovation_3");

                entity.Property(e => e.Respect)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("respect");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212examPromissory>(entity =>
            {
                entity.HasKey(e => e.ExamPromiId)
                    .HasName("PK_212ExamPromissory");

                entity.ToTable("212exam_promissory");

                entity.Property(e => e.ExamPromiId).HasColumnName("exam_promi_id");

                entity.Property(e => e.FinalPromiId).HasColumnName("final_promi_id");

                entity.Property(e => e.MidtermPromiId).HasColumnName("midterm_promi_id");

                entity.Property(e => e.PrelimPromiId).HasColumnName("prelim_promi_id");

                entity.Property(e => e.RequestFinal).HasColumnName("request_final");

                entity.Property(e => e.RequestFinalAmount).HasColumnName("request_final_amount");

                entity.Property(e => e.RequestFinalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_final_date");

                entity.Property(e => e.RequestMidterm).HasColumnName("request_midterm");

                entity.Property(e => e.RequestMidtermAmount).HasColumnName("request_midterm_amount");

                entity.Property(e => e.RequestMidtermDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_midterm_date");

                entity.Property(e => e.RequestPrelim).HasColumnName("request_prelim");

                entity.Property(e => e.RequestPrelimAmount).HasColumnName("request_prelim_amount");

                entity.Property(e => e.RequestPrelimDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_prelim_date");

                entity.Property(e => e.RequestSemi).HasColumnName("request_semi");

                entity.Property(e => e.RequestSemiAmount).HasColumnName("request_semi_amount");

                entity.Property(e => e.RequestSemiDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_semi_date");

                entity.Property(e => e.SemiPromiId).HasColumnName("semi_promi_id");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212familyInfo>(entity =>
            {
                entity.HasKey(e => e.FamilyInfoId);

                entity.ToTable("212family_info");

                entity.Property(e => e.FamilyInfoId).HasColumnName("family_info_id");

                entity.Property(e => e.FatherContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("father_contact");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("father_name");

                entity.Property(e => e.FatherOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("father_occupation");

                entity.Property(e => e.GuardianContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("guardian_contact");

                entity.Property(e => e.GuardianName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("guardian_name");

                entity.Property(e => e.GuardianOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("guardian_occupation");

                entity.Property(e => e.MotherContact)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mother_contact");

                entity.Property(e => e.MotherName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("mother_name");

                entity.Property(e => e.MotherOccupation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mother_occupation");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<_212gradesBe>(entity =>
            {
                entity.HasKey(e => e.GradesBeId);

                entity.ToTable("212grades_be");

                entity.Property(e => e.GradesBeId).HasColumnName("grades_be_id");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.Grade3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_3");

                entity.Property(e => e.Grade4)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_4");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212gradesCl>(entity =>
            {
                entity.HasKey(e => e.GradesClId);

                entity.ToTable("212grades_cl");

                entity.Property(e => e.GradesClId).HasColumnName("grades_cl_id");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212gradesSh>(entity =>
            {
                entity.HasKey(e => e.GradesShId);

                entity.ToTable("212grades_sh");

                entity.Property(e => e.GradesShId).HasColumnName("grades_sh_id");

                entity.Property(e => e.Dte)
                    .HasColumnType("datetime")
                    .HasColumnName("dte");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Grade1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_1");

                entity.Property(e => e.Grade2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("grade_2");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212oenrp>(entity =>
            {
                entity.HasKey(e => e.OenrpId);

                entity.ToTable("212oenrp");

                entity.Property(e => e.OenrpId).HasColumnName("oenrp_id");

                entity.Property(e => e.AdjustmentBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("adjustment_by");

                entity.Property(e => e.AdjustmentCount).HasColumnName("adjustment_count");

                entity.Property(e => e.AdjustmentOn)
                    .HasColumnType("datetime")
                    .HasColumnName("adjustment_on");

                entity.Property(e => e.ApprovedAcctg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_acctg");

                entity.Property(e => e.ApprovedAcctgOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_acctg_on");

                entity.Property(e => e.ApprovedCashier)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_cashier");

                entity.Property(e => e.ApprovedCashierOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_cashier_on");

                entity.Property(e => e.ApprovedDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_dean");

                entity.Property(e => e.ApprovedDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_dean_on");

                entity.Property(e => e.ApprovedRegDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_reg_dean");

                entity.Property(e => e.ApprovedRegDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_reg_dean_on");

                entity.Property(e => e.ApprovedRegRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("approved_reg_registrar");

                entity.Property(e => e.ApprovedRegRegistrarOn)
                    .HasColumnType("datetime")
                    .HasColumnName("approved_reg_registrar_on");

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("classification");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.DisapprovedDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_dean");

                entity.Property(e => e.DisapprovedDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_dean_on");

                entity.Property(e => e.DisapprovedRegDean)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_reg_dean");

                entity.Property(e => e.DisapprovedRegDeanOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_reg_dean_on");

                entity.Property(e => e.DisapprovedRegRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("disapproved_reg_registrar");

                entity.Property(e => e.DisapprovedRegRegistrarOn)
                    .HasColumnType("datetime")
                    .HasColumnName("disapproved_reg_registrar_on");

                entity.Property(e => e.EnrollmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("enrollment_date");

                entity.Property(e => e.Mdn)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.NeededPayment).HasColumnName("needed_payment");

                entity.Property(e => e.PromiPay).HasColumnName("promi_pay");

                entity.Property(e => e.RegisteredOn)
                    .HasColumnType("datetime")
                    .HasColumnName("registered_on");

                entity.Property(e => e.RequestDeblock).HasColumnName("request_deblock");

                entity.Property(e => e.RequestOverload).HasColumnName("request_overload");

                entity.Property(e => e.RequestPromissory).HasColumnName("request_promissory");

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.SubmittedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("submitted_on");

                entity.Property(e => e.Units).HasColumnName("units");

                entity.Property(e => e.YearLevel).HasColumnName("year_level");
            });

            modelBuilder.Entity<_212ostsp>(entity =>
            {
                entity.HasKey(e => e.StsId);

                entity.ToTable("212ostsp");

                entity.Property(e => e.StsId).HasColumnName("sts_id");

                entity.Property(e => e.AdjustedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("adjusted_on");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212promissory>(entity =>
            {
                entity.HasKey(e => e.PromiId);

                entity.ToTable("212promissory");

                entity.Property(e => e.PromiId).HasColumnName("promi_id");

                entity.Property(e => e.PromiDate)
                    .HasColumnType("datetime")
                    .HasColumnName("promi_date");

                entity.Property(e => e.PromiMessage)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("promi_message");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");
            });

            modelBuilder.Entity<_212schedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("212schedules");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("days");

                entity.Property(e => e.Deployed).HasColumnName("deployed");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EdpCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("edp_code");

                entity.Property(e => e.Instructor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor");

                entity.Property(e => e.Instructor2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("instructor_2");

                entity.Property(e => e.InternalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("internal_code");

                entity.Property(e => e.IsGened).HasColumnName("is_gened");

                entity.Property(e => e.MDays)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_days");

                entity.Property(e => e.MTimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_end");

                entity.Property(e => e.MTimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m_time_start");

                entity.Property(e => e.MaxSize).HasColumnName("max_size");

                entity.Property(e => e.Mdn)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.OfficialEnrolled).HasColumnName("official_enrolled");

                entity.Property(e => e.PendingEnrolled).HasColumnName("pending_enrolled");

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("room");

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("section");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.SplitCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_code");

                entity.Property(e => e.SplitType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("split_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("sub_type");

                entity.Property(e => e.TimeEnd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("time_start");

                entity.Property(e => e.Units).HasColumnName("units");
            });

            modelBuilder.Entity<_212schoolInfo>(entity =>
            {
                entity.HasKey(e => e.SchoolInfoId);

                entity.ToTable("212school_info");

                entity.Property(e => e.SchoolInfoId).HasColumnName("school_info_id");

                entity.Property(e => e.ColCourse)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("col_course");

                entity.Property(e => e.ColLastYear).HasColumnName("col_last_year");

                entity.Property(e => e.ColName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("col_name");

                entity.Property(e => e.ColYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("col_year");

                entity.Property(e => e.ElemEscSchoolId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_esc_school_id");

                entity.Property(e => e.ElemEscStudentId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_esc_student_id");

                entity.Property(e => e.ElemLastYear).HasColumnName("elem_last_year");

                entity.Property(e => e.ElemLrnNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_lrn_no");

                entity.Property(e => e.ElemName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("elem_name");

                entity.Property(e => e.ElemType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("elem_type");

                entity.Property(e => e.ElemYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("elem_year");

                entity.Property(e => e.SecEscSchoolId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_esc_school_id");

                entity.Property(e => e.SecEscStudentId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_esc_student_id");

                entity.Property(e => e.SecLastStrand)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sec_last_strand");

                entity.Property(e => e.SecLastYear).HasColumnName("sec_last_year");

                entity.Property(e => e.SecLrnNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_lrn_no");

                entity.Property(e => e.SecName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("sec_name");

                entity.Property(e => e.SecType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sec_type");

                entity.Property(e => e.SecYear)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("sec_year");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");
            });

            modelBuilder.Entity<_212studentInfo>(entity =>
            {
                entity.HasKey(e => e.StudInfoId);

                entity.ToTable("212student_info");

                entity.Property(e => e.StudInfoId).HasColumnName("stud_info_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("birth_place");

                entity.Property(e => e.Classification)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("classification");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("course_code");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_updated");

                entity.Property(e => e.Dept)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Mdn)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mdn");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Religion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("religion");

                entity.Property(e => e.StartTerm).HasColumnName("start_term");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("stud_id");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("suffix");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.YearLevel).HasColumnName("year_level");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
