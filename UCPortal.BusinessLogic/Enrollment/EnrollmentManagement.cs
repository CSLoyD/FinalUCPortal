using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCPortal.DatabaseEntities.Models;
using UCPortal.DTO.Request;
using UCPortal.DTO.Response;
using UCPortal.EmailHandler;
using UCPortal.EmailHandler.Handlers;
using UCPortal.RequestResponse.Enums;
using UCPortal.RequestResponse.Literals;
using UCPortal.Utils;

namespace UCPortal.BusinessLogic.Enrollment
{
    public class EnrollmentManagement : IEnrollmentManagement
    {
        private UCOnlinePortalContext _ucOnlinePortalContext;
        private ISMTPHandler _emailHandler;
        private static Random random = new Random();
        public EnrollmentManagement(UCOnlinePortalContext ucOnlinePortalContext, ISMTPHandler emailHandler)
        {
            _ucOnlinePortalContext = ucOnlinePortalContext;
            _emailHandler = emailHandler;
        }

        /*
         * Method to add new student record
        */
        public RegistrationResponse RegisterStudent(RegistrationRequest registrationRequest)
        {
            //Generate random number for Token
            Random generator = new Random();
            String token = generator.Next(0, 1000000).ToString("D6");

            bool hasError = false;

            try
            {
                //Create new student object
                StudentInfo newStudent = new StudentInfo
                {
                    CourseCode = registrationRequest.student_info.course,
                    YearLevel = registrationRequest.student_info.year_level,
                    Mdn = registrationRequest.student_info.mdn,
                    FirstName = registrationRequest.student_info.first_name,
                    LastName = registrationRequest.student_info.last_name,
                    MiddleName = registrationRequest.student_info.middle_name,
                    Suffix = registrationRequest.student_info.suffix,
                    Gender = registrationRequest.student_info.gender,
                    Status = registrationRequest.student_info.status,
                    Nationality = registrationRequest.student_info.nationality,
                    BirthDate = DateTime.ParseExact(registrationRequest.student_info.birthdate, "yyyy-MM-dd", null),
                    BirthPlace = registrationRequest.student_info.birthplace,
                    Religion = registrationRequest.student_info.religion,
                    DateCreated = DateTime.Now.Date,
                    DateUpdated = DateTime.Now.Date,
                    StartTerm = (short)registrationRequest.student_info.start_term,
                    IsVerified = 0,
                    Token = token,
                    Classification = registrationRequest.student_info.classification,
                    Dept = registrationRequest.student_info.dept,
                    ActiveTerm = registrationRequest.active_term
                };

                //Insert to table
                _ucOnlinePortalContext.StudentInfos.Add(newStudent);
                _ucOnlinePortalContext.SaveChanges();

                //Save primary id which will be used in linking to different tables
                int stud_info_id = newStudent.StudInfoId;

                //Update id number to stud_info_id for linking
                var newStudentRecord = _ucOnlinePortalContext.StudentInfos.Where(x => x.StudInfoId == stud_info_id).FirstOrDefault();
                newStudentRecord.StudId = stud_info_id.ToString();

                //Temporarily use primary id for student id
                _ucOnlinePortalContext.StudentInfos.Update(newStudentRecord);
                _ucOnlinePortalContext.SaveChanges();

                //Create new contact & address object
                ContactAddress newContactAddress = new ContactAddress
                {
                    StudInfoId = (short)stud_info_id,
                    PCountry = registrationRequest.address_contact.pcountry,
                    PProvince = registrationRequest.address_contact.pprovince,
                    PCity = registrationRequest.address_contact.pcity,
                    PBarangay = registrationRequest.address_contact.pbarangay,
                    PStreet = registrationRequest.address_contact.pstreet,
                    PZip = registrationRequest.address_contact.pzip,
                    CProvince = registrationRequest.address_contact.cprovince,
                    CCity = registrationRequest.address_contact.ccity,
                    CBarangay = registrationRequest.address_contact.cbarangay,
                    CStreet = registrationRequest.address_contact.cstreet,
                    Mobile = registrationRequest.address_contact.mobile,
                    Landline = registrationRequest.address_contact.landline,
                    Email = registrationRequest.address_contact.email,
                    Facebook = registrationRequest.address_contact.facebook,
                    ActiveTerm = registrationRequest.active_term
                };

                //Create new family info object
                FamilyInfo newFamilyInfo = new FamilyInfo
                {
                    StudInfoId = (short)stud_info_id,
                    FatherName = registrationRequest.family_info.father_name,
                    FatherContact = registrationRequest.family_info.father_contact,
                    FatherOccupation = registrationRequest.family_info.father_occupation,
                    MotherName = registrationRequest.family_info.mother_name,
                    MotherContact = registrationRequest.family_info.mother_contact,
                    MotherOccupation = registrationRequest.family_info.mother_occupation,
                    GuardianName = registrationRequest.family_info.guardian_name,
                    GuardianContact = registrationRequest.family_info.guardian_contact,
                    GuardianOccupation = registrationRequest.family_info.guardian_occupation,
                    ActiveTerm = registrationRequest.active_term
                };

                //Create new school info object
                SchoolInfo newSchoolInfo = new SchoolInfo
                {
                    StudInfoId = (short)stud_info_id,
                    ElemName = registrationRequest.school_info.elem_name,
                    ElemYear = registrationRequest.school_info.elem_year,
                    ElemLastYear = (short)registrationRequest.school_info.elem_last_year,
                    ElemType = registrationRequest.school_info.elem_type,
                    ElemLrnNo = registrationRequest.school_info.elem_lrn_number,
                    ElemEscSchoolId = registrationRequest.school_info.elem_esc_school_id,
                    ElemEscStudentId = registrationRequest.school_info.elem_esc_student_id,
                    SecName = registrationRequest.school_info.sec_name,
                    SecYear = registrationRequest.school_info.sec_year,
                    SecLastYear = (short)registrationRequest.school_info.sec_last_year,
                    SecLastStrand = registrationRequest.school_info.sec_last_strand,
                    SecType = registrationRequest.school_info.sec_type,
                    SecLrnNo = registrationRequest.school_info.sec_lrn_number,
                    SecEscSchoolId = registrationRequest.school_info.sec_esc_school_id,
                    SecEscStudentId = registrationRequest.school_info.sec_esc_student_id,
                    ColName = registrationRequest.school_info.col_name,
                    ColYear = registrationRequest.school_info.col_year,
                    ColCourse = registrationRequest.school_info.col_course,
                    ColLastYear = (short)registrationRequest.school_info.col_last_year,
                    ActiveTerm = registrationRequest.active_term
                };

                foreach (RegistrationRequest.Attachment attachment in registrationRequest.attachment)
                {
                    Attachment newAttachment = new Attachment
                    {
                        StudId = stud_info_id.ToString(),
                        Email = attachment.email,
                        Filename = attachment.filename,
                        Type = attachment.type,
                        Acknowledged = 0,
                        ActiveTerm = registrationRequest.active_term
                    };

                    _ucOnlinePortalContext.Attachments.Add(newAttachment);
                }
                //Add to tables
                _ucOnlinePortalContext.ContactAddresses.Add(newContactAddress);
                _ucOnlinePortalContext.FamilyInfos.Add(newFamilyInfo);
                _ucOnlinePortalContext.SchoolInfos.Add(newSchoolInfo);

                //Save Changes
                _ucOnlinePortalContext.SaveChanges();


                //Add OENRP
                Oenrp newStudentOenrp = new Oenrp
                {
                    StudId = stud_info_id.ToString(),
                    YearLevel = (short)registrationRequest.student_info.year_level,
                    CourseCode = registrationRequest.student_info.course,
                    RegisteredOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Units = 0,
                    Classification = registrationRequest.student_info.classification,
                    Dept = registrationRequest.student_info.dept,
                    Status = registrationRequest.student_info.classification == "S" || registrationRequest.student_info.classification == "T" ? (short)EnrollmentStatus.SUBJECT_EVALUATION_BY_DEAN : (short)EnrollmentStatus.REGISTERED,
                    AdjustmentCount = 1,
                    RequestDeblock = 0,
                    RequestOverload = 0,
                    NeededPayment = 0,
                    RequestPromissory = 0,
                    PromiPay = 0,
                    ActiveTerm = registrationRequest.active_term
                };

                //Save OENRP
                _ucOnlinePortalContext.Oenrps.Add(newStudentOenrp);
                _ucOnlinePortalContext.SaveChanges();

                //Checkfirst if name has similarity
                String sourceName = registrationRequest.student_info.first_name.Trim() + registrationRequest.student_info.last_name.Trim() + (registrationRequest.student_info.middle_name.Equals(String.Empty) ? "" : registrationRequest.student_info.middle_name.Substring(0, 1));
                //Compare and find 
                var checkIfNameExistOrlike = (from loginInfo in _ucOnlinePortalContext.LoginInfos.AsEnumerable()
                                              where Utils.Function.CalculateSimilarity(loginInfo.FirstName.Trim() + loginInfo.LastName.Trim(), sourceName) > 0.75
                                              select loginInfo);


                var result = checkIfNameExistOrlike.ToList();

                if (result == null || result.Count == 0)
                {
                    newStudentOenrp.ApprovedRegRegistrar = "AUTO-APPROVE";
                    newStudentOenrp.ApprovedRegRegistrarOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    newStudentOenrp.Status = registrationRequest.student_info.classification == "S" || registrationRequest.student_info.classification == "T" ? (short)EnrollmentStatus.SUBJECT_EVALUATION_BY_DEAN : (short)EnrollmentStatus.REGISTERED;
                }


                //Insert new record to loginInfo but with No Id Numbers first
                LoginInfo newLogin = new LoginInfo
                {
                    StudId = stud_info_id.ToString(),
                    LastName = registrationRequest.student_info.last_name,
                    FirstName = registrationRequest.student_info.first_name,
                    Mi = registrationRequest.student_info.middle_name.Equals(String.Empty) ? "" : registrationRequest.student_info.middle_name.Substring(0, 1),
                    Suffix = registrationRequest.student_info.suffix,
                    StartTerm = registrationRequest.student_info.start_term.ToString(),
                    Password = Utils.Function.EncodeBase64(registrationRequest.student_info.password),
                    Dept = registrationRequest.student_info.dept,
                    Year = (short)registrationRequest.student_info.year_level,
                    CourseCode = registrationRequest.student_info.course,
                    Sex = registrationRequest.student_info.gender,
                    MobileNumber = registrationRequest.address_contact.mobile,
                    Email = registrationRequest.address_contact.email,
                    Birthdate = DateTime.ParseExact(registrationRequest.student_info.birthdate, "yyyy-MM-dd", null),
                    Facebook = registrationRequest.address_contact.facebook,
                    IsVerified = 1,
                    IsBlocked = 0,
                    UserType = "STUDENT",
                    Token = token
                };

                //Save Login Info
                _ucOnlinePortalContext.LoginInfos.Add(newLogin);
                _ucOnlinePortalContext.SaveChanges();

                //Create Notification
                Notification newNotification = new Notification
                {
                    StudId = stud_info_id.ToString(),
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.REGISTERED,
                    NotifRead = 0,
                    ActiveTerm = registrationRequest.active_term
                };

                //Save Notification
                _ucOnlinePortalContext.Notifications.Add(newNotification);
                _ucOnlinePortalContext.SaveChanges();

                if (result == null || result.Count == 0)
                {
                    //Create Notification
                    newNotification = new Notification
                    {
                        StudId = stud_info_id.ToString(),
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVED_REGISTRATION_REGISTRAR,
                        NotifRead = 0,
                        ActiveTerm = registrationRequest.active_term
                    };

                    //Save Notification

                    _ucOnlinePortalContext.Notifications.Add(newNotification);
                }

                //Delete old tmp login
                var findTmpLogin = _ucOnlinePortalContext.TmpLogins.Where(x => x.Email == registrationRequest.address_contact.email).FirstOrDefault();

                if (findTmpLogin != null)
                {
                    _ucOnlinePortalContext.TmpLogins.Attach(findTmpLogin);
                    _ucOnlinePortalContext.TmpLogins.Remove(findTmpLogin);
                }

                _ucOnlinePortalContext.SaveChanges();
            }
            catch (Exception ex)
            {
                String message = ex.InnerException.ToString();
                hasError = true;
            }


            if (hasError)
            {
                return new RegistrationResponse { success = 0 };
            }
            else
            {
                return new RegistrationResponse { success = 1 };
            }
        }


        /*
         * Method to save enrollment data
        */
        public SaveEnrollmentResponse SaveEnrollmentData(SaveEnrollmentRequest saveEnrollRequest)
        {
            //Get data from Login Info
            var student = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == saveEnrollRequest.id_number).FirstOrDefault();
            List<Schedule> schedules = new List<Schedule>();
            List<SchedulesBe> schedulesBe = new List<SchedulesBe>();

            bool hasError = false;
            bool isFull = false;

            try
            {
                var enrollmentData = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == saveEnrollRequest.id_number && x.ActiveTerm == saveEnrollRequest.active_term).FirstOrDefault();

                enrollmentData.YearLevel = (short)saveEnrollRequest.year_level;
                enrollmentData.CourseCode = student.CourseCode;
                enrollmentData.EnrollmentDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                enrollmentData.SubmittedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                enrollmentData.Units = (short)saveEnrollRequest.total_units;
                enrollmentData.Classification = saveEnrollRequest.classification;
                enrollmentData.Dept = student.Dept;
                enrollmentData.Status = (short)EnrollmentStatus.SELECTING_SUBJECTS;
                enrollmentData.AdjustmentCount = 1;
                enrollmentData.DisapprovedDeanOn = null;
                enrollmentData.ActiveTerm = saveEnrollRequest.active_term;

                if (enrollmentData.DisapprovedDean != null && enrollmentData.DisapprovedDean != "")
                {
                    enrollmentData.DisapprovedDean = null;
                    enrollmentData.DisapprovedDeanOn = null;
                }

                Notification newNotif = new Notification
                {
                    StudId = saveEnrollRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.SELECTING_SUBJECTS,
                    ActiveTerm = saveEnrollRequest.active_term
                };

                _ucOnlinePortalContext.Notifications.Add(newNotif);
                _ucOnlinePortalContext.SaveChanges();

                if (saveEnrollRequest.accept_section == 1)
                {
                    enrollmentData.Status = (short)EnrollmentStatus.APPROVED_BY_DEAN;
                    enrollmentData.ApprovedDean = "AUTO-APPROVE";
                    enrollmentData.ApprovedDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    enrollmentData.Status = (short)EnrollmentStatus.APPROVED_BY_DEAN;

                    //Insert OSTSP if section is set by dean
                    var ostsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == saveEnrollRequest.id_number && x.Status != 2 && x.ActiveTerm == saveEnrollRequest.active_term).Select(x => x.EdpCode).ToList();

                    schedules = _ucOnlinePortalContext.Schedules.Where(x => ostsp.Contains(x.EdpCode) && x.Size == x.MaxSize && saveEnrollRequest.active_term == saveEnrollRequest.active_term).ToList();
                    schedulesBe = _ucOnlinePortalContext.SchedulesBes.Where(x => ostsp.Contains(x.EdpCode) && x.Size == x.MaxSize && saveEnrollRequest.active_term == saveEnrollRequest.active_term).ToList();

                    if ((schedules.Count > 0 || schedulesBe.Count > 0) && saveEnrollRequest.accept_section != 1)
                    {
                        isFull = true;
                    }
                    else
                    {
                        //Insert OSTSP if section is set by dean
                        var schedulesOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == saveEnrollRequest.id_number && x.Status != 2 && x.ActiveTerm == saveEnrollRequest.active_term).ToList();

                        var selected = schedulesOstp.Select(x => x.EdpCode).ToList();
                        var addPeOrNstp = saveEnrollRequest.schedules.Except(selected).ToList();

                        //Iterate Schedules to save individual EDP codes to OSTSP
                        for (int index = 0; index < addPeOrNstp.Count; index++)
                        {
                            Ostsp newStudentOstsp = new Ostsp
                            {
                                StudId = saveEnrollRequest.id_number,
                                EdpCode = addPeOrNstp[index],
                                Status = 1,
                                Remarks = null,
                                AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                ActiveTerm = saveEnrollRequest.active_term
                            };

                            if (enrollmentData.Dept.Equals("CL") || enrollmentData.Dept.Equals("SH"))
                            {
                                var scheduleAdd = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == addPeOrNstp[index] && x.ActiveTerm == saveEnrollRequest.active_term).FirstOrDefault();
                                scheduleAdd.Size += 1;
                                scheduleAdd.PendingEnrolled += 1;
                                _ucOnlinePortalContext.Schedules.Update(scheduleAdd);
                            }
                            else
                            {
                                var scheduleAdd = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == addPeOrNstp[index] && x.ActiveTerm == saveEnrollRequest.active_term).FirstOrDefault();
                                scheduleAdd.Size += 1;
                                scheduleAdd.PendingEnrolled += 1;
                                _ucOnlinePortalContext.SchedulesBes.Update(scheduleAdd);
                            }

                            //Save OSTSP
                            _ucOnlinePortalContext.Ostsps.Add(newStudentOstsp);
                            _ucOnlinePortalContext.SaveChanges();
                        }

                        schedulesOstp.ToList().ForEach(x => x.Status = 1);
                        schedulesOstp.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));

                        newNotif = new Notification
                        {
                            StudId = saveEnrollRequest.id_number,
                            NotifRead = 0,
                            Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            Message = Literals.APPROVED_BY_DEAN,
                            ActiveTerm = saveEnrollRequest.active_term
                        };

                        _ucOnlinePortalContext.Notifications.Add(newNotif);
                        _ucOnlinePortalContext.SaveChanges();

                        //If student is Accounting, Auto Approve!
                        if (!saveEnrollRequest.classification.Equals(String.Empty))
                        {
                            if (saveEnrollRequest.classification.Equals("H"))
                            {
                                enrollmentData.ApprovedAcctg = "AUTO-APPROVE";
                                enrollmentData.ApprovedAcctgOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                enrollmentData.NeededPayment = 500;

                                newNotif = new Notification
                                {
                                    StudId = saveEnrollRequest.id_number,
                                    NotifRead = 0,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Message = Literals.APPROVED_BY_ACCOUNTING,
                                    ActiveTerm = saveEnrollRequest.active_term
                                };

                                enrollmentData.Status = (short)EnrollmentStatus.APPROVED_BY_ACCOUNTING;

                                _ucOnlinePortalContext.Notifications.Add(newNotif);
                                _ucOnlinePortalContext.SaveChanges();
                            }

                        }

                    }
                }

                var ostspSelected = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == saveEnrollRequest.id_number && x.Status != 2 && x.ActiveTerm == saveEnrollRequest.active_term).Select(x => x.EdpCode).ToList();

                if (ostspSelected.Count > 0)
                {
                    if (saveEnrollRequest.schedules.Count > 0)
                    {
                        var toDelete = ostspSelected.Except(saveEnrollRequest.schedules).ToList();
                        var toAdd = saveEnrollRequest.schedules.Except(ostspSelected).ToList();

                        if (toDelete.Count > 0)
                        {
                            var schedule = _ucOnlinePortalContext.Ostsps.Where(x => toDelete.Contains(x.EdpCode) && x.StudId == saveEnrollRequest.id_number && x.ActiveTerm == saveEnrollRequest.active_term);

                            schedule.ToList().ForEach(x => x.Status = 2);
                            schedule.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                            _ucOnlinePortalContext.SaveChanges();
                        }

                        //Iterate Schedules to save individual EDP codes to OSTSP
                        for (int index = 0; index < toAdd.Count; index++)
                        {
                            var otspSched = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == saveEnrollRequest.id_number && x.EdpCode == toAdd[index] && x.ActiveTerm == saveEnrollRequest.active_term).Count();

                            if (otspSched == 0)
                            {
                                Ostsp newStudentOstsp = new Ostsp
                                {
                                    StudId = saveEnrollRequest.id_number,
                                    EdpCode = toAdd[index],
                                    Status = 0,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = saveEnrollRequest.active_term
                                };

                                //Save OSTSP
                                _ucOnlinePortalContext.Ostsps.Add(newStudentOstsp);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }
                    }
                }

                if (ostspSelected.Count == 0)
                {
                    //Iterate Schedules to save individual EDP codes to OSTSP
                    for (int index = 0; index < saveEnrollRequest.schedules.Count; index++)
                    {
                        var otspSched = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == saveEnrollRequest.id_number && x.EdpCode == saveEnrollRequest.schedules[index] && x.ActiveTerm == saveEnrollRequest.active_term).Count();

                        if (otspSched == 0)
                        {
                            Ostsp newStudentOstsp = new Ostsp
                            {
                                StudId = saveEnrollRequest.id_number,
                                EdpCode = saveEnrollRequest.schedules[index],
                                Status = 0,
                                Remarks = null,
                                AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                ActiveTerm = saveEnrollRequest.active_term
                            };

                            //Save OSTSP
                            _ucOnlinePortalContext.Ostsps.Add(newStudentOstsp);
                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }
                }
            }
            catch
            {
                hasError = true;
            }

            if (hasError)
            {
                return new SaveEnrollmentResponse { success = 0 };
            }
            else
            {
                return new SaveEnrollmentResponse { success = 1 };
            }
        }

        /*
         * Method to get all departments
        */
        public GetDepartmentResponse GetDepartment(GetDepartmentRequest getDepartmentRequest)
        {
            //get department names for college
            var departments = _ucOnlinePortalContext.CourseLists.Where(x => x.Department == getDepartmentRequest.department && x.CourseActive == 1 && x.ActiveTerm == getDepartmentRequest.active_term).ToList();

            var departmentResult = departments.Select(x => new GetDepartmentResponse.department
            {
                dept_abbr = x.CourseDepartmentAbbr,
                dept_name = x.CourseDepartment
            }).Distinct().ToList();

            var departmentList = departmentResult.GroupBy(x => x.dept_abbr).Select(grp => grp.First());

            return new GetDepartmentResponse { departments = departmentList.ToList() };
        }

        /*
       * Method to get all colleges from department
       */
        public GetCoursesResponse GetCourses(GetCoursesRequest getCollegeRequest)
        {
            var colleges = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseActive == 1 && x.ActiveTerm == getCollegeRequest.active_term).ToList();

            if (!getCollegeRequest.department.Equals(String.Empty))
            {
                colleges = _ucOnlinePortalContext.CourseLists.Where(x => (x.Department == getCollegeRequest.department && x.CourseActive == 1 && x.ActiveTerm == getCollegeRequest.active_term)).ToList();
            }
            else if (!getCollegeRequest.course_department.Equals(String.Empty) || !getCollegeRequest.department_abbr.Equals(String.Empty))
            {
                colleges = _ucOnlinePortalContext.CourseLists.Where(x => (x.CourseDepartment == getCollegeRequest.course_department || x.CourseDepartmentAbbr == getCollegeRequest.department_abbr) && x.CourseActive == 1 && x.ActiveTerm == getCollegeRequest.active_term).ToList();
            }

            var collegeResult = colleges.Select(x => new GetCoursesResponse.college
            {
                college_id = x.CourseId,
                college_code = x.CourseCode,
                college_name = x.CourseAbbr + " - " + x.CourseDescription,
                year_limit = x.CourseYearLimit,
                department = x.Department
            }).ToList();

            return new GetCoursesResponse { colleges = collegeResult };
        }

        /*
        * Method to view schedules
        */
        public ViewScheduleResponse ViewSchedules(ViewScheduleRequest viewScheduleRequest)
        {
            int take = (int)viewScheduleRequest.limit;
            int skip = (int)viewScheduleRequest.limit * ((int)viewScheduleRequest.page - 1);

            string department = String.Empty;

            if (!viewScheduleRequest.course_code.Equals(""))
            {
                department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == viewScheduleRequest.course_code && x.ActiveTerm == viewScheduleRequest.active_term).Select(x => x.Department).FirstOrDefault();
            }

            IQueryable<ViewScheduleResponse.schedule> result;

            if (department.Equals("BE") || department.Equals("JH"))
            {
                result = (from _schedules in _ucOnlinePortalContext.SchedulesBes
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on _schedules.InternalCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          join _courselist in _ucOnlinePortalContext.CourseLists
                          on _schedules.CourseCode equals _courselist.CourseCode into course
                          from _courselist in course.DefaultIfEmpty()
                          join _instructor in _ucOnlinePortalContext.LoginInfos
                          on _schedules.Instructor equals _instructor.StudId into instructor
                          from _instructor in instructor.DefaultIfEmpty()
                          where _schedules.ActiveTerm == viewScheduleRequest.active_term && _courselist.ActiveTerm == viewScheduleRequest.active_term
                          select new ViewScheduleResponse.schedule
                          {
                              edpcode = _schedules.EdpCode,
                              subject_name = _schedules.Description,
                              subject_type = _schedules.SubType,
                              days = _schedules.Days,
                              begin_time = _schedules.TimeStart,
                              end_time = _schedules.TimeEnd,
                              mdn = _schedules.Mdn,
                              m_begin_time = _schedules.MTimeStart,
                              m_end_time = _schedules.MTimeEnd,
                              m_days = _schedules.MDays,
                              units = _schedules.Units.ToString(),
                              room = _schedules.Room,
                              size = _schedules.Size.ToString(),
                              pending_enrolled = (int)_schedules.PendingEnrolled,
                              official_enrolled = (int)_schedules.OfficialEnrolled,
                              max_size = _schedules.MaxSize.ToString(),
                              status = _schedules.Status,
                              section = _schedules.Section,
                              split_code = _schedules.SplitCode,
                              split_type = _schedules.SplitType,
                              descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                              course_code = _schedules.CourseCode,
                              course_abbr = _courselist.CourseAbbr,
                              gened = (short)_schedules.IsGened,
                              instructor = _instructor.LastName + " , " + _instructor.FirstName
                          });
            }
            else
            {
                result = (from _schedules in _ucOnlinePortalContext.Schedules
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on _schedules.InternalCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          join _courselist in _ucOnlinePortalContext.CourseLists
                          on _schedules.CourseCode equals _courselist.CourseCode into course
                          from _courselist in course.DefaultIfEmpty()
                          join _instructor in _ucOnlinePortalContext.LoginInfos
                          on _schedules.Instructor equals _instructor.StudId into instructor
                          from _instructor in instructor.DefaultIfEmpty()
                          where _schedules.ActiveTerm == viewScheduleRequest.active_term && _courselist.ActiveTerm == viewScheduleRequest.active_term
                          select new ViewScheduleResponse.schedule
                          {
                              edpcode = _schedules.EdpCode,
                              subject_name = _schedules.Description,
                              subject_type = _schedules.SubType,
                              days = _schedules.Days,
                              begin_time = _schedules.TimeStart,
                              end_time = _schedules.TimeEnd,
                              mdn = _schedules.Mdn,
                              m_begin_time = _schedules.MTimeStart,
                              m_end_time = _schedules.MTimeEnd,
                              m_days = _schedules.MDays,
                              units = _schedules.Units.ToString(),
                              room = _schedules.Room,
                              size = _schedules.Size.ToString(),
                              pending_enrolled = (int)_schedules.PendingEnrolled,
                              official_enrolled = (int)_schedules.OfficialEnrolled,
                              max_size = _schedules.MaxSize.ToString(),
                              status = _schedules.Status,
                              section = _schedules.Section,
                              split_code = _schedules.SplitCode,
                              split_type = _schedules.SplitType,
                              descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                              course_code = _schedules.CourseCode,
                              course_abbr = _courselist.CourseAbbr,
                              gened = (short)_schedules.IsGened,
                              instructor = _instructor.LastName + " , " + _instructor.FirstName
                          });
            }

            if (viewScheduleRequest.gen_ed != null && !viewScheduleRequest.gen_ed.Equals(String.Empty))
            {
                var splitGen = viewScheduleRequest.gen_ed.Split(",").Select(Int32.Parse).ToList();
                if (splitGen.Count == 0)
                {
                    result = result.Where(x => x.gened == short.Parse(viewScheduleRequest.gen_ed));
                }
                else
                {
                    result = result.Where(x => splitGen.Contains(x.gened));
                }
            }

            if (!viewScheduleRequest.department_abbr.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == viewScheduleRequest.department_abbr && x.ActiveTerm == viewScheduleRequest.active_term).ToList();

                var courses = courseList.Select(x => x.CourseCode).ToList();
                result = result.Where(x => courses.Contains(x.course_code));
            }

            if (!viewScheduleRequest.course_code.Equals(String.Empty))
            {
                String[] courseCodeArray = { "PN", "MT", "CC", "BN", "MM", "MI", "MV", "MR" };

                if (viewScheduleRequest.no_nstp > 0 && viewScheduleRequest.no_pe > 0)
                {
                    if (viewScheduleRequest.course_code.Contains("PN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("PE"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MT"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("PE"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("CC"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("DEFTACT"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("BN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("PE"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MI"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("P.E"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MR"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("P.E"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MM") || viewScheduleRequest.course_code.Contains("MV"))
                    {
                        result = result.Where(x => (x.course_code.Contains("MM") || x.course_code.Contains("MV")) && x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("P.E"));
                    }
                    else
                    {
                        result = result.Where(x => !courseCodeArray.Contains(x.course_code) && (x.subject_name.StartsWith("NSTP") || x.subject_name.StartsWith("PE")));
                    }
                }
                else if (viewScheduleRequest.no_nstp > 0)
                {
                    if (viewScheduleRequest.course_code.Contains("PN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MT"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("CC"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("BN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MI"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MR"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("NSTP"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MM") || viewScheduleRequest.course_code.Contains("MV"))
                    {
                        result = result.Where(x => (x.course_code.Contains("MM") || x.course_code.Contains("MV")) && x.subject_name.StartsWith("NSTP"));
                    }
                    else
                    {
                        result = result.Where(x => !courseCodeArray.Contains(x.course_code) && x.subject_name.StartsWith("NSTP"));
                    }
                }
                else if (viewScheduleRequest.no_pe > 0)
                {
                    if (viewScheduleRequest.course_code.Contains("PN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("PE"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MT"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("PE"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("CC"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("DEFTACT"));
                    }
                    /*
                    else if (viewScheduleRequest.course_code.Contains("BN"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("PE"));
                    }*/
                    else if (viewScheduleRequest.course_code.Contains("MI"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("P.E"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MR"))
                    {
                        result = result.Where(x => x.course_code == viewScheduleRequest.course_code && x.subject_name.StartsWith("P.E"));
                    }
                    else if (viewScheduleRequest.course_code.Contains("MM") || viewScheduleRequest.course_code.Contains("MV"))
                    {
                        result = result.Where(x => (x.course_code.Contains("MM") || x.course_code.Contains("MV")) && x.subject_name.StartsWith("P.E"));
                    }
                    else
                    {
                        result = result.Where(x => !courseCodeArray.Contains(x.course_code) && x.subject_name.StartsWith("PE"));
                    }
                }
                else
                {
                    result = result.Where(x => x.course_code == viewScheduleRequest.course_code);
                }
            }

            if (!viewScheduleRequest.year_level.Equals(String.Empty) && viewScheduleRequest.year_level > 0)
            {
                if (viewScheduleRequest.year_level != 9)
                {
                    var mdnS = viewScheduleRequest.year_level % 10;
                    var ban = false;
                    var norm = false;
                    var yearEleven = false;
                    var nstpPeFilter = false;

                    if (viewScheduleRequest.no_nstp > 0 && viewScheduleRequest.no_pe > 0)
                    {
                        nstpPeFilter = true;
                    }

                    if (viewScheduleRequest.year_level / 10 == 111)
                    {
                        result = result.Where(x => x.section.Substring(2, 1).Equals("1"));
                    }
                    else if (viewScheduleRequest.year_level / 10 == 121)
                    {
                        result = result.Where(x => x.section.Substring(2, 1).Equals("2"));
                    }
                    else if (viewScheduleRequest.year_level / 10 == 112)
                    {
                        yearEleven = true;
                        ban = true;
                        result = result.Where(x => x.section.Substring(1, 1).Equals("-"));
                    }
                    else if (viewScheduleRequest.year_level / 10 == 122)
                    {
                        ban = true;
                        result = result.Where(x => x.section.Substring(2, 1).Equals("-"));
                    }
                    else if (viewScheduleRequest.no_nstp > 0 && viewScheduleRequest.no_pe > 0)
                    {
                        result = result.Where(x => x.subject_name.Contains("PE 101") || x.subject_name.Contains("P.E 111") || x.subject_name.Contains("P.E. 111 L") || x.subject_name.Contains("NSTP 101"));
                    }
                    else if (viewScheduleRequest.no_nstp > 0)
                    {
                        result = result.Where(x => x.subject_name.Contains("NSTP 101"));
                    }
                    else if (viewScheduleRequest.no_pe > 0)
                    {
                        result = result.Where(x => x.subject_name.Contains("PE 101") || x.subject_name.Contains("P.E 111") || x.subject_name.Contains("P.E. 111 L"));
                    }
                    else
                    {
                        norm = true;
                        result = result.Where(x => x.section.Contains(viewScheduleRequest.year_level.ToString()));
                    }

                    if (!norm && !nstpPeFilter)
                    {
                        //mdn lookup
                        if (mdnS == 8)
                        {
                            if (ban)
                            {
                                if (yearEleven)
                                {
                                    result = result.Where(x => x.section.Substring(2, 1).Equals("A"));
                                }
                                else
                                {
                                    result = result.Where(x => x.section.Substring(3, 1).Equals("A"));
                                }
                            }
                            else
                            {
                                result = result.Where(x => x.section.Substring(3, 1).Equals("A"));
                            }
                        }
                        else
                        {
                            if (ban)
                            {
                                if (yearEleven)
                                {
                                    result = result.Where(x => x.section.Substring(2, 1).Equals("P"));
                                }
                                else
                                {
                                    result = result.Where(x => x.section.Substring(3, 1).Equals("P"));
                                }
                            }
                            else
                            {
                                result = result.Where(x => x.section.Substring(3, 1).Equals("P"));
                            }
                        }
                    }
                }
            }

            if (viewScheduleRequest.edp_codes.Count > 0)
            {
                result = result.Where(x => viewScheduleRequest.edp_codes.Contains(x.edpcode));
            }

            if (!viewScheduleRequest.subject_name.Equals(String.Empty))
            {
                result = result.Where(x => x.subject_name.Contains(viewScheduleRequest.subject_name));
            }

            if (!viewScheduleRequest.section.Equals(String.Empty))
            {
                result = result.Where(x => x.section == viewScheduleRequest.section);
            }

            if (!viewScheduleRequest.status.Equals(String.Empty) && viewScheduleRequest.status != 9)
            {
                result = result.Where(x => x.status == viewScheduleRequest.status);
            }

            var count = result.Count();

            if (viewScheduleRequest.page != 0 && viewScheduleRequest.limit != 0)
            {
                result = result.OrderBy(x => x.section).ThenBy(x => x.course_code).ThenBy(x => x.edpcode).Skip(skip).Take(take);
            }

            return new ViewScheduleResponse { schedules = result.ToList(), count = count };
        }

        /*
         * Method to View Studyload
        */
        public GetStudyLoadResponse GetStudyLoad(GetStudyLoadRequest getRequest)
        {
            //Get user data
            var studyLoad = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == getRequest.id_number && x.ActiveTerm == getRequest.active_term);

            //check if the the data exist
            if (studyLoad == null)
            {
                //return empty data
                return new GetStudyLoadResponse { };
            }
            else
            {
                if (studyLoad.FirstOrDefault().Dept.Equals("CL") || studyLoad.FirstOrDefault().Dept.Equals("SH"))
                {
                    //Get data from Ostsp and Schedules
                    var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                                  join Schedules in _ucOnlinePortalContext.Schedules
                                  on Ostsp.EdpCode equals Schedules.EdpCode
                                  join _subject_info in _ucOnlinePortalContext.SubjectInfos
                                  on Schedules.InternalCode equals _subject_info.InternalCode into sched
                                  from _subject_info in sched.DefaultIfEmpty()
                                  join _courselist in _ucOnlinePortalContext.CourseLists
                                  on Schedules.CourseCode equals _courselist.CourseCode into course
                                  from _courselist in course.DefaultIfEmpty()
                                  where Ostsp.StudId == getRequest.id_number
                                  && Ostsp.Status != 2 & Ostsp.Status != 4 && Ostsp.Status != 5
                                  && Ostsp.ActiveTerm == getRequest.active_term && Schedules.ActiveTerm == getRequest.active_term
                                  && _courselist.ActiveTerm == getRequest.active_term
                                  select new GetStudyLoadResponse.Schedules
                                  {
                                      edp_code = Schedules.EdpCode,
                                      subject_name = Schedules.Description,
                                      subject_type = Schedules.SubType,
                                      days = Schedules.Days,
                                      begin_time = Schedules.TimeStart,
                                      end_time = Schedules.TimeEnd,
                                      mdn = Schedules.Mdn,
                                      m_begin_time = Schedules.MTimeStart,
                                      m_end_time = Schedules.MTimeEnd,
                                      m_days = Schedules.MDays,
                                      size = Schedules.Size,
                                      max_size = Schedules.MaxSize,
                                      units = Schedules.Units,
                                      room = Schedules.Room,
                                      descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                                      split_code = Schedules.SplitCode,
                                      split_type = Schedules.SplitType,
                                      section = Schedules.Section,
                                      course_abbr = _courselist.CourseAbbr,
                                      status = Schedules.Status
                                  }).ToList();


                    var has_pe_v = result.Where(x => x.subject_name.StartsWith("PE") || x.subject_name.StartsWith("P.E") || x.subject_name.StartsWith("DEFTACT")).Count() > 0 ? 1 : 0;
                    var has_nstp_v = result.Where(x => x.subject_name.StartsWith("NSTP")).Count() > 0 ? 1 : 0;

                    var getDept = studyLoad.FirstOrDefault();

                    string[] exempted = { "JD", "JT", "PD" };

                    if (getDept != null)
                    {
                        if (getDept.Dept.Equals("SH"))
                        {
                            has_nstp_v = 1;
                            has_pe_v = 1;
                        }
                        if (getDept.YearLevel == 1 && exempted.Contains(getDept.CourseCode))
                        {
                            has_nstp_v = 1;
                            has_pe_v = 1;
                        }
                        if (getDept.YearLevel > 1)
                        {
                            has_nstp_v = 1;
                        }
                        if (getDept.YearLevel > 2)
                        {
                            has_nstp_v = 1;
                            has_pe_v = 1;
                        }
                        if (getDept.YearLevel == 1 && getDept.CourseCode.Equals("PN"))
                        {
                            has_nstp_v = 1;
                        }
                        if (getDept.YearLevel == 2 && getDept.CourseCode.Equals("HM"))
                        {
                            has_nstp_v = 1;
                            has_pe_v = 1;
                        }
                        if (getDept.YearLevel == 1 && getDept.CourseCode.Equals("BN"))
                        {
                            has_nstp_v = 1;
                        }
                    }


                    //return studyload response
                    return new GetStudyLoadResponse { schedules = result, has_pe = has_pe_v, has_nstp = has_nstp_v };
                }
                else
                {
                    //Get data from Ostsp and Schedules
                    var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                                  join Schedules in _ucOnlinePortalContext.SchedulesBes
                                  on Ostsp.EdpCode equals Schedules.EdpCode
                                  join _subject_info in _ucOnlinePortalContext.SubjectInfos
                                  on Schedules.InternalCode equals _subject_info.InternalCode into sched
                                  from _subject_info in sched.DefaultIfEmpty()
                                  join _courselist in _ucOnlinePortalContext.CourseLists
                                  on Schedules.CourseCode equals _courselist.CourseCode into course
                                  from _courselist in course.DefaultIfEmpty()
                                  where Ostsp.StudId == getRequest.id_number
                                  && Ostsp.Status != 2 && Ostsp.Status != 4 && Ostsp.Status != 5
                                  && Ostsp.ActiveTerm == getRequest.active_term && Schedules.ActiveTerm == getRequest.active_term
                                  && _courselist.ActiveTerm == getRequest.active_term
                                  select new GetStudyLoadResponse.Schedules
                                  {
                                      edp_code = Schedules.EdpCode,
                                      subject_name = Schedules.Description,
                                      subject_type = Schedules.SubType,
                                      days = Schedules.Days,
                                      begin_time = Schedules.TimeStart,
                                      end_time = Schedules.TimeEnd,
                                      mdn = Schedules.Mdn,
                                      m_begin_time = Schedules.MTimeStart,
                                      m_end_time = Schedules.MTimeEnd,
                                      m_days = Schedules.MDays,
                                      size = Schedules.Size,
                                      max_size = Schedules.MaxSize,
                                      units = Schedules.Units,
                                      room = Schedules.Room,
                                      descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                                      split_code = Schedules.SplitCode,
                                      split_type = Schedules.SplitType,
                                      section = Schedules.Section,
                                      course_abbr = _courselist.CourseAbbr,
                                      status = Schedules.Status
                                  }).ToList();

                    //return studyload response
                    return new GetStudyLoadResponse { schedules = result, has_pe = 1, has_nstp = 1 };
                }
            }
        }

        /*
         * Method to Get Student Status
        */
        public GetStudentStatusResponse GetStudentStatus(GetStudentStatusRequest getStudentStatusRequest)
        {
            //Get student enrollment data
            var studentStatus = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == getStudentStatusRequest.id_number && x.ActiveTerm == getStudentStatusRequest.active_term).FirstOrDefault();
            var loginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == getStudentStatusRequest.id_number).FirstOrDefault();

            //create list holder
            List<GetStudentStatusResponse.Status> status = new List<GetStudentStatusResponse.Status>();

            if (studentStatus != null)
            {
                //Loop to 8 steps and build each step's result
                for (int counter = 0; counter < 8; counter++)
                {
                    GetStudentStatusResponse.Status stat = new GetStudentStatusResponse.Status();

                    stat.done = 0;
                    stat.step = counter + 1;

                    switch (counter)
                    {
                        case 0:
                            {
                                if (studentStatus.Status >= 0)
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.RegisteredOn.ToString();
                                    stat.approved = 1;
                                }
                            }
                            break;
                        case 1:
                            {
                                if ((studentStatus.ApprovedRegRegistrar != null && studentStatus.ApprovedRegRegistrar.Trim().Length > 0) || (studentStatus.DisapprovedRegRegistrar != null && studentStatus.DisapprovedRegRegistrar.Trim().Length > 0))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.DisapprovedRegRegistrar != null ? studentStatus.DisapprovedRegRegistrarOn.ToString() : studentStatus.ApprovedRegRegistrarOn.ToString();
                                    stat.approved = studentStatus.DisapprovedRegRegistrar != null ? 0 : 1;
                                }
                            }
                            break;
                        case 2:
                            {
                                if ((studentStatus.ApprovedRegDean != null && studentStatus.ApprovedRegDean.Trim().Length > 0) || (studentStatus.DisapprovedRegDean != null && studentStatus.DisapprovedRegDean.Trim().Length > 0))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.DisapprovedRegDean != null ? studentStatus.DisapprovedRegDeanOn.ToString() : studentStatus.ApprovedRegDeanOn.ToString();
                                    stat.approved = studentStatus.DisapprovedRegDean != null ? 0 : 1;
                                }
                            }
                            break;
                        case 3:
                            {
                                if (!studentStatus.SubmittedOn.ToString().Equals(String.Empty))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.SubmittedOn.ToString();
                                    stat.approved = 1;
                                }
                            }
                            break;
                        case 4:
                            {
                                if ((studentStatus.ApprovedDean != null && studentStatus.ApprovedDean.Trim().Length > 0) || (studentStatus.DisapprovedDean != null && studentStatus.DisapprovedDean.Trim().Length > 0))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.DisapprovedDean != null ? studentStatus.DisapprovedDeanOn.ToString() : studentStatus.ApprovedDeanOn.ToString();
                                    stat.approved = studentStatus.DisapprovedDean != null ? 0 : 1;
                                }
                            }
                            break;
                        case 5:
                            {
                                if (!studentStatus.ApprovedAcctgOn.ToString().Equals(String.Empty))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.ApprovedAcctgOn.ToString();
                                    stat.approved = 1;
                                }
                            }
                            break;
                        case 6:
                            {
                                if (!studentStatus.ApprovedCashierOn.ToString().Equals(String.Empty))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.ApprovedCashierOn.ToString();
                                    stat.approved = 1;
                                }
                            }
                            break;
                        case 7:
                            {
                                if (!studentStatus.ApprovedCashierOn.ToString().Equals(String.Empty))
                                {
                                    stat.done = 1;
                                    stat.date = studentStatus.ApprovedCashierOn.ToString();
                                    stat.approved = 1;
                                }
                            }
                            break;
                    }
                    status.Add(stat);
                }
            }

            var clasify = studentStatus == null ? "" : studentStatus.Classification;

            int is_cancelled = 0;
            string neededPayment = "0";
            int pending_promissory = 0;
            int promise_pay = 0;
            int hasAdjustment = 0;

            var openAdjustment = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == loginInfo.CourseCode && x.AdjustmentStart <= DateTime.Now.Date && x.AdjustmentEnd >= DateTime.Now.Date && x.ActiveTerm == getStudentStatusRequest.active_term).Count();
            short? enrollmentOpen = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == loginInfo.CourseCode && x.ActiveTerm == getStudentStatusRequest.active_term).Select(x => x.EnrollmentOpen).FirstOrDefault();

            if (studentStatus != null)
            {
                is_cancelled = studentStatus.Status == 13 ? 1 : 0;
                neededPayment = studentStatus.NeededPayment == null ? "0" : studentStatus.NeededPayment.ToString();
                pending_promissory = studentStatus.RequestPromissory != 0 && studentStatus.RequestPromissory != 3 ? 1 : 0;
                promise_pay = studentStatus.PromiPay;
                hasAdjustment = (studentStatus.AdjustmentCount == 0 || studentStatus.AdjustmentCount == 9) ? 1 : 0;
            }

            return new GetStudentStatusResponse { status = status, classification = clasify, is_cancelled = is_cancelled, needed_payment = neededPayment, pending_promissory = pending_promissory, promi_pay = promise_pay, adjustment_open = openAdjustment, enrollment_open = enrollmentOpen.Value, has_adjustment = hasAdjustment };
        }

        /*
        * Method to View List
        */

        public ViewStudentPerStatusResponse ViewStudentStatus(ViewStudentPerStatusRequest viewStudentPerStatusRequest)
        {
            //settings for pagination
            int take = (int)viewStudentPerStatusRequest.limit;
            int skip = (int)viewStudentPerStatusRequest.limit * ((int)viewStudentPerStatusRequest.page - 1);

            //populate initial response object
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          where Oenrp.ActiveTerm == viewStudentPerStatusRequest.active_term && _courseList.ActiveTerm == viewStudentPerStatusRequest.active_term
                          //join _attach in _ucOnlinePortalContext.Attachments
                          //on Oenrp.StudId equals _attach.StudId into gattach
                          //from _attach in gattach.DefaultIfEmpty()
                          //where Oenrp.Status == viewStudentPerStatusRequest.status
                          select new ViewStudentPerStatusResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              classification_abbr = Oenrp.Classification,
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              status = Oenrp.Status,
                              year_level = Oenrp.YearLevel,
                              submitted_on = Oenrp.SubmittedOn,
                              registered_on = Oenrp.RegisteredOn,
                              approved_reg_registrar = Oenrp.ApprovedRegRegistrar,
                              approved_reg_registrar_on = Oenrp.ApprovedRegRegistrarOn,
                              disapproved_reg_registrar = Oenrp.DisapprovedRegRegistrar,
                              disaproved_reg_registrar_on = Oenrp.DisapprovedRegRegistrarOn,
                              approved_dean_reg = Oenrp.ApprovedRegDean,
                              approved_dean_reg_on = Oenrp.ApprovedRegDeanOn,
                              disapproved_reg_dean = Oenrp.DisapprovedRegDean,
                              disapproved_reg_dean_on = Oenrp.DisapprovedRegDeanOn,
                              approved_dean = Oenrp.ApprovedDean,
                              approved_dean_on = Oenrp.ApprovedDeanOn,
                              disapproved_dean = Oenrp.DisapprovedDean,
                              disapproved_dean_on = Oenrp.DisapprovedDeanOn,
                              approved_accounting = Oenrp.ApprovedAcctg,
                              approved_accounting_on = Oenrp.ApprovedAcctgOn,
                              approved_cashier = Oenrp.ApprovedCashier,
                              approved_cashier_on = Oenrp.ApprovedCashierOn,
                              request_deblock = (short)Oenrp.RequestDeblock,
                              request_overload = (short)Oenrp.RequestOverload,
                              needed_payment = (int)Oenrp.NeededPayment,
                              promi_pay = (int)Oenrp.PromiPay,
                              has_payment = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == Oenrp.StudId && x.Type.Equals("Payment") && x.ActiveTerm == viewStudentPerStatusRequest.active_term).Take(1).Count(),
                              has_promissory = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == Oenrp.StudId && x.RequestPromissory == 3 && x.ActiveTerm == viewStudentPerStatusRequest.active_term).Count(),
                              profile = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == Oenrp.StudId && x.Type == "2x2 ID Picture" && x.ActiveTerm == viewStudentPerStatusRequest.active_term).Select(x => x.Filename).FirstOrDefault(),
                              enrollmentDate = Oenrp.EnrollmentDate
                          });

            if (viewStudentPerStatusRequest.status == 99)
            {
                //do nothing
            }
            else if (viewStudentPerStatusRequest.status == 98)
            {
                int[] statStudyload = { 6, 8, 9, 10 };

                result = result.Where(x => statStudyload.Contains((int)x.status));
            }
            else
            {
                if (viewStudentPerStatusRequest.status == 6)
                {
                    if (viewStudentPerStatusRequest.course_department.Equals(String.Empty))
                    {
                        result = result.Where(x => (int)x.status == viewStudentPerStatusRequest.status);
                    }
                    else
                    {
                        result = result.Where(x => x.approved_dean_on != null);
                    }
                }
                else if (viewStudentPerStatusRequest.status == 7)
                {
                    result = result.Where(x => x.disapproved_dean_on != null);
                }
                else if (viewStudentPerStatusRequest.status == 8)
                {
                    if (viewStudentPerStatusRequest.is_cashier != null && viewStudentPerStatusRequest.is_cashier == 1)
                    {
                        result = result.Where(x => (int)x.status == 8);

                        var countCashier = (from Oenrp in _ucOnlinePortalContext.Oenrps
                                            join _attachment in _ucOnlinePortalContext.Attachments
                                            on Oenrp.StudId equals _attachment.StudId
                                            where Oenrp.ActiveTerm == viewStudentPerStatusRequest.active_term &&
                                            _attachment.AttachmentId == (from _attach in _ucOnlinePortalContext.Attachments
                                                                         where _attach.StudId == _attachment.StudId
                                                                         && _attach.Type.Equals("Payment")
                                                                         && _attach.ActiveTerm == viewStudentPerStatusRequest.active_term
                                                                         orderby _attach.AttachmentId
                                                                         select _attach.AttachmentId).FirstOrDefault()
                                                                        && Oenrp.Status == 8
                                            select Oenrp.StudId).ToList();

                        result = result.Where(x => countCashier.Contains(x.id_number));
                    }
                    else
                    {
                        result = result.Where(x => x.approved_accounting_on != null);
                    }
                }
                else
                {
                    result = result.Where(x => (int)x.status == viewStudentPerStatusRequest.status);
                }
            }

            //if status stage requires filtering of courses, add another filter
            if (viewStudentPerStatusRequest.status == 1 || viewStudentPerStatusRequest.status == 3 || viewStudentPerStatusRequest.status == 4 || viewStudentPerStatusRequest.status == 5 || viewStudentPerStatusRequest.status == 6 || viewStudentPerStatusRequest.status == 7 || viewStudentPerStatusRequest.status == 10 || viewStudentPerStatusRequest.status == 99 || viewStudentPerStatusRequest.status == 14)
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == viewStudentPerStatusRequest.course_department && x.ActiveTerm == viewStudentPerStatusRequest.active_term).ToList();

                if (viewStudentPerStatusRequest.status == 10 && viewStudentPerStatusRequest.course_department.Equals(String.Empty) && viewStudentPerStatusRequest.course.Equals(String.Empty))
                {
                    //do nothing
                }
                else if (viewStudentPerStatusRequest.course_department.Equals(String.Empty) && viewStudentPerStatusRequest.course.Equals(String.Empty))
                {
                    //do nothing
                }
                else if (viewStudentPerStatusRequest.course.Equals(String.Empty))
                {
                    var courses = courseList.Select(x => x.CourseCode).ToList();
                    result = result.Where(x => courses.Contains(x.course_code));
                }
                else
                {
                    result = result.Where(x => x.course_code == viewStudentPerStatusRequest.course);
                }
            }
            else
            {
                if (!viewStudentPerStatusRequest.course.Equals(String.Empty))
                {
                    result = result.Where(x => x.course_code == viewStudentPerStatusRequest.course);
                }
            }


            //searching options
            if (!viewStudentPerStatusRequest.id_number.Equals(String.Empty))
            {
                result = result.Where(x => x.id_number == viewStudentPerStatusRequest.id_number);
            }
            if (!viewStudentPerStatusRequest.name.Equals(String.Empty) && !viewStudentPerStatusRequest.date.Equals(String.Empty))
            {
                result = result.Where(x => (x.firstname + "" + x.lastname).Contains(viewStudentPerStatusRequest.name) && DateTime.Parse(viewStudentPerStatusRequest.date + " 00:00:00") <= x.registered_on && DateTime.Parse(viewStudentPerStatusRequest.date + " 23:59:59") >= x.registered_on);
            }
            if (!viewStudentPerStatusRequest.name.Equals(String.Empty))
            {
                result = result.Where(x => (x.firstname + "" + x.lastname).Contains(viewStudentPerStatusRequest.name));
            }
            if (!viewStudentPerStatusRequest.date.Equals(String.Empty))
            {
                result = result.Where(x => DateTime.Parse(viewStudentPerStatusRequest.date + " 00:00:00") <= x.registered_on && DateTime.Parse(viewStudentPerStatusRequest.date + " 23:59:59") >= x.registered_on);
            }
            if (viewStudentPerStatusRequest.year_level != 0)
            {
                result = result.Where(x => x.year_level == viewStudentPerStatusRequest.year_level);
            }
            if (!viewStudentPerStatusRequest.classification.Equals(String.Empty))
            {
                result = result.Where(x => x.classification_abbr == viewStudentPerStatusRequest.classification);
            }

            var count = result.Count();

            if (viewStudentPerStatusRequest.page != 0 && viewStudentPerStatusRequest.limit != 0)
            {
                result = result.OrderBy(x => x.id_number).Skip(skip).Take(take);
            }

            return new ViewStudentPerStatusResponse { students = result.ToList(), count = 1 };
        }


        /*
        * Method to View List
        */

        public ViewStudentRegistrationResponse ViewRegistration(ViewStudentRegistrationRequest viewStudentRegistrationRequest)
        {
            //get data from different tables
            var studentinfo = _ucOnlinePortalContext.StudentInfos.Where(x => x.StudId == viewStudentRegistrationRequest.id_number && x.ActiveTerm == viewStudentRegistrationRequest.active_term).FirstOrDefault();
            var contactInfo = _ucOnlinePortalContext.ContactAddresses.Where(x => x.StudInfoId == studentinfo.StudInfoId && x.ActiveTerm == viewStudentRegistrationRequest.active_term).FirstOrDefault();
            var familyInfo = _ucOnlinePortalContext.FamilyInfos.Where(x => x.StudInfoId == studentinfo.StudInfoId && x.ActiveTerm == viewStudentRegistrationRequest.active_term).FirstOrDefault();
            var schooInfo = _ucOnlinePortalContext.SchoolInfos.Where(x => x.StudInfoId == studentinfo.StudInfoId && x.ActiveTerm == viewStudentRegistrationRequest.active_term).FirstOrDefault(); ;
            var loginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == viewStudentRegistrationRequest.id_number).FirstOrDefault();
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == viewStudentRegistrationRequest.id_number && x.ActiveTerm == viewStudentRegistrationRequest.active_term).FirstOrDefault();

            var attachmentList = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == viewStudentRegistrationRequest.id_number && x.Type != "Payment" && x.ActiveTerm == viewStudentRegistrationRequest.active_term).ToList();

            if (viewStudentRegistrationRequest.payment != null && viewStudentRegistrationRequest.payment == 1)
            {
                attachmentList = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == viewStudentRegistrationRequest.id_number && x.Type.Equals("Payment") && x.ActiveTerm == viewStudentRegistrationRequest.active_term).Take(1).ToList();
            }
            var course = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == studentinfo.CourseCode).FirstOrDefault();


            List<ViewStudentRegistrationResponse.attachment> attach = new List<ViewStudentRegistrationResponse.attachment>();
            attach = attachmentList.Select(x => new ViewStudentRegistrationResponse.attachment {
                attachment_id = x.AttachmentId,
                email = x.Email,
                filename = x.Filename,
                id_number = x.StudId,
                type = x.Type
            }).ToList();

            //populate data
            ViewStudentRegistrationResponse registrationResponse = new ViewStudentRegistrationResponse
            {
                stud_id = studentinfo.StudId,
                allowed_units = loginInfo.AllowedUnits.HasValue ? (int)loginInfo.AllowedUnits : 0,
                course = course.CourseAbbr,
                college = course.CourseDepartment,
                course_code = studOenrp.CourseCode,
                assigned_section = studOenrp.Section,
                year_level = studentinfo.YearLevel,
                mdn = studentinfo.Mdn,
                first_name = studentinfo.FirstName,
                middle_name = studentinfo.MiddleName,
                last_name = studentinfo.LastName,
                suffix = studentinfo.Suffix,
                gender = studentinfo.Gender,
                status = studentinfo.Status,
                nationality = studentinfo.Nationality,
                birthdate = studentinfo.BirthDate.ToString(),
                birthplace = studentinfo.BirthPlace,
                religion = studentinfo.Religion,
                start_term = studentinfo.StartTerm,
                is_verified = (short)loginInfo.IsVerified,
                classification = studentinfo.Classification,
                dept = studentinfo.Dept,
                pcountry = contactInfo.PCountry,
                pprovince = contactInfo.PProvince,
                pcity = contactInfo.PCity,
                pbarangay = contactInfo.PBarangay,
                pstreet = contactInfo.PStreet,
                pzip = contactInfo.PZip,
                cprovince = contactInfo.CProvince,
                ccity = contactInfo.CCity,
                cbarangay = contactInfo.CBarangay,
                cstreet = contactInfo.CStreet,
                mobile = contactInfo.Mobile,
                landline = contactInfo.Landline,
                email = contactInfo.Email,
                facebook = contactInfo.Facebook,
                father_name = familyInfo.FatherName,
                father_contact = familyInfo.FatherContact,
                father_occupation = familyInfo.FatherOccupation,
                mother_name = familyInfo.MotherName,
                mother_contact = familyInfo.MotherContact,
                mother_occupation = familyInfo.MotherOccupation,
                guardian_name = familyInfo.GuardianName,
                guardian_contact = familyInfo.GuardianContact,
                guardian_occupation = familyInfo.GuardianOccupation,
                elem_name = schooInfo.ElemName,
                elem_year = schooInfo.ElemYear,
                elem_last_year = schooInfo.ElemLastYear.HasValue ? (short)schooInfo.ElemLastYear : 0,
                elem_type = schooInfo.ElemType,
                elem_lrn_number = schooInfo.ElemLrnNo,
                elem_esc_school_id = schooInfo.ElemEscSchoolId,
                elem_esc_student_id = schooInfo.ElemEscStudentId,
                sec_name = schooInfo.SecName,
                sec_year = schooInfo.SecYear,
                sec_last_year = schooInfo.SecLastYear.HasValue ? (short)schooInfo.SecLastYear : 0,
                sec_last_strand = schooInfo.SecLastStrand,
                sec_type = schooInfo.SecType,
                sec_lrn_number = schooInfo.SecLrnNo,
                sec_esc_school_id = schooInfo.SecEscSchoolId,
                sec_esc_student_id = schooInfo.SecEscStudentId,
                col_name = schooInfo.ColName,
                col_year = schooInfo.ColYear,
                col_course = schooInfo.ColCourse,
                col_last_year = schooInfo.ColLastYear.HasValue ? (short)schooInfo.ColLastYear : 0,
                attachments = attach,
                request_overload = (short)studOenrp.RequestOverload,
                request_deblock = (short)studOenrp.RequestDeblock
            };

            return registrationResponse;
        }

        /*
        * Method to Set Approve or Disapprove
        */

        public SetApproveOrDisapprovedResponse SetApproveOrDisapprove(SetApproveOrDisapprovedRequest setApproveOrDisapprovedRequest)
        {
            var studentOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).FirstOrDefault();
            var studentLogin = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number).FirstOrDefault();
            var studentInfo = _ucOnlinePortalContext.StudentInfos.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).FirstOrDefault();
            var notification = _ucOnlinePortalContext.Notifications.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
            var studentOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
            var studentAttachment = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);

            String final_id = setApproveOrDisapprovedRequest.id_number;
            Notification newNotif = new Notification();
            List<Schedule> schedules = new List<Schedule>();
            List<SchedulesBe> schedulesBe = new List<SchedulesBe>();
            bool isFull = false;

            if (setApproveOrDisapprovedRequest.status == 1)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;
                studentOenrp.Units = (short)setApproveOrDisapprovedRequest.allowed_units;
                studentOenrp.ApprovedRegRegistrar = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.ApprovedRegRegistrarOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                if (!setApproveOrDisapprovedRequest.year_level.Equals(String.Empty))
                {
                    studentOenrp.YearLevel = (short)setApproveOrDisapprovedRequest.year_level;
                }
                if (!setApproveOrDisapprovedRequest.classification.Equals(String.Empty))
                {
                    studentOenrp.Classification = setApproveOrDisapprovedRequest.classification;
                }
                if (!setApproveOrDisapprovedRequest.allowed_units.Equals(String.Empty))
                {
                    studentLogin.AllowedUnits = (short)setApproveOrDisapprovedRequest.allowed_units;
                }
                if (!setApproveOrDisapprovedRequest.curr_year.Equals(String.Empty))
                {
                    studentLogin.CurrYear = (short)setApproveOrDisapprovedRequest.curr_year;
                }

                _ucOnlinePortalContext.LoginInfos.Update(studentLogin);

                //Check classification 
                // if classification is old, update id number
                if (setApproveOrDisapprovedRequest.classification.Equals("O"))
                {
                    if (!setApproveOrDisapprovedRequest.existing_id_number.Equals(""))
                    {
                        //get the old loginfo
                        var toDelete = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == setApproveOrDisapprovedRequest.existing_id_number).FirstOrDefault();

                        //delete the old loginfo
                        if (toDelete != null)
                        {
                            _ucOnlinePortalContext.LoginInfos.Remove(toDelete);
                        }

                        //update student id
                        final_id = setApproveOrDisapprovedRequest.existing_id_number;
                        studentOenrp.StudId = setApproveOrDisapprovedRequest.existing_id_number;
                        studentLogin.StudId = setApproveOrDisapprovedRequest.existing_id_number;
                        studentInfo.StudId = setApproveOrDisapprovedRequest.existing_id_number;

                        if (studentAttachment != null)
                        {
                            studentAttachment.ToList().ForEach(x => x.StudId = setApproveOrDisapprovedRequest.existing_id_number);
                        }

                        notification.ToList().ForEach(x => x.StudId = setApproveOrDisapprovedRequest.existing_id_number);
                    }
                }
                else if (setApproveOrDisapprovedRequest.classification.Equals("H"))
                {
                    //create new id number
                    var config = _ucOnlinePortalContext.Configs.Where(x => x.IdYear == short.Parse(setApproveOrDisapprovedRequest.active_term.Substring(2, 2))).FirstOrDefault();

                    //fill with 0 those empty values
                    string sequence = config.Sequence.ToString();
                    sequence = sequence.PadLeft(4, '0');

                    StringBuilder idnumber = new StringBuilder();
                    idnumber.Append(config.IdYear);
                    idnumber.Append(config.CampusId);
                    idnumber.Append(sequence);

                    string idNumber = Utils.Function.Modulo10(idnumber.ToString());

                    var configs = _ucOnlinePortalContext.Configs.Where(x => x.IdYear == short.Parse(setApproveOrDisapprovedRequest.active_term.Substring(2, 2)));
                    configs.ToList().ForEach(x => x.Sequence = config.Sequence + 1);

                    //update sequence in config
                    //_ucOnlinePortalContext.Configs.Update(config);

                    //update student id
                    final_id = idNumber;
                    studentOenrp.StudId = idNumber;
                    studentLogin.StudId = idNumber;
                    studentInfo.StudId = idNumber;


                    if (studentAttachment != null)
                    {
                        studentAttachment.ToList().ForEach(x => x.StudId = final_id);
                    }

                    notification.ToList().ForEach(x => x.StudId = idNumber);
                }

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVED_REGISTRATION_REGISTRAR + ". Your ID Number is : " + final_id,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };
            }
            else if (setApproveOrDisapprovedRequest.status == 2)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.DisapprovedRegRegistrar = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.DisapprovedRegRegistrarOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                newNotif = new Notification
                {
                    StudId = setApproveOrDisapprovedRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVED_REGISTRATION_REGISTRAR + " " + setApproveOrDisapprovedRequest.message,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                _ucOnlinePortalContext.Notifications.Add(newNotif);
            }
            else if (setApproveOrDisapprovedRequest.status == 3)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.ApprovedRegDean = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.ApprovedRegDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                final_id = setApproveOrDisapprovedRequest.id_number;

                if (setApproveOrDisapprovedRequest.id_number.Length != 8)
                {
                    //create new id number
                    //var config = _ucOnlinePortalContext.Configs.FirstOrDefault();
                    var config = _ucOnlinePortalContext.Configs.Where(x => x.IdYear == short.Parse(setApproveOrDisapprovedRequest.active_term.Substring(2, 2))).FirstOrDefault();

                    //fill with 0 those empty values
                    string sequence = config.Sequence.ToString();
                    sequence = sequence.PadLeft(4, '0');

                    StringBuilder idnumber = new StringBuilder();
                    idnumber.Append(config.IdYear);
                    idnumber.Append(config.CampusId);
                    idnumber.Append(sequence);

                    string idNumber = Utils.Function.Modulo10(idnumber.ToString());

                    var configs = _ucOnlinePortalContext.Configs.Where(x => x.IdYear == short.Parse(setApproveOrDisapprovedRequest.active_term.Substring(2, 2)));
                    configs.ToList().ForEach(x => x.Sequence = config.Sequence + 1);

                    //update sequence in config
                    //_ucOnlinePortalContext.Configs.Update(config);

                    //update student id
                    final_id = idNumber;
                    studentOenrp.StudId = idNumber;
                    studentLogin.StudId = idNumber;
                    studentInfo.StudId = idNumber;

                    if (studentAttachment != null)
                    {
                        studentAttachment.ToList().ForEach(x => x.StudId = final_id);
                    }

                    notification.ToList().ForEach(x => x.StudId = idNumber);
                }

                if (!setApproveOrDisapprovedRequest.section.Equals(String.Empty))
                {
                    studentOenrp.Section = setApproveOrDisapprovedRequest.section;

                    //Insert OSTSP if section is set by dean
                    schedules = _ucOnlinePortalContext.Schedules.Where(x => x.Section == setApproveOrDisapprovedRequest.section && x.CourseCode == setApproveOrDisapprovedRequest.course_code && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).ToList();
                    schedulesBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.Section == setApproveOrDisapprovedRequest.section && x.CourseCode == setApproveOrDisapprovedRequest.course_code && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).ToList();

                    if (setApproveOrDisapprovedRequest.course_code.Equals("MM") || setApproveOrDisapprovedRequest.course_code.Equals("MV"))
                    {
                        schedules = _ucOnlinePortalContext.Schedules.Where(x => x.Section == setApproveOrDisapprovedRequest.section && x.CourseCode == setApproveOrDisapprovedRequest.course_code && x.Status == 1 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).ToList();
                    }

                    var schedFull = schedules.Where(x => x.Size == x.MaxSize).ToList();
                    var schedFullBe = schedulesBe.Where(x => x.Size == x.MaxSize).ToList();

                    if (schedFull.Count > 0 || schedFullBe.Count > 0)
                    {
                        schedules = schedules.Where(x => x.Size == x.MaxSize).ToList();
                        schedFullBe = schedFullBe.Where(x => x.Size == x.MaxSize).ToList();
                        isFull = true;
                    }
                    else
                    {
                        if (studentOenrp.Dept.Equals("CL") || studentOenrp.Dept.Equals("SH"))
                        {
                            foreach (Schedule sched in schedules)
                            {
                                Ostsp newStudentOstsp = new Ostsp
                                {
                                    StudId = final_id,
                                    EdpCode = sched.EdpCode,
                                    Status = 0,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                                };

                                var scheduleAdd = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == sched.EdpCode && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).FirstOrDefault();
                                scheduleAdd.Size += 1;
                                scheduleAdd.PendingEnrolled += 1;
                                _ucOnlinePortalContext.Schedules.Update(scheduleAdd);

                                //Save OSTSP
                                _ucOnlinePortalContext.Ostsps.Add(newStudentOstsp);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }
                        else
                        {
                            foreach (SchedulesBe sched in schedulesBe)
                            {
                                Ostsp newStudentOstsp = new Ostsp
                                {
                                    StudId = final_id,
                                    EdpCode = sched.EdpCode,
                                    Status = 0,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                                };

                                var scheduleAdd = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == sched.EdpCode && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).FirstOrDefault();
                                scheduleAdd.Size += 1;
                                scheduleAdd.PendingEnrolled += 1;
                                _ucOnlinePortalContext.SchedulesBes.Update(scheduleAdd);

                                //Save OSTSP
                                _ucOnlinePortalContext.Ostsps.Add(newStudentOstsp);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }
                    }
                }
                if (!setApproveOrDisapprovedRequest.year_level.Equals(String.Empty) && setApproveOrDisapprovedRequest.year_level > 0)
                {
                    studentLogin.Year = (short)setApproveOrDisapprovedRequest.year_level;
                    studentOenrp.YearLevel = (short)setApproveOrDisapprovedRequest.year_level;
                }
                if (!setApproveOrDisapprovedRequest.classification.Equals(String.Empty))
                {
                    studentOenrp.Classification = setApproveOrDisapprovedRequest.classification;
                }
                if (!setApproveOrDisapprovedRequest.allowed_units.Equals(String.Empty))
                {
                    studentLogin.AllowedUnits = (short)setApproveOrDisapprovedRequest.allowed_units;
                }
                if (!setApproveOrDisapprovedRequest.curr_year.Equals(String.Empty))
                {
                    studentLogin.CurrYear = (short)setApproveOrDisapprovedRequest.curr_year;
                }
                if (!setApproveOrDisapprovedRequest.mdn.Equals(String.Empty))
                {
                    if (studentInfo != null)
                    {
                        studentInfo.Mdn = setApproveOrDisapprovedRequest.mdn;
                    }
                    studentOenrp.Mdn = setApproveOrDisapprovedRequest.mdn;
                }

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVED_REGISTRATION_DEAN + ". Your ID Number is : " + final_id,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                if (studentOenrp.Dept.Equals("SH"))
                {
                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVED_REGISTRATION_DEAN + ". Additional Instructions: " + setApproveOrDisapprovedRequest.message + ". Your ID Number is : " + final_id,
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };
                }

                //This code is for pre-enrollmen auto-approve
                if (setApproveOrDisapprovedRequest.active_term == "20221")
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();

                    studentOenrp.Status = 6;

                    studentOenrp.SubmittedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    studentOenrp.EnrollmentDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.SELECTING_SUBJECTS,
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };

                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();

                    //Insert OSTSP if section is set by dean
                    /*var schedulesOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);

                    schedulesOstp.ToList().ForEach(x => x.Status = 1);
                    schedulesOstp.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                    _ucOnlinePortalContext.SaveChanges();*/


                    var dateAdjust = Strings.Format(DateAndTime.Now, "yyyy-MM-dd");
                    _ucOnlinePortalContext.Database.ExecuteSqlRaw("UPDATE ostsp SET status = 1, adjusted_on = '" + dateAdjust + "' WHERE stud_id = '" + setApproveOrDisapprovedRequest.id_number + "' and status != 2 and status = 0 and active_term = '" + setApproveOrDisapprovedRequest.active_term + "'");

                    studentOenrp.ApprovedDean = setApproveOrDisapprovedRequest.name_of_approver;
                    studentOenrp.ApprovedDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVED_BY_DEAN,
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };

                    _ucOnlinePortalContext.Notifications.Add(newNotif);

                    //If student is Accounting, Auto Approve!
                    if (!setApproveOrDisapprovedRequest.classification.Equals(String.Empty))
                    {
                        if (true)//setApproveOrDisapprovedRequest.classification.Equals("H"))
                        {
                            _ucOnlinePortalContext.SaveChanges();

                            studentOenrp.ApprovedAcctg = "AUTO-APPROVE";
                            studentOenrp.ApprovedAcctgOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                            if (studentOenrp.Dept == "CL")
                            {
                                if (studentOenrp.CourseCode == "MT")
                                {
                                    studentOenrp.NeededPayment = 1500;
                                }
                                else
                                {
                                    studentOenrp.NeededPayment = 500;
                                }
                            }
                            else
                            {
                                studentOenrp.NeededPayment = 200;
                            }

                            newNotif = new Notification
                            {
                                StudId = final_id,
                                NotifRead = 0,
                                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                Message = Literals.APPROVED_BY_ACCOUNTING,
                                ActiveTerm = setApproveOrDisapprovedRequest.active_term
                            };

                            studentOenrp.Status = 8;
                        }
                    }
                }
            }
            else if (setApproveOrDisapprovedRequest.status == 4)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.DisapprovedRegDean = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.DisapprovedRegDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVED_REGISTRATION_DEAN + " " + setApproveOrDisapprovedRequest.message,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };
            }
            else if (setApproveOrDisapprovedRequest.status == 5)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.SubmittedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                studentOenrp.EnrollmentDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.SELECTING_SUBJECTS,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };
            }
            else if (setApproveOrDisapprovedRequest.status == 6)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;
                //Insert OSTSP if section is set by dean
                var ostsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).Select(x => x.EdpCode).ToList();

                schedules = _ucOnlinePortalContext.Schedules.Where(x => ostsp.Contains(x.EdpCode) && x.Size >= x.MaxSize && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term).ToList();

                if (schedules.Count > 0)
                {
                    isFull = true;
                }
                else
                {
                    var schedulesAdd = _ucOnlinePortalContext.Schedules.Where(x => ostsp.Contains(x.EdpCode) && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
                    schedulesAdd.ToList().ForEach(x => x.Size = (x.Size + 1));
                    schedulesAdd.ToList().ForEach(x => x.PendingEnrolled = (x.PendingEnrolled.Value + 1));
                    _ucOnlinePortalContext.SaveChanges();

                    //Insert OSTSP if section is set by dean
                    var schedulesOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);

                    schedulesOstp.ToList().ForEach(x => x.Status = 1);
                    schedulesOstp.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                    _ucOnlinePortalContext.SaveChanges();

                    studentOenrp.ApprovedDean = setApproveOrDisapprovedRequest.name_of_approver;
                    studentOenrp.ApprovedDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVED_BY_DEAN,
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };

                    _ucOnlinePortalContext.Notifications.Add(newNotif);

                    //If student is Accounting, Auto Approve!
                    if (!setApproveOrDisapprovedRequest.classification.Equals(String.Empty))
                    {
                        if (setApproveOrDisapprovedRequest.classification.Equals("H"))
                        {
                            _ucOnlinePortalContext.SaveChanges();

                            studentOenrp.ApprovedAcctg = "AUTO-APPROVE";
                            studentOenrp.ApprovedAcctgOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                            studentOenrp.NeededPayment = 500;

                            newNotif = new Notification
                            {
                                StudId = final_id,
                                NotifRead = 0,
                                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                Message = Literals.APPROVED_BY_ACCOUNTING,
                                ActiveTerm = setApproveOrDisapprovedRequest.active_term
                            };

                            studentOenrp.Status = 8;

                            _ucOnlinePortalContext.Notifications.Add(newNotif);
                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }
                }
            }
            else if (setApproveOrDisapprovedRequest.status == 7)
            {
                studentOenrp.DisapprovedDeanOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVED_BY_DEAN + " " + setApproveOrDisapprovedRequest.message,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                //set back status for selecting subject
                studentOenrp.Status = (short)RequestResponse.Enums.EnrollmentStatus.APPROVED_REGISTRATION_DEAN;
                studentOenrp.SubmittedOn = null;

            }
            else if (setApproveOrDisapprovedRequest.status == 8)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.ApprovedAcctg = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.ApprovedAcctgOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                studentOenrp.NeededPayment = setApproveOrDisapprovedRequest.needed_payment;

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVED_BY_ACCOUNTING,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                if (setApproveOrDisapprovedRequest.needed_payment <= 0)
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();

                    studentOenrp.ApprovedCashier = setApproveOrDisapprovedRequest.name_of_approver;
                    studentOenrp.ApprovedCashierOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVED_BY_CASHIER,
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };

                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();

                    newNotif = new Notification
                    {
                        StudId = final_id,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.OFFICIALLY_ENROLLED + ". Please check your email, we sent a copy of your studyload.",
                        ActiveTerm = setApproveOrDisapprovedRequest.active_term
                    };

                    var ostsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.Status == 1 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
                    ostsp.ToList().ForEach(x => x.Status = 3);
                    ostsp.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));

                    var edpCode = ostsp.Select(x => x.EdpCode);

                    var schedulesUpdate = _ucOnlinePortalContext.Schedules.Where(x => edpCode.Contains(x.EdpCode) && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
                    schedulesUpdate.ToList().ForEach(x => x.PendingEnrolled = (x.PendingEnrolled.Value - 1));
                    schedulesUpdate.ToList().ForEach(x => x.OfficialEnrolled = (x.OfficialEnrolled.Value + 1));

                    _ucOnlinePortalContext.SaveChanges();

                    studentOenrp.Status = 10;
                    setApproveOrDisapprovedRequest.status = 9;
                }

            }
            else if (setApproveOrDisapprovedRequest.status == 9)
            {
                studentOenrp.Status = (short)setApproveOrDisapprovedRequest.status;

                studentOenrp.ApprovedCashier = setApproveOrDisapprovedRequest.name_of_approver;
                studentOenrp.ApprovedCashierOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVED_BY_CASHIER,
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                _ucOnlinePortalContext.Notifications.Add(newNotif);
                _ucOnlinePortalContext.SaveChanges();

                newNotif = new Notification
                {
                    StudId = final_id,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.OFFICIALLY_ENROLLED + ". Please check your email, we sent a copy of your studyload.",
                    ActiveTerm = setApproveOrDisapprovedRequest.active_term
                };

                var ostsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == setApproveOrDisapprovedRequest.id_number && x.Status == 1 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
                ostsp.ToList().ForEach(x => x.Status = 3);
                ostsp.ToList().ForEach(x => x.AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));

                var edpCode = ostsp.Select(x => x.EdpCode);

                var schedulesUpdate = _ucOnlinePortalContext.Schedules.Where(x => edpCode.Contains(x.EdpCode) && x.Status != 2 && x.ActiveTerm == setApproveOrDisapprovedRequest.active_term);
                schedulesUpdate.ToList().ForEach(x => x.PendingEnrolled = (x.PendingEnrolled.Value - 1));
                schedulesUpdate.ToList().ForEach(x => x.OfficialEnrolled = (x.OfficialEnrolled.Value + 1));

                _ucOnlinePortalContext.SaveChanges();

                studentOenrp.Status = 10;
            }

            if (setApproveOrDisapprovedRequest.status == 9)
            {
                //Send Offial Enrollment and Studyload
                GetStudyLoadRequest getRequest = new GetStudyLoadRequest { id_number = final_id, active_term = setApproveOrDisapprovedRequest.active_term };
                GetStudyLoadResponse response = GetStudyLoad(getRequest);

                StringBuilder constructStudyload = new StringBuilder();

                if (setApproveOrDisapprovedRequest.active_term.Equals("20221"))
                {
                    constructStudyload.Append("<h2><b>PRE-ENROLLED</b></h2></br>");
                }

                constructStudyload.Append("<table>");
                constructStudyload.Append("<tr><th>EDP CODE</th><th>SUBJECT NAME</th><th>TYPE</th><th>TIME</th><th>DAYS</th><th>UNITS</th></tr>");

                if (response.schedules.Count > 0)
                {
                    foreach (GetStudyLoadResponse.Schedules sched in response.schedules)
                    {
                        constructStudyload.Append("<tr>");
                        constructStudyload.Append("<td>" + sched.edp_code + "</td>");
                        constructStudyload.Append("<td>" + sched.subject_name + "</td>");
                        constructStudyload.Append("<td>" + sched.subject_type + "</td>");
                        constructStudyload.Append("<td>" + sched.begin_time + " - " + sched.end_time + " " + sched.mdn + "</td>");
                        constructStudyload.Append("<td>" + sched.days + "</td>");
                        constructStudyload.Append("<td>" + sched.units + "</td>");
                        constructStudyload.Append("</tr>");
                    }
                }
                constructStudyload.Append("</table>");

                var Tk = Task.Run(() =>
                {
                    var emailDetails = new EmailDetails
                    {
                        To = new EmailAddress { Address = studentLogin.Email, Name = studentLogin.FirstName + " " + studentLogin.LastName }

                    };
                    emailDetails.SpecificInfo.Add("{{code}}", constructStudyload.ToString());
                    _emailHandler.SendEmail(emailDetails, (int)RequestResponse.Enums.EmailType.OFFICIALENROLLMENT);
                });

                Tk.Wait();
            }
            else
            {
            }

            if (!isFull)
            {
                _ucOnlinePortalContext.Notifications.Add(newNotif);
                _ucOnlinePortalContext.Oenrps.Update(studentOenrp);
                _ucOnlinePortalContext.SaveChanges();

                return new SetApproveOrDisapprovedResponse { success = 1, id_number = final_id, edp_code = null };
            }
            else
            {
                List<string> edpcodes = new List<string>();

                if (studentOenrp.Dept.Equals("CL") && studentOenrp.Dept.Equals("Sh"))
                {
                    foreach (Schedule sched in schedules)
                    {
                        edpcodes.Add(sched.EdpCode);
                    }
                }
                else
                {
                    foreach (SchedulesBe sched in schedulesBe)
                    {
                        edpcodes.Add(sched.EdpCode);
                    }
                }

                return new SetApproveOrDisapprovedResponse { success = 0, id_number = final_id, edp_code = edpcodes };
            }
        }

        /*
       * Method to get open sections
       */

        public GetActiveSectionsResponse GetActiveSections(GetActiveSectionsRequest getActiveSectionsRequest)
        {
            string department = String.Empty;
            List<SchedulesBe> sectionsBe = new List<SchedulesBe>();

            department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == getActiveSectionsRequest.course_code).Select(x => x.Department).FirstOrDefault();

            //get schedules having the same coursecode and year
            var sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Contains(getActiveSectionsRequest.year_level.ToString()) && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();

            if (department.Equals("BE") || department.Equals("JH"))
            {
                sectionsBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Contains(getActiveSectionsRequest.year_level.ToString()) && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
            }
            else
            {

                if (getActiveSectionsRequest.year_level == 1118)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("1A") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1119)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("1P") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1218)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("2A") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();

                }
                else if (getActiveSectionsRequest.year_level == 1219)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("2P") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1128)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(1, 2).Equals("-A") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1129)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(1, 2).Equals("-P") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1228)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("-A") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.year_level == 1229)
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Substring(2, 2).Equals("-P") && !x.Section.Contains("XX") && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
                else if (getActiveSectionsRequest.course_code.Equals("MM") || getActiveSectionsRequest.course_code.Equals("MV"))
                {
                    sections = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getActiveSectionsRequest.course_code && x.Section.Contains(getActiveSectionsRequest.year_level.ToString()) && !x.Section.Contains("XX") && x.Status == 1 && x.ActiveTerm == getActiveSectionsRequest.active_term).ToList();
                }
            }

            List<string> sects = new List<string>();

            if (sectionsBe.Count > 0)
            {
                var sectionList = sectionsBe.OrderBy(x => x.Section).ToList();

                //algo to check every subject in section. if section has 1 full subject, dont include
                if (sectionList != null)
                {
                    string currentSec = sectionList.First().Section;
                    bool valid = true;

                    foreach (SchedulesBe sched in sectionList)
                    {
                        if (!currentSec.Equals(sched.Section))
                        {
                            if (valid)
                            {
                                sects.Add(currentSec);
                            }

                            currentSec = sched.Section;
                            valid = true;
                        }

                        if (valid)
                        {
                            valid = sched.MaxSize - sched.Size >= 1 ? true : false;
                        }
                    }

                    if (valid)
                    {
                        sects.Add(currentSec);
                    }
                }
            }
            else
            {
                //sort by section'
                if (sections.Count > 0)
                {
                    var sectionList = sections.OrderBy(x => x.Section).ToList();

                    //algo to check every subject in section. if section has 1 full subject, dont include
                    if (sectionList != null)
                    {
                        string currentSec = sectionList.First().Section;
                        bool valid = true;

                        foreach (Schedule sched in sectionList)
                        {
                            if (!currentSec.Equals(sched.Section))
                            {
                                if (valid)
                                {
                                    sects.Add(currentSec);
                                }

                                currentSec = sched.Section;
                                valid = true;
                            }

                            if (valid)
                            {
                                valid = sched.MaxSize - sched.Size >= 1 ? true : false;
                            }
                        }

                        if (valid)
                        {
                            sects.Add(currentSec);
                        }
                    }
                }
            }

            return new GetActiveSectionsResponse { sections = sects };
        }

        /*
        * Method to get student request
        */

        public StudentReqResponse GetStudentRequest(StudentReqRequest studentReqRequest)
        {
            //always get status 5 -> for dean
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          where Oenrp.Status == 5 && (Oenrp.RequestDeblock == 1 || Oenrp.RequestOverload == 1)
                          && Oenrp.ActiveTerm == studentReqRequest.active_term && _courseList.ActiveTerm == studentReqRequest.active_term
                          select new StudentReqResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              status = Oenrp.Status,
                              date = Oenrp.RegisteredOn,
                              request_overload = (int)Oenrp.RequestOverload,
                              request_deblock = (int)Oenrp.RequestDeblock
                          });

            if (!studentReqRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == studentReqRequest.course_department && x.ActiveTerm == studentReqRequest.active_term).ToList();
                var courses = courseList.Select(x => x.CourseCode).ToList();

                result = result.Where(x => courses.Contains(x.course_code));
            }

            var count = result.Count();

            return new StudentReqResponse { students = result.ToList(), count = count };
        }

        /*
        * Method to apply request
        */
        public ApplyReqResponse ApplyRequest(ApplyReqRequest applyReqRequest)
        {
            var student = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == applyReqRequest.stud_id).FirstOrDefault();

            student.RequestDeblock = (short)applyReqRequest.request_deblock;
            student.RequestOverload = (short)applyReqRequest.request_overload;

            _ucOnlinePortalContext.SaveChanges();

            return new ApplyReqResponse { success = 1 };
        }


        /*
        * Method to approve request
        */
        public ApproveReqResponse ApproveRequest(ApproveReqRequest approveReqRequest)
        {
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == approveReqRequest.id_number && x.ActiveTerm == approveReqRequest.active_term).FirstOrDefault();
            var login = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == approveReqRequest.id_number).FirstOrDefault();

            Notification newNotif = new Notification();

            if (approveReqRequest.approved_overload == 2)
            {
                studOenrp.RequestOverload = 2;
                login.AllowedUnits = (short)approveReqRequest.max_units;

                newNotif = new Notification
                {
                    StudId = approveReqRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVE_OVERLOAD + " " + approveReqRequest.max_units,
                    ActiveTerm = approveReqRequest.active_term
                };
            }
            else if (approveReqRequest.approved_overload == 3)
            {
                studOenrp.RequestOverload = 3;
                newNotif = new Notification
                {
                    StudId = approveReqRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVE_OVERLOAD,
                    ActiveTerm = approveReqRequest.active_term
                };
            }


            if (approveReqRequest.approved_deblock == 2)
            {
                studOenrp.RequestDeblock = 2;
                studOenrp.Section = "";
                newNotif = new Notification
                {
                    StudId = approveReqRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVE_DE_BLOCK,
                    ActiveTerm = approveReqRequest.active_term
                };

                studOenrp.SubmittedOn = null;
                studOenrp.EnrollmentDate = null;


                _ucOnlinePortalContext.Ostsps.RemoveRange(_ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveReqRequest.id_number && x.ActiveTerm == approveReqRequest.active_term));
            }
            if (approveReqRequest.approved_deblock == 3)
            {
                studOenrp.RequestDeblock = 3;
                newNotif = new Notification
                {
                    StudId = approveReqRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVE_DE_BLOCK,
                    ActiveTerm = approveReqRequest.active_term
                };
            }

            if (approveReqRequest.approved_promissory == 3)
            {
                studOenrp.RequestPromissory = 3;

                if (studOenrp.PromiPay != approveReqRequest.promise_pay)
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY + " but changed the promissory amount",
                        ActiveTerm = approveReqRequest.active_term
                    };
                }
                else
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                var promiP = studOenrp.PromiPay;

                if (approveReqRequest.promise_pay == 0)
                {
                    studOenrp.PromiPay = promiP;
                }
                else
                {
                    studOenrp.PromiPay = approveReqRequest.promise_pay;
                }

                if (!approveReqRequest.message.Equals(String.Empty))
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();
                    newNotif = new Notification

                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = "Promissory Message: " + approveReqRequest.message,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

            }

            var examPromi = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == approveReqRequest.id_number && x.ActiveTerm == approveReqRequest.active_term).FirstOrDefault();

            if (approveReqRequest.approved_prelim_promissory.HasValue && approveReqRequest.approved_prelim_promissory.Value == 3)
            {
                examPromi.RequestPrelim = 3;

                if (approveReqRequest.promise_pay == 0)
                {
                    examPromi.RequestPrelimAmount = examPromi.RequestPrelimAmount;
                }

                if (examPromi.RequestPrelimAmount != approveReqRequest.promise_pay)
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY + " but changed the promissory amount",
                        ActiveTerm = approveReqRequest.active_term
                    };

                    examPromi.RequestPrelimAmount = approveReqRequest.promise_pay;
                }
                else
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                if (!approveReqRequest.message.Equals(String.Empty))
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();
                    newNotif = new Notification

                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = "Promissory Message: " + approveReqRequest.message,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                _ucOnlinePortalContext.ExamPromissories.Update(examPromi);
            }
            else if (approveReqRequest.approved_midterm_promissory.HasValue && approveReqRequest.approved_midterm_promissory.Value == 3)
            {
                examPromi.RequestMidterm = 3;

                if (approveReqRequest.promise_pay == 0)
                {
                    examPromi.RequestMidtermAmount = examPromi.RequestMidtermAmount;
                }

                if (examPromi.RequestMidtermAmount != approveReqRequest.promise_pay)
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY + " but changed the promissory amount",
                        ActiveTerm = approveReqRequest.active_term
                    };

                    examPromi.RequestPrelimAmount = approveReqRequest.promise_pay;
                }
                else
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                if (!approveReqRequest.message.Equals(String.Empty))
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();
                    newNotif = new Notification

                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = "Promissory Message: " + approveReqRequest.message,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                _ucOnlinePortalContext.ExamPromissories.Update(examPromi);
            }
            else if (approveReqRequest.approved_semi_promissory.HasValue && approveReqRequest.approved_semi_promissory.Value == 3)
            {
                examPromi.RequestSemi = 3;

                if (approveReqRequest.promise_pay == 0)
                {
                    examPromi.RequestSemiAmount = examPromi.RequestSemiAmount;
                }

                if (examPromi.RequestSemiAmount != approveReqRequest.promise_pay)
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY + " but changed the promissory amount",
                        ActiveTerm = approveReqRequest.active_term
                    };

                    examPromi.RequestPrelimAmount = approveReqRequest.promise_pay;
                }
                else
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                if (!approveReqRequest.message.Equals(String.Empty))
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();
                    newNotif = new Notification

                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = "Promissory Message: " + approveReqRequest.message,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                _ucOnlinePortalContext.ExamPromissories.Update(examPromi);
            }
            else if (approveReqRequest.approved_final_promissory.HasValue && approveReqRequest.approved_final_promissory.Value == 3)
            {
                examPromi.RequestFinal = 3;

                if (approveReqRequest.promise_pay == 0)
                {
                    examPromi.RequestFinalAmount = examPromi.RequestFinalAmount;
                }

                if (examPromi.RequestFinalAmount != approveReqRequest.promise_pay)
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY + " but changed the promissory amount",
                        ActiveTerm = approveReqRequest.active_term
                    };

                    examPromi.RequestPrelimAmount = approveReqRequest.promise_pay;
                }
                else
                {
                    newNotif = new Notification
                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = Literals.APPROVE_PROMISSORY,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                if (!approveReqRequest.message.Equals(String.Empty))
                {
                    _ucOnlinePortalContext.Notifications.Add(newNotif);
                    _ucOnlinePortalContext.SaveChanges();
                    newNotif = new Notification

                    {
                        StudId = approveReqRequest.id_number,
                        NotifRead = 0,
                        Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        Message = "Promissory Message: " + approveReqRequest.message,
                        ActiveTerm = approveReqRequest.active_term
                    };
                }

                _ucOnlinePortalContext.ExamPromissories.Update(examPromi);
            }

            _ucOnlinePortalContext.Oenrps.Update(studOenrp);
            _ucOnlinePortalContext.Notifications.Add(newNotif);
            _ucOnlinePortalContext.SaveChanges();
            return new ApproveReqResponse { success = 1 };
        }

        /*
       * Method to get sections 
       */
        public GetSectionResponse GetSection(GetSectionRequest getSectionRequest)
        {
            List<Schedule> schedules = new List<Schedule>();
            List<SchedulesBe> schedulesBe = new List<SchedulesBe>();

            var department = String.Empty;

            department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == getSectionRequest.course_code).Select(x => x.Department).FirstOrDefault();

            if (department.Equals("BE") || department.Equals("JH"))
            {
                if (!getSectionRequest.college_abbr.Equals(String.Empty))
                {
                    var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getSectionRequest.college_abbr && x.ActiveTerm == getSectionRequest.active_term).ToList();
                    var courses = courseList.Select(x => x.CourseCode).ToList();

                    schedulesBe = _ucOnlinePortalContext.SchedulesBes.Where(x => courses.Contains(x.CourseCode) && x.ActiveTerm == getSectionRequest.active_term).ToList();
                }
                if (!getSectionRequest.course_code.Equals(String.Empty))
                {
                    if (schedulesBe.Count > 0)
                    {
                        schedulesBe = schedulesBe.Where(x => x.CourseCode == getSectionRequest.course_code).ToList();
                    }
                    else
                    {
                        schedulesBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.CourseCode == getSectionRequest.course_code && x.ActiveTerm == getSectionRequest.active_term).ToList();
                    }
                }
                if (!getSectionRequest.course_code.Equals(String.Empty))
                {
                    if (getSectionRequest.year_level != 0)
                    {

                        schedulesBe = schedulesBe.Where(x => x.Section.Contains(getSectionRequest.year_level.ToString())).ToList();

                    }
                }

                schedulesBe = schedulesBe.GroupBy(x => x.Section).Select(grp => grp.First()).ToList();

                var result = schedulesBe.Select(x => new GetSectionResponse.sections
                {
                    course_code = x.CourseCode,
                    section = x.Section
                });

                return new GetSectionResponse { section = result.ToList() };
            }
            else
            {
                if (!getSectionRequest.college_abbr.Equals(String.Empty))
                {
                    var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getSectionRequest.college_abbr && x.ActiveTerm == getSectionRequest.active_term).ToList();
                    var courses = courseList.Select(x => x.CourseCode).ToList();

                    schedules = _ucOnlinePortalContext.Schedules.Where(x => courses.Contains(x.CourseCode) && x.ActiveTerm == getSectionRequest.active_term).ToList();
                }
                if (!getSectionRequest.course_code.Equals(String.Empty))
                {
                    if (schedules.Count > 0)
                    {
                        schedules = schedules.Where(x => x.CourseCode == getSectionRequest.course_code).ToList();
                    }
                    else
                    {
                        schedules = _ucOnlinePortalContext.Schedules.Where(x => x.CourseCode == getSectionRequest.course_code && x.ActiveTerm == getSectionRequest.active_term).ToList();
                    }
                }
                if (!getSectionRequest.course_code.Equals(String.Empty))
                {
                    if (getSectionRequest.year_level != 0)
                    {
                        if (getSectionRequest.year_level == 111)
                        {
                            schedules = schedules.Where(x => x.Section.Substring(2, 1).Equals("1")).ToList();
                        }
                        else if (getSectionRequest.year_level == 121)
                        {
                            schedules = schedules.Where(x => x.Section.Substring(2, 1).Equals("2")).ToList();
                        }
                        else if (getSectionRequest.year_level == 112)
                        {
                            schedules = schedules.Where(x => x.Section.Substring(1, 1).Equals("-")).ToList();
                        }
                        else if (getSectionRequest.year_level == 122)
                        {
                            schedules = schedules.Where(x => x.Section.Substring(2, 1).Equals("-")).ToList();
                        }
                        else
                        {
                            schedules = schedules.Where(x => x.Section.Contains(getSectionRequest.year_level.ToString())).ToList();
                        }
                    }
                }

                schedules = schedules.GroupBy(x => x.Section).Select(grp => grp.First()).ToList();

                var result = schedules.Select(x => new GetSectionResponse.sections
                {
                    course_code = x.CourseCode,
                    section = x.Section
                });

                return new GetSectionResponse { section = result.ToList() };
            }
        }

        /*
       * Method to update status sections 
       */
        public ChangeSchedStatusResponse ChangeSchedStatus(ChangeSchedStatusRequest changeSchedStatusRequest)
        {
            if (!changeSchedStatusRequest.course_code.Equals(String.Empty) && !changeSchedStatusRequest.section.Equals(String.Empty))
            {
                var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.Section == changeSchedStatusRequest.section && x.CourseCode == changeSchedStatusRequest.course_code && x.ActiveTerm == changeSchedStatusRequest.active_term);
                schedule.ToList().ForEach(x => x.Status = (short)changeSchedStatusRequest.status);
                _ucOnlinePortalContext.SaveChanges();
            }

            if (!changeSchedStatusRequest.edp_code.Equals(String.Empty) && changeSchedStatusRequest.edp_code.Count > 0)
            {
                var schedules = _ucOnlinePortalContext.Schedules.Where(x => changeSchedStatusRequest.edp_code.Contains(x.EdpCode) && x.ActiveTerm == changeSchedStatusRequest.active_term);
                schedules.ToList().ForEach(x => x.Status = (short)changeSchedStatusRequest.status);
                _ucOnlinePortalContext.SaveChanges();
            }

            return new ChangeSchedStatusResponse { success = 1 };
        }

        /*
       * Method to cancel enrollment
       */
        public CancelEnrollmentResponse CancelEnrollment(CancelEnrollmentRequest cancelEnrollmentRequest)
        {
            var studentOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == cancelEnrollmentRequest.id_number && x.ActiveTerm == cancelEnrollmentRequest.active_term).FirstOrDefault();

            if (studentOenrp != null)
            {
                studentOenrp.Status = (short)EnrollmentStatus.CANCELLED;
            }

            var studentOtsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == cancelEnrollmentRequest.id_number && x.ActiveTerm == cancelEnrollmentRequest.active_term).ToList();
            var edpCodes = studentOtsp.Select(x => x.EdpCode).ToList();

            if (studentOtsp.Count > 0)
            {
                studentOtsp.ForEach(x => x.Status = 2);

                var schedules = _ucOnlinePortalContext.Schedules.Where(x => edpCodes.Contains(x.EdpCode) && x.ActiveTerm == cancelEnrollmentRequest.active_term).ToList();
                schedules.ForEach(x => x.Size = (x.Size - 1));
            }

            Notification newNotif = new Notification
            {
                StudId = cancelEnrollmentRequest.id_number,
                NotifRead = 0,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = Literals.CANCELLED,
                ActiveTerm = cancelEnrollmentRequest.active_term
            };

            _ucOnlinePortalContext.Notifications.Add(newNotif);
            _ucOnlinePortalContext.SaveChanges();

            return new CancelEnrollmentResponse { success = 1 };
        }

        /*
       * Method to get status count
       */
        public GetStatusCountResponse GetStatusCount(GetStatusCountRequest getStatusCountRequest)
        {
            int requestCount = 0;
            int pendingPromissory = 0;
            int approvedPromissory = 0;
            int pendingAdjustment = 0;
            int approvedAdjustment = 0;
            int disapproveAdjustment = 0;

            int pending_prelim = 0;
            int approve_prelim = 0;
            int pending_midterm = 0;
            int approve_midterm = 0;
            int pending_semi = 0;
            int approve_semi = 0;
            int pending_final = 0;
            int approve_final = 0;

            int ack_prelim = 0;
            int notack_prelim = 0;


            Dictionary<int, int> counts = new Dictionary<int, int>();

            List<string> courses = new List<string>();
            if (!getStatusCountRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getStatusCountRequest.course_department && x.ActiveTerm == getStatusCountRequest.active_term).ToList();
                courses = courseList.Select(x => x.CourseCode).ToList();

                var count_dissapprove = _ucOnlinePortalContext.Oenrps.Where(x => courses.Contains(x.CourseCode) && x.DisapprovedDeanOn != null && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                counts.Add(7, count_dissapprove);

                var count_approved_dean = _ucOnlinePortalContext.Oenrps.Where(x => courses.Contains(x.CourseCode) && x.ApprovedDeanOn != null && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                counts.Add(6, count_approved_dean);
            }

            if (!getStatusCountRequest.course_department.Equals(String.Empty))
            {
                foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).GroupBy(x => x.Status)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
                {
                    if (line.Metric != 6)
                        counts.Add(line.Metric, line.Count);
                }


                requestCount = _ucOnlinePortalContext.Oenrps.Where(x => courses.Contains(x.CourseCode) && (x.RequestOverload == 1 || x.RequestDeblock == 1) && x.ActiveTerm == getStatusCountRequest.active_term).Count();
            }
            else
            {
                var count_dissapprove = _ucOnlinePortalContext.Oenrps.Where(x => x.DisapprovedDeanOn != null && x.ActiveTerm == getStatusCountRequest.active_term).Count();

                counts.Add(7, count_dissapprove);

                var count_accounting = _ucOnlinePortalContext.Oenrps.Where(x => x.ApprovedAcctgOn != null && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                counts.Add(14, count_accounting);

                foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => x.ActiveTerm == getStatusCountRequest.active_term).GroupBy(x => x.Status)
                       .Select(group => new
                       {
                           Metric = group.Key,
                           Count = group.Count()
                       })
                       .OrderBy(x => x.Metric))
                {
                    counts.Add(line.Metric, line.Count);
                }
            }

            List<int> count = new List<int>();
            for (int counter = 0; counter < 15; counter++)
            {
                if (counts.ContainsKey(counter))
                {
                    if (counter == 8)
                    {
                        var studIdNumbers = _ucOnlinePortalContext.Attachments.Where(x => x.Type.Equals("Payment") && x.ActiveTerm == getStatusCountRequest.active_term).Select(x => x.StudId).Distinct();
                        var countCashier = _ucOnlinePortalContext.Oenrps.Where(x => x.Status == 8 && studIdNumbers.Contains(x.StudId) && x.ActiveTerm == getStatusCountRequest.active_term).Count();

                        count.Add(countCashier);
                    }
                    else
                    {
                        count.Add(counts[counter]);
                    }
                }
                else
                {
                    count.Add(0);
                }
            }

            if (!getStatusCountRequest.course_department.Equals(String.Empty))
            {
                pendingPromissory = _ucOnlinePortalContext.Oenrps.Where(x => (x.RequestPromissory == 1) && courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                approvedPromissory = _ucOnlinePortalContext.Oenrps.Where(x => (x.RequestPromissory == 3) && courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).Count();

                pendingAdjustment = _ucOnlinePortalContext.Oenrps.Where(x => (x.AdjustmentCount == 0) && courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                approvedAdjustment = _ucOnlinePortalContext.Oenrps.Where(x => (x.AdjustmentCount == 9) && courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                disapproveAdjustment = _ucOnlinePortalContext.Oenrps.Where(x => (x.AdjustmentCount == 8) && courses.Contains(x.CourseCode) && x.ActiveTerm == getStatusCountRequest.active_term).Count();

                pending_prelim = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                  join Oenrp in _ucOnlinePortalContext.Oenrps
                                  on _promi.StudId equals Oenrp.StudId
                                  where courses.Contains(Oenrp.CourseCode) && (_promi.RequestPrelim == 1)
                                  && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                  select new {
                                      id_number = Oenrp.StudId
                                  }).Count();

                approve_prelim = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                  join Oenrp in _ucOnlinePortalContext.Oenrps
                                  on _promi.StudId equals Oenrp.StudId
                                  where courses.Contains(Oenrp.CourseCode) && (_promi.RequestPrelim == 3)
                                   && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                  select new
                                  {
                                      id_number = Oenrp.StudId
                                  }).Count();

                pending_midterm = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                   join Oenrp in _ucOnlinePortalContext.Oenrps
                                   on _promi.StudId equals Oenrp.StudId
                                   where courses.Contains(Oenrp.CourseCode) && (_promi.RequestMidterm == 1)
                                    && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                   select new
                                   {
                                       id_number = Oenrp.StudId
                                   }).Count();

                approve_midterm = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                   join Oenrp in _ucOnlinePortalContext.Oenrps
                                   on _promi.StudId equals Oenrp.StudId
                                   where courses.Contains(Oenrp.CourseCode) && (_promi.RequestMidterm == 3)
                                    && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                   select new
                                   {
                                       id_number = Oenrp.StudId
                                   }).Count();

                pending_semi = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                join Oenrp in _ucOnlinePortalContext.Oenrps
                                on _promi.StudId equals Oenrp.StudId
                                where courses.Contains(Oenrp.CourseCode) && (_promi.RequestSemi == 1)
                                && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                select new
                                {
                                    id_number = Oenrp.StudId
                                }).Count();

                approve_semi = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                join Oenrp in _ucOnlinePortalContext.Oenrps
                                on _promi.StudId equals Oenrp.StudId
                                where courses.Contains(Oenrp.CourseCode) && (_promi.RequestSemi == 3)
                                && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                select new
                                {
                                    id_number = Oenrp.StudId
                                }).Count();

                pending_final = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                 join Oenrp in _ucOnlinePortalContext.Oenrps
                                 on _promi.StudId equals Oenrp.StudId
                                 where courses.Contains(Oenrp.CourseCode) && (_promi.RequestFinal == 1)
                                 && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                 select new
                                 {
                                     id_number = Oenrp.StudId
                                 }).Count();

                approve_final = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                 join Oenrp in _ucOnlinePortalContext.Oenrps
                                 on _promi.StudId equals Oenrp.StudId
                                 where courses.Contains(Oenrp.CourseCode) && (_promi.RequestFinal == 3)
                                 && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                 select new
                                 {
                                     id_number = Oenrp.StudId
                                 }).Count();
            }
            else
            {
                pendingPromissory = _ucOnlinePortalContext.Oenrps.Where(x => (x.RequestPromissory == 2) && x.ActiveTerm == getStatusCountRequest.active_term).Count();
                approvedPromissory = _ucOnlinePortalContext.Oenrps.Where(x => (x.RequestPromissory == 3) && x.ActiveTerm == getStatusCountRequest.active_term).Count();

                pending_prelim = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                  join Oenrp in _ucOnlinePortalContext.Oenrps
                                  on _promi.StudId equals Oenrp.StudId
                                  where (_promi.RequestPrelim == 2)
                                  && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                  select new
                                  {
                                      id_number = Oenrp.StudId
                                  }).Count();

                approve_prelim = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                  join Oenrp in _ucOnlinePortalContext.Oenrps
                                  on _promi.StudId equals Oenrp.StudId
                                  where (_promi.RequestPrelim == 3)
                                  && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                  select new
                                  {
                                      id_number = Oenrp.StudId
                                  }).Count();

                pending_midterm = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                   join Oenrp in _ucOnlinePortalContext.Oenrps
                                   on _promi.StudId equals Oenrp.StudId
                                   where (_promi.RequestMidterm == 2)
                                   && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                   select new
                                   {
                                       id_number = Oenrp.StudId
                                   }).Count();

                approve_midterm = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                   join Oenrp in _ucOnlinePortalContext.Oenrps
                                   on _promi.StudId equals Oenrp.StudId
                                   where (_promi.RequestMidterm == 3)
                                   && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                   select new
                                   {
                                       id_number = Oenrp.StudId
                                   }).Count();

                pending_semi = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                join Oenrp in _ucOnlinePortalContext.Oenrps
                                on _promi.StudId equals Oenrp.StudId
                                where (_promi.RequestSemi == 2)
                                && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                select new
                                {
                                    id_number = Oenrp.StudId
                                }).Count();

                approve_semi = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                join Oenrp in _ucOnlinePortalContext.Oenrps
                                on _promi.StudId equals Oenrp.StudId
                                where (_promi.RequestSemi == 3)
                                && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                select new
                                {
                                    id_number = Oenrp.StudId
                                }).Count();

                pending_final = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                 join Oenrp in _ucOnlinePortalContext.Oenrps
                                 on _promi.StudId equals Oenrp.StudId
                                 where (_promi.RequestFinal == 2)
                                 && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                 select new
                                 {
                                     id_number = Oenrp.StudId
                                 }).Count();

                approve_final = (from _promi in _ucOnlinePortalContext.ExamPromissories
                                 join Oenrp in _ucOnlinePortalContext.Oenrps
                                 on _promi.StudId equals Oenrp.StudId
                                 where (_promi.RequestFinal == 3)
                                 && _promi.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                 select new
                                 {
                                     id_number = Oenrp.StudId
                                 }).Count();

                notack_prelim = (from _attach in _ucOnlinePortalContext.Attachments
                                 join Oenrp in _ucOnlinePortalContext.Oenrps
                                 on _attach.StudId equals Oenrp.StudId
                                 where (_attach.Filename.Contains("_[payment]_[") && _attach.Acknowledged == 0)
                                 && _attach.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                                 select new
                                 {
                                     id_number = Oenrp.StudId
                                 }).Distinct().Count();


                ack_prelim = (from _attach in _ucOnlinePortalContext.Attachments
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _attach.StudId equals Oenrp.StudId
                              where (_attach.Filename.Contains("_[payment]_[") && _attach.Acknowledged == 1)
                              && _attach.ActiveTerm == getStatusCountRequest.active_term && Oenrp.ActiveTerm == getStatusCountRequest.active_term
                              select new
                              {
                                  id_number = Oenrp.StudId
                              }).Distinct().Count();
            }

            GetStatusCountResponse response = new GetStatusCountResponse
            {
                registered = count[0],
                approved_registration_registrar = count[1],
                disapproved_registration_registrar = count[2],
                approved_registration_dean = count[3],
                disapproved_registration_dean = count[4],
                selecting_subjects = count[5] - requestCount,
                approved_by_dean = count[6],
                disapproved_by_dean = count[7],
                approved_by_accounting = count[8],
                approved_by_cashier = count[9],
                officially_enrolled = count[10],
                withdrawn_enrollment_before_start_of_class = count[11],
                withdrawn_enrollment_start_of_class = count[12],
                cancelled = count[13],
                accounting_count = count[14],
                request = requestCount,
                pending_promissory = pendingPromissory,
                approved_promissory = approvedPromissory,
                pending_adjustment = pendingAdjustment,
                approved_adjustment = approvedAdjustment,
                disapproved_adjustment = disapproveAdjustment,
                pending_prelim = pending_prelim,
                approve_prelim = approve_prelim,
                pending_midterm = pending_midterm,
                approve_midterm = approve_midterm,
                pending_semi = pending_semi,
                approve_semi = approve_semi,
                pending_final = pending_final,
                approve_final = approve_final,
                ack_receipts = ack_prelim,
                notaack_receipts = notack_prelim
            };

            return response;
        }

        /*
        * Method to save payments
        */
        public SavePaymentResponse SavePayments(SavePaymentRequest savePaymentsRequest)
        {
            if (savePaymentsRequest.attachments.Count > 0)
            {
                foreach (SavePaymentRequest.Attachmentss attachment in savePaymentsRequest.attachments)
                {
                    Attachment newAttachment = new Attachment
                    {
                        StudId = savePaymentsRequest.id_number,
                        Email = attachment.email,
                        Filename = attachment.filename,
                        Type = "Payment",
                        Acknowledged = 0,
                        ActiveTerm = savePaymentsRequest.active_term
                    };

                    _ucOnlinePortalContext.Attachments.Add(newAttachment);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            return new SavePaymentResponse { succcess = 1 };
        }

        /*
       * Method to view classlist
       */
        public ViewClasslistResponse ViewClasslist(ViewClasslistRequest viewClasslistRequest)
        {
            List<ViewClasslistResponse.Enrolled> result = new List<ViewClasslistResponse.Enrolled>();

            var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == viewClasslistRequest.edp_code && x.ActiveTerm == viewClasslistRequest.active_term).FirstOrDefault();
            var scheduleBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == viewClasslistRequest.edp_code && x.ActiveTerm == viewClasslistRequest.active_term).FirstOrDefault();

            var courC = "";

            if (schedule != null)
            {
                courC = schedule.CourseCode;

                var depart = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == courC && x.ActiveTerm == viewClasslistRequest.active_term).Select(x => x.Department).FirstOrDefault();

                if (depart.Equals("CL"))
                {

                    using (var command = _ucOnlinePortalContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "select * from ostsp a join schedules b on a.edp_code = b.edp_code join login_info c on a.stud_id = c.stud_id join course_list d on c.course_code = d.course_code left join grades_cl e on a.stud_id = e.stud_id and e.edp_code = '" + schedule.EdpCode + "' and e.active_term = '" + viewClasslistRequest.active_term + "' where a.edp_code = '" + viewClasslistRequest.edp_code + "' and a.active_term = '" + viewClasslistRequest.active_term + "' and b.active_term = '" + viewClasslistRequest.active_term + "' and d.active_term = '" + viewClasslistRequest.active_term + "'";
                        _ucOnlinePortalContext.Database.OpenConnection();

                        using (var res = command.ExecuteReader())
                        {
                            while (res.Read())
                            {
                                result.Add(new ViewClasslistResponse.Enrolled
                                {
                                    id_number = res.GetValue(1).ToString(),
                                    last_name = res.GetValue(37).ToString(),
                                    firstname = res.GetValue(38).ToString(),
                                    course_year = res.GetValue(59).ToString() + " " + res.GetValue(45).ToString(),
                                    mobile_number = res.GetValue(47).ToString(),
                                    email = res.GetValue(48).ToString(),
                                    status = int.Parse(res.GetValue(3).ToString()),
                                    gender = res.GetValue(46).ToString(),
                                    grade1 = res.GetValue(73) == DBNull.Value ? String.Empty : res.GetValue(73).ToString(),
                                    grade2 = res.GetValue(74) == DBNull.Value ? String.Empty : res.GetValue(74).ToString()
                                });
                            }
                        }
                    }
                }
                else
                {
                    using (var command = _ucOnlinePortalContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "select * from ostsp a join schedules b on a.edp_code = b.edp_code join login_info c on a.stud_id = c.stud_id join course_list d on c.course_code = d.course_code left join grades_sh e on a.stud_id = e.stud_id and e.edp_code = '" + schedule.EdpCode + "' and e.active_term = '" + viewClasslistRequest.active_term + "' where a.edp_code = '" + viewClasslistRequest.edp_code + "' and a.active_term = '" + viewClasslistRequest.active_term + "' and b.active_term = '" + viewClasslistRequest.active_term + "' and d.active_term = '" + viewClasslistRequest.active_term + "'";
                        _ucOnlinePortalContext.Database.OpenConnection();

                        using (var res = command.ExecuteReader())
                        {
                            while (res.Read())
                            {
                                result.Add(new ViewClasslistResponse.Enrolled
                                {
                                    id_number = res.GetValue(1).ToString(),
                                    last_name = res.GetValue(37).ToString(),
                                    firstname = res.GetValue(38).ToString(),
                                    course_year = res.GetValue(59).ToString() + " " + res.GetValue(45).ToString(),
                                    mobile_number = res.GetValue(47).ToString(),
                                    email = res.GetValue(48).ToString(),
                                    status = int.Parse(res.GetValue(3).ToString()),
                                    gender = res.GetValue(46).ToString(),
                                    grade1 = res.GetValue(73) == DBNull.Value ? String.Empty : res.GetValue(73).ToString(),
                                    grade2 = res.GetValue(74) == DBNull.Value ? String.Empty : res.GetValue(74).ToString()
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                courC = scheduleBe.CourseCode;

                using (var command = _ucOnlinePortalContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "select * from ostsp a join schedules_be b on a.edp_code = b.edp_code join login_info c on a.stud_id = c.stud_id join course_list d on c.course_code = d.course_code left join grade_evaluation_be e on a.stud_id = e.stud_id and e.int_code = '" + scheduleBe.InternalCode + "' and e.term = '" + viewClasslistRequest.active_term + "' where a.edp_code = '" + viewClasslistRequest.edp_code + "' and a.active_term = '" + viewClasslistRequest.active_term + "' and b.active_term = '" + viewClasslistRequest.active_term + "' and d.active_term = '" + viewClasslistRequest.active_term + "'";
                    _ucOnlinePortalContext.Database.OpenConnection();

                    using (var res = command.ExecuteReader())
                    {
                        while (res.Read())
                        {
                            result.Add(new ViewClasslistResponse.Enrolled
                            {
                                id_number = res.GetValue(1).ToString(),
                                last_name = res.GetValue(37).ToString(),
                                firstname = res.GetValue(38).ToString(),
                                course_year = res.GetValue(59).ToString() + " " + res.GetValue(45).ToString(),
                                mobile_number = res.GetValue(47).ToString(),
                                email = res.GetValue(48).ToString(),
                                status = int.Parse(res.GetValue(3).ToString()),
                                gender = res.GetValue(46).ToString(),
                                grade1 = res.GetValue(72) == DBNull.Value ? String.Empty : res.GetValue(72).ToString(),
                                grade2 = res.GetValue(73) == DBNull.Value ? String.Empty : res.GetValue(73).ToString(),
                                grade3 = res.GetValue(74) == DBNull.Value ? String.Empty : res.GetValue(74).ToString(),
                                grade4 = res.GetValue(75) == DBNull.Value ? String.Empty : res.GetValue(75).ToString()
                            });
                        }
                    }
                }
            }

            var OfficialEnrolled = result.Where(x => x.status == 3).OrderBy(x => x.last_name).ToList();
            var PendingEnrolled = result.Where(x => x.status == 1).OrderBy(x => x.last_name).ToList();

            var department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == courC && x.ActiveTerm == viewClasslistRequest.active_term).Select(x => x.Department).FirstOrDefault();

            if ((department.Equals("SH") || department.Equals("JH") || department.Equals("BE")))
            {
                OfficialEnrolled = result.Where(x => x.status == 3).OrderBy(x => x.gender).ThenBy(x => x.last_name).ToList();
                PendingEnrolled = result.Where(x => x.status == 1).OrderBy(x => x.gender).ThenBy(x => x.last_name).ToList();
            }

            var NotAcceptedSection = result.Where(x => x.status == 0).OrderBy(x => x.last_name).ToList();
            List<ViewClasslistResponse.Enrolled> not_accepted = new List<ViewClasslistResponse.Enrolled>();

            foreach (ViewClasslistResponse.Enrolled enr in NotAcceptedSection)
            {
                var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == enr.id_number).FirstOrDefault();

                if (studOenrp.Section != null && !studOenrp.Section.Equals(String.Empty))
                {
                    not_accepted.Add(enr);
                }
            }

            ViewClasslistResponse response = null;

            if (department.Equals("CL") || department.Equals("SH"))
            {
                response = new ViewClasslistResponse
                {
                    edp_code = schedule.EdpCode,
                    subject_name = schedule.Description + " " + schedule.SubType,
                    time_info = schedule.TimeStart + " - " + schedule.TimeEnd + " " + schedule.Mdn + " " + schedule.Days,
                    units = schedule.Units.ToString(),
                    official_enrolled = OfficialEnrolled,
                    pending_enrolled = PendingEnrolled,
                    subject_size = schedule.Size,
                    department = department,
                    official_enrolled_size = OfficialEnrolled.Count(),
                    pending_enrolled_size = PendingEnrolled.Count(),
                    not_accepted_section = schedule.Size - (OfficialEnrolled.Count() + PendingEnrolled.Count()),
                    not_accepted = not_accepted
                };
            }
            else
            {
                response = new ViewClasslistResponse
                {
                    edp_code = scheduleBe.EdpCode,
                    subject_name = scheduleBe.Description + " " + scheduleBe.SubType,
                    time_info = scheduleBe.TimeStart + " - " + scheduleBe.TimeEnd + " " + scheduleBe.Mdn + " " + scheduleBe.Days,
                    units = scheduleBe.Units.ToString(),
                    official_enrolled = OfficialEnrolled,
                    pending_enrolled = PendingEnrolled,
                    subject_size = scheduleBe.Size,
                    department = department,
                    official_enrolled_size = OfficialEnrolled.Count(),
                    pending_enrolled_size = PendingEnrolled.Count(),
                    not_accepted_section = scheduleBe.Size - (OfficialEnrolled.Count() + PendingEnrolled.Count()),
                    not_accepted = not_accepted
                };
            }

            return response;
        }

        /*
        * Method to view classlist
        *   0 REGISTERED,
            1 APPROVED_REGISTRATION_REGISTRAR,
            2 DISAPPROVED_REGISTRATION_REGISTRAR,
            3 APPROVED_REGISTRATION_DEAN,
            4 DISAPPROVED_REGISTRATION_DEAN,
            5 SELECTING_SUBJECTS,
            6 APPROVED_BY_DEAN,
            7 DISAPPROVED_BY_DEAN,
            8 APPROVED_BY_ACCOUNTING,
            9 APPROVED_BY_CASHIER,
            10 OFFICIALLY_ENROLLED,
            11 WITHDRAWN_ENROLLMENT_BEFORE_START_OF_CLASS,
            12 WITHDRAWN_ENROLLMENT_START_OF_CLASS,
            13 CANCELLED
        */
        public GetEnrollmentStatusResponse GetEnrollmentStatus(GetEnrollmentStatusRequest getEnrollmentStatusRequest)
        {
            List<GetEnrollmentStatusResponse.courseStatus> records = new List<GetEnrollmentStatusResponse.courseStatus>();

            Dictionary<int, int> counts = new Dictionary<int, int>();

            var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.Department == getEnrollmentStatusRequest.department && x.CourseActive == 1 && x.ActiveTerm == getEnrollmentStatusRequest.active_term).ToList();

            var coursesList = courseList.Select(x => x.CourseCode);

            if (!getEnrollmentStatusRequest.dte.ToString().ToUpper().Equals("1/1/0001 12:00:00 AM"))
            {
                foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => coursesList.Contains(x.CourseCode) && DateTime.Parse(getEnrollmentStatusRequest.dte.ToShortDateString() + " 00:00:00") <= x.RegisteredOn && DateTime.Parse(getEnrollmentStatusRequest.dte.ToShortDateString() + " 23:59:59") >= x.RegisteredOn && x.ActiveTerm == getEnrollmentStatusRequest.active_term).GroupBy(x => x.Status)
                       .Select(group => new
                       {
                           Metric = group.Key,
                           Count = group.Count()
                       })
                       .OrderBy(x => x.Metric))
                {
                    counts.Add(line.Metric, line.Count);
                }
            }
            else
            {
                foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => coursesList.Contains(x.CourseCode) && x.ActiveTerm == getEnrollmentStatusRequest.active_term).GroupBy(x => x.Status)
                      .Select(group => new
                      {
                          Metric = group.Key,
                          Count = group.Count()
                      })
                      .OrderBy(x => x.Metric))
                {
                    counts.Add(line.Metric, line.Count);
                }
            }

            int countPendingPayment = 0;
            int countPendingCashier = 0;

            List<int> count = new List<int>();
            for (int counter = 0; counter < 14; counter++)
            {
                if (counts.ContainsKey(counter))
                {
                    if (counter == 8)
                    {
                        var studIdNumbers = _ucOnlinePortalContext.Attachments.Where(x => x.Type.Equals("Payment") && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Select(x => x.StudId).Distinct();
                        countPendingCashier = _ucOnlinePortalContext.Oenrps.Where(x => x.Status == 8 && coursesList.Contains(x.CourseCode) && studIdNumbers.Contains(x.StudId) && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Count();
                        countPendingPayment = _ucOnlinePortalContext.Oenrps.Where(x => x.Status == 8 && coursesList.Contains(x.CourseCode) && !studIdNumbers.Contains(x.StudId) && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Count();
                    }
                    count.Add(counts[counter]);
                }
                else
                {
                    count.Add(0);
                }
            }

            GetEnrollmentStatusResponse.courseStatus statusAll = new GetEnrollmentStatusResponse.courseStatus
            {
                courseName = "All",
                pending_registered = count[0] + count[1],
                subject_selection = count[3],
                pending_dean = count[5],
                pending_accounting = count[6],
                pending_payment = countPendingPayment,
                pending_cashier = countPendingCashier,
                pending_total = count[5] + count[6] + countPendingCashier,
                official_total = count[10]
            };

            records.Add(statusAll);

            foreach (CourseList cl in courseList)
            {
                int year_level = 0;

                if (getEnrollmentStatusRequest.department.Equals("CL"))
                {
                    year_level = 1;
                }
                else if (getEnrollmentStatusRequest.department.Equals("SH"))
                {
                    year_level = 11;
                }
                else
                {
                    if (cl.CourseCode.Equals("N1"))
                    {
                        year_level = 1;
                    }
                    else if (cl.CourseCode.Equals("K1"))
                    {
                        year_level = 1;
                    }
                    else if (cl.CourseCode.Equals("E1"))
                    {
                        year_level = 1;
                    }
                    else if (cl.CourseCode.Equals("E2"))
                    {
                        year_level = 2;
                    }
                    else if (cl.CourseCode.Equals("E3"))
                    {
                        year_level = 3;
                    }
                    else if (cl.CourseCode.Equals("E4"))
                    {
                        year_level = 4;
                    }
                    else if (cl.CourseCode.Equals("E5"))
                    {
                        year_level = 5;
                    }
                    else if (cl.CourseCode.Equals("E6"))
                    {
                        year_level = 6;
                    }
                    else if (cl.CourseCode.Equals("E7"))
                    {
                        year_level = 7;
                    }
                    else if (cl.CourseCode.Equals("E8"))
                    {
                        year_level = 8;
                    }
                    else if (cl.CourseCode.Equals("E9"))
                    {
                        year_level = 9;
                    }
                    else if (cl.CourseCode.Equals("E10"))
                    {
                        year_level = 10;
                    }
                }

                for (int year = year_level; year < year_level + cl.CourseYearLimit; year++)
                {
                    counts = new Dictionary<int, int>();
                    if (!getEnrollmentStatusRequest.dte.ToString().ToUpper().Equals("1/1/0001 12:00:00 AM"))
                    {
                        foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => x.CourseCode == cl.CourseCode && DateTime.Parse(getEnrollmentStatusRequest.dte.ToShortDateString() + " 00:00:00") <= x.RegisteredOn && DateTime.Parse(getEnrollmentStatusRequest.dte.ToShortDateString() + " 23:59:59") >= x.RegisteredOn && x.YearLevel == year && x.ActiveTerm == getEnrollmentStatusRequest.active_term).GroupBy(x => x.Status)
                              .Select(group => new
                              {
                                  Metric = group.Key,
                                  Count = group.Count()
                              })
                              .OrderBy(x => x.Metric))
                        {
                            counts.Add(line.Metric, line.Count);
                        }
                    }
                    else
                    {
                        foreach (var line in _ucOnlinePortalContext.Oenrps.Where(x => x.CourseCode == cl.CourseCode && x.YearLevel == year && x.ActiveTerm == getEnrollmentStatusRequest.active_term).GroupBy(x => x.Status)
                              .Select(group => new
                              {
                                  Metric = group.Key,
                                  Count = group.Count()
                              })
                              .OrderBy(x => x.Metric))
                        {
                            counts.Add(line.Metric, line.Count);
                        }
                    }

                    countPendingPayment = 0;
                    countPendingCashier = 0;

                    count = new List<int>();
                    for (int counter = 0; counter < 14; counter++)
                    {
                        if (counts.ContainsKey(counter))
                        {
                            if (counter == 8)
                            {
                                var studIdNumbers = _ucOnlinePortalContext.Attachments.Where(x => x.Type.Equals("Payment") && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Select(x => x.StudId).Distinct();
                                countPendingCashier = _ucOnlinePortalContext.Oenrps.Where(x => x.Status == 8 && cl.CourseCode == (x.CourseCode) && studIdNumbers.Contains(x.StudId) && x.YearLevel == year && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Count();
                                countPendingPayment = _ucOnlinePortalContext.Oenrps.Where(x => x.Status == 8 && cl.CourseCode == (x.CourseCode) && !studIdNumbers.Contains(x.StudId) && x.YearLevel == year && x.ActiveTerm == getEnrollmentStatusRequest.active_term).Count();
                            }
                            count.Add(counts[counter]);
                        }
                        else
                        {
                            count.Add(0);
                        }
                    }

                    statusAll = new GetEnrollmentStatusResponse.courseStatus
                    {
                        courseName = cl.CourseDepartment + " - " + cl.CourseAbbr,
                        pending_registered = count[0] + count[1],
                        subject_selection = count[3],
                        pending_dean = count[5],
                        pending_accounting = count[6],
                        pending_payment = countPendingPayment,
                        pending_cashier = countPendingCashier,
                        pending_total = count[5] + count[6] + countPendingCashier,
                        official_total = count[10],
                        year_level = year
                    };

                    records.Add(statusAll);
                }

            }
            return new GetEnrollmentStatusResponse { courseStat = records };
        }

        /*
       * Method to view classlist
       */
        public UpdateStudentInfoResponse UpdateStudentInfo(UpdateStudentInfoRequest updateStudentInfoRequest)
        {
            var studLoginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == updateStudentInfoRequest.id_number).FirstOrDefault();

            studLoginInfo.CourseCode = updateStudentInfoRequest.course_code;
            studLoginInfo.Dept = updateStudentInfoRequest.dept;
            studLoginInfo.Year = (short)updateStudentInfoRequest.year;
            studLoginInfo.MobileNumber = updateStudentInfoRequest.mobile;
            studLoginInfo.Facebook = updateStudentInfoRequest.facebook;


            //Add OENRP
            Oenrp newStudentOenrp = new Oenrp
            {
                StudId = updateStudentInfoRequest.id_number,
                YearLevel = (short)updateStudentInfoRequest.year,
                CourseCode = updateStudentInfoRequest.course_code,
                RegisteredOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Units = 0,
                Classification = updateStudentInfoRequest.classification,
                Dept = updateStudentInfoRequest.dept,
                Status = updateStudentInfoRequest.classification == "S" || updateStudentInfoRequest.classification == "T" ? (short)EnrollmentStatus.SUBJECT_EVALUATION_BY_DEAN : (short)EnrollmentStatus.REGISTERED,
                AdjustmentCount = 1,
                RequestDeblock = 0,
                RequestOverload = 0,
                NeededPayment = 0,
                Mdn = updateStudentInfoRequest.mdn,
                PromiPay = 0,
                RequestPromissory = 0,
                ActiveTerm = updateStudentInfoRequest.active_term
            };

            _ucOnlinePortalContext.LoginInfos.Update(studLoginInfo);
            _ucOnlinePortalContext.Oenrps.Add(newStudentOenrp);
            _ucOnlinePortalContext.SaveChanges();

            Notification newNotification = new Notification
            {
                StudId = updateStudentInfoRequest.id_number,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = Literals.REGISTERED,
                NotifRead = 0,
                ActiveTerm = updateStudentInfoRequest.active_term
            };

            _ucOnlinePortalContext.Notifications.Add(newNotification);
            _ucOnlinePortalContext.SaveChanges();

            var studentOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == updateStudentInfoRequest.id_number && x.ActiveTerm == updateStudentInfoRequest.active_term).FirstOrDefault();

            studentOenrp.ApprovedRegRegistrar = "AUTO-APPROVE";
            studentOenrp.ApprovedRegRegistrarOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            studentOenrp.Status = updateStudentInfoRequest.classification == "S" || updateStudentInfoRequest.classification == "T" ? (short)EnrollmentStatus.SUBJECT_EVALUATION_BY_DEAN : (short)EnrollmentStatus.APPROVED_REGISTRATION_REGISTRAR;

            newNotification = new Notification
            {
                StudId = updateStudentInfoRequest.id_number,
                NotifRead = 0,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = Literals.APPROVED_REGISTRATION_REGISTRAR,
                ActiveTerm = updateStudentInfoRequest.active_term
            };

            _ucOnlinePortalContext.Notifications.Add(newNotification);
            _ucOnlinePortalContext.SaveChanges();

            return new UpdateStudentInfoResponse { success = 1 };
        }

        /*
       * Method to view classlist
       */
        public ViewStudentEvaluationResponse ViewEvaluation(ViewStudentEvaluationRequest viewStudentEvaluationRequest)
        {
            var result = (from _gradesEval in _ucOnlinePortalContext.GradeEvaluations
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on _gradesEval.IntCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          where _gradesEval.StudId == viewStudentEvaluationRequest.id_number
                          select new ViewStudentEvaluationResponse.Grades
                          {
                              subject_name = _subject_info.SubjectName,
                              subject_type = _subject_info.SubjectType,
                              descriptive = _subject_info.Descr1.Trim() + " " + _subject_info.Descr2.Trim(),
                              midterm_grade = _gradesEval.MidtermGrade,
                              final_grade = _gradesEval.FinalGrade,
                              units = _subject_info.Units,
                              term = _gradesEval.Term,
                              int_code = _subject_info.InternalCode
                          }).ToList();


            List<ViewStudentEvaluationResponse.Grades> listGrades = new List<ViewStudentEvaluationResponse.Grades>();
            List<String> subNames = new List<string>();

            var studentInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == viewStudentEvaluationRequest.id_number).FirstOrDefault();

            foreach (ViewStudentEvaluationResponse.Grades grd in result)
            {
                if (grd.term.Equals("20211") && !studentInfo.CourseCode.Contains("BN"))
                {
                    if (subNames.Contains(grd.int_code))
                    {
                        var findGrade = listGrades.Where(x => x.int_code == grd.int_code).FirstOrDefault();
                        if (grd.midterm_grade != null && !grd.midterm_grade.Equals(String.Empty))
                        {
                            findGrade.midterm_grade = grd.midterm_grade;

                        }
                        if (grd.final_grade != null && !grd.final_grade.Equals(String.Empty))
                        {
                            findGrade.final_grade = grd.final_grade;
                        }
                        continue;
                    }
                    else
                    {
                        subNames.Add(grd.int_code);
                        listGrades.Add(grd);
                    }
                }
                else
                {
                    listGrades.Add(grd);
                }
            }

            return new ViewStudentEvaluationResponse { studentGrades = listGrades.OrderByDescending(x => x.term).ThenBy(x => x.subject_name).ToList() };
        }


        /*
       * Method to view classlist
       */
        public ViewStudentEvaluationBEResponse ViewEvaluationBE(ViewStudentEvaluationBERequest viewStudentEvaluationBERequest)
        {
            var result = (from _gradesEval in _ucOnlinePortalContext.GradeEvaluationBes
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on _gradesEval.IntCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          where _gradesEval.StudId == viewStudentEvaluationBERequest.id_number
                          select new ViewStudentEvaluationBEResponse.Grades
                          {
                              subject_name = _subject_info.SubjectName,
                              subject_type = _subject_info.SubjectType,
                              descriptive = _subject_info.Descr1.Trim() + " " + _subject_info.Descr2.Trim(),
                              grade_1 = _gradesEval.Grade1,
                              grade_2 = _gradesEval.Grade2,
                              grade_3 = _gradesEval.Grade3,
                              grade_4 = _gradesEval.Grade4,
                              units = _subject_info.Units,
                              term = _gradesEval.Term,
                              int_code = _subject_info.InternalCode
                          }).ToList();


            var studentInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == viewStudentEvaluationBERequest.id_number).FirstOrDefault();

            return new ViewStudentEvaluationBEResponse { studentGrades = result.OrderByDescending(x => x.term).ThenBy(x => x.subject_name).ToList() };
        }


        /*
      * Method to view classlist
      */
        public ViewOldStudentInfoResponse ViewOldStudentInfo(ViewOldStudentInfoRequest viewOldStudentInfoRequest)
        {
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == viewOldStudentInfoRequest.id_number && x.ActiveTerm == viewOldStudentInfoRequest.active_term).FirstOrDefault();
            var studLogin = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == viewOldStudentInfoRequest.id_number).FirstOrDefault();

            var attachmentList = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == viewOldStudentInfoRequest.id_number && x.Type != "Payment" && x.ActiveTerm == viewOldStudentInfoRequest.active_term).ToList();

            if (viewOldStudentInfoRequest.payment != null && viewOldStudentInfoRequest.payment == 1)
            {
                attachmentList = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == viewOldStudentInfoRequest.id_number && x.Type.Equals("Payment") && x.ActiveTerm == viewOldStudentInfoRequest.active_term).Take(1).ToList();
            }

            List<ViewOldStudentInfoResponse.attachment> attach = new List<ViewOldStudentInfoResponse.attachment>();
            attach = attachmentList.Select(x => new ViewOldStudentInfoResponse.attachment
            {
                attachment_id = x.AttachmentId,
                email = x.Email,
                filename = x.Filename,
                id_number = x.StudId,
                type = x.Type
            }).ToList();

            ViewOldStudentInfoResponse oldStudInfo = new ViewOldStudentInfoResponse();

            var course = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == studLogin.CourseCode).FirstOrDefault();

            if (studLogin != null)
            {
                oldStudInfo.stud_id = studLogin.StudId;
                oldStudInfo.last_name = studLogin.LastName;
                oldStudInfo.first_name = studLogin.FirstName;
                oldStudInfo.middle_name = studLogin.Mi;
                oldStudInfo.suffix = studLogin.Suffix;
                oldStudInfo.start_term = studLogin.StartTerm == null ? 0 : int.Parse(studLogin.StartTerm);
                oldStudInfo.dept = studLogin.Dept;
                oldStudInfo.course_code = studLogin.CourseCode;
                oldStudInfo.course = course.CourseAbbr;
                oldStudInfo.college = course.CourseDepartment;
                oldStudInfo.year_level = (short)studLogin.Year;
                oldStudInfo.mobile = studLogin.MobileNumber;
                oldStudInfo.email = studLogin.Email;
                oldStudInfo.facebook = studLogin.Facebook;
                oldStudInfo.allowed_units = studLogin.AllowedUnits == null ? 0 : (short)studLogin.AllowedUnits;
                oldStudInfo.gender = studLogin.Sex;
                oldStudInfo.birthdate = studLogin.Birthdate.ToString();
                oldStudInfo.is_verified = (short)studLogin.IsVerified;
            }

            if (studOenrp != null)
            {
                oldStudInfo.section = studOenrp.Section;
                oldStudInfo.mdn = studOenrp.Mdn;
                oldStudInfo.classification = studOenrp.Classification;
                oldStudInfo.assigned_section = studOenrp.Section;
                oldStudInfo.request_deblock = (short)studOenrp.RequestDeblock;
                oldStudInfo.request_overload = (short)studOenrp.RequestOverload;
            }

            oldStudInfo.attachments = attach;

            return oldStudInfo;
        }


        /*
         * Method to view classlist
         */
        public SelectSubjectInfoResponse SelectSubject(SelectSubjectInfoRequest selectSubjectInfoRequest)
        {
            var subject = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == selectSubjectInfoRequest.edp_code && x.ActiveTerm == selectSubjectInfoRequest.active_term).FirstOrDefault();

            if (subject == null)
            {
                return null;
            }
            else
            {
                SelectSubjectInfoResponse subjectResponse = new SelectSubjectInfoResponse
                {
                    ScheduleId = subject.ScheduleId,
                    EdpCode = subject.EdpCode,
                    Description = subject.Description,
                    InternalCode = subject.InternalCode,
                    SubType = subject.SubType,
                    Units = subject.Units,
                    TimeStart = subject.TimeStart,
                    TimeEnd = subject.TimeEnd,
                    Mdn = subject.Mdn,
                    Days = subject.Days,
                    MTimeStart = subject.MTimeStart,
                    MTimeEnd = subject.MTimeEnd,
                    MDays = subject.MDays,
                    MaxSize = subject.MaxSize,
                    Section = subject.Section,
                    Room = subject.Room,
                    SplitType = subject.SplitType,
                    SplitCode = subject.SplitCode,
                    IsGened = subject.IsGened,
                    status = subject.Status

                };

                return subjectResponse;
            }
        }

        /*
        * Method to view classlist
        */
        public UpdateSubjectResponse UpdateSubject(UpdateSubjectRequest updateSubjectRequest)
        {
            var subject = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == updateSubjectRequest.EdpCode && x.ActiveTerm == updateSubjectRequest.active_term).FirstOrDefault();

            subject.Description = updateSubjectRequest.Description;
            subject.SubType = updateSubjectRequest.SubType;
            subject.Units = updateSubjectRequest.Units;
            subject.TimeStart = updateSubjectRequest.TimeStart;
            subject.TimeEnd = updateSubjectRequest.TimeEnd;
            subject.Mdn = updateSubjectRequest.Mdn;
            subject.Days = updateSubjectRequest.Days;
            subject.MTimeStart = updateSubjectRequest.MTimeStart;
            subject.MTimeEnd = updateSubjectRequest.MTimeEnd;
            subject.MDays = updateSubjectRequest.MDays;
            subject.MaxSize = updateSubjectRequest.MaxSize;
            subject.Section = updateSubjectRequest.Section;
            subject.Room = updateSubjectRequest.Room;
            subject.SplitType = updateSubjectRequest.SplitType;
            subject.SplitCode = updateSubjectRequest.SplitCode;
            subject.IsGened = updateSubjectRequest.IsGened;
            subject.Status = (short)updateSubjectRequest.status;

            _ucOnlinePortalContext.Schedules.Update(subject);
            _ucOnlinePortalContext.SaveChanges();

            return new UpdateSubjectResponse { success = 1 };
        }

        /*
        * Method to view classlist
        */
        public RemoveDuplicateOtspResponse RemoveDuplicateOstsp(RemoveDuplicateOstspRequest removeDuplicateOstspRequest)
        {
            String sqlToRemove = "WITH cte AS ( SELECT stud_id, edp_code, status, active_term, row_number() OVER(PARTITION BY stud_id, edp_code, status, active_term ORDER BY stud_id) AS[rn] FROM[UCOnlinePortal].[dbo].[Ostsp]) DELETE cte WHERE[rn] > 1 and active_term = '" + removeDuplicateOstspRequest.active_term + "'";
            _ucOnlinePortalContext.Database.ExecuteSqlRaw(sqlToRemove);

            var distinctOstsp = _ucOnlinePortalContext.Ostsps.Where(x => x.ActiveTerm == removeDuplicateOstspRequest.active_term).Select(x => x.EdpCode).Distinct().ToList();

            foreach (string edpcode in distinctOstsp)
            {
                int pending = 0;
                int official = 0;

                var ostspDetail = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == edpcode && x.ActiveTerm == removeDuplicateOstspRequest.active_term).ToList();

                foreach (Ostsp ost in ostspDetail)
                {
                    var studOstsp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == ost.StudId && x.ActiveTerm == removeDuplicateOstspRequest.active_term).FirstOrDefault();

                    if (studOstsp != null)
                    {
                        if (studOstsp.Section != null && !studOstsp.Section.Equals(String.Empty))
                        {
                            if (ost.Status == 3)
                                official++;
                            else if (ost.Status == 1 || ost.Status == 0)
                                pending++;
                        }
                        else
                        {
                            if (ost.Status == 3)
                                official++;
                            else if (ost.Status == 1)
                                pending++;
                        }
                    }
                }

                var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edpcode && x.ActiveTerm == removeDuplicateOstspRequest.active_term).FirstOrDefault();
                var scheduleBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == edpcode && x.ActiveTerm == removeDuplicateOstspRequest.active_term).FirstOrDefault();

                if (schedule != null)
                {
                    schedule.Size = official + pending;
                    schedule.PendingEnrolled = pending;
                    schedule.OfficialEnrolled = official;

                    _ucOnlinePortalContext.Schedules.Update(schedule);
                }
                else if (scheduleBe != null)
                {
                    scheduleBe.Size = official + pending;
                    scheduleBe.PendingEnrolled = pending;
                    scheduleBe.OfficialEnrolled = official;

                    _ucOnlinePortalContext.SchedulesBes.Update(scheduleBe);
                }
                _ucOnlinePortalContext.SaveChanges();
            }

            return new RemoveDuplicateOtspResponse { success = 1 };
        }

        /*
       * Method to view classlist
       */
        public ViewClasslistPerSectionResponse ViewClasslistPerSection(ViewClasslistPerSectionRequest viewClasslistPerSectionRequest)
        {
            var courseDesc = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == viewClasslistPerSectionRequest.course_code && x.ActiveTerm == viewClasslistPerSectionRequest.active_term).FirstOrDefault();

            var result = (from _oenrp in _ucOnlinePortalContext.Oenrps
                          join _login in _ucOnlinePortalContext.LoginInfos
                          on _oenrp.StudId equals _login.StudId
                          join _courselist in _ucOnlinePortalContext.CourseLists
                          on _login.CourseCode equals _courselist.CourseCode
                          where _oenrp.Section == viewClasslistPerSectionRequest.section
                          && _oenrp.CourseCode == viewClasslistPerSectionRequest.course_code
                          && _oenrp.ActiveTerm == viewClasslistPerSectionRequest.active_term
                          && _courselist.ActiveTerm == viewClasslistPerSectionRequest.active_term
                          select new ViewClasslistPerSectionResponse.Enrolled
                          {
                              id_number = _login.StudId,
                              last_name = _login.LastName,
                              firstname = _login.FirstName,
                              course_year = _courselist.CourseAbbr + " " + _login.Year,
                              mobile_number = _login.MobileNumber,
                              email = _login.Email,
                              status = _oenrp.Status,
                              gender = _login.Sex
                          });

            if (courseDesc.Department.Equals("SH"))
            {
                result = result.OrderBy(x => x.gender).ThenBy(x => x.last_name);
            }
            else
            {
                result = result.OrderBy(x => x.last_name);
            }

            ViewClasslistPerSectionResponse response = new ViewClasslistPerSectionResponse
            {
                course = courseDesc.CourseDescription,
                section = viewClasslistPerSectionRequest.section,
                section_size = result.Count().ToString(),
                assigned_section = result.ToList()
            };

            return response;
        }

        /*
       * Method to view classlist
       */
        public UpdateStudentStatusResponse UpdateStudentStatus(UpdateStudentStatusRequest updateStudentStatusRequest)
        {
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == updateStudentStatusRequest.id_number && x.ActiveTerm == updateStudentStatusRequest.active_term).FirstOrDefault();

            if (studOenrp != null)
            {
                if (studOenrp.Status < updateStudentStatusRequest.new_status)
                {
                    return new UpdateStudentStatusResponse { success = 0 };
                }
                else
                {
                    studOenrp.Status = (short)updateStudentStatusRequest.new_status;

                    if (updateStudentStatusRequest.new_status == 0)
                    {
                        studOenrp.ApprovedRegDean = null;
                        studOenrp.ApprovedRegDeanOn = null;
                        studOenrp.DisapprovedRegDean = null;
                        studOenrp.DisapprovedRegDeanOn = null;
                        studOenrp.ApprovedRegRegistrar = null;
                        studOenrp.ApprovedRegRegistrarOn = null;
                        studOenrp.DisapprovedRegRegistrar = null;
                        studOenrp.DisapprovedRegRegistrarOn = null;
                        studOenrp.ApprovedDean = null;
                        studOenrp.ApprovedDeanOn = null;
                        studOenrp.DisapprovedDean = null;
                        studOenrp.DisapprovedDeanOn = null;
                        studOenrp.ApprovedAcctg = null;
                        studOenrp.ApprovedAcctgOn = null;
                        studOenrp.NeededPayment = 0;
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                        studOenrp.EnrollmentDate = null;
                        studOenrp.SubmittedOn = null;
                    }
                    else if (updateStudentStatusRequest.new_status == 1)
                    {
                        studOenrp.ApprovedRegDean = null;
                        studOenrp.ApprovedRegDeanOn = null;
                        studOenrp.DisapprovedRegDean = null;
                        studOenrp.DisapprovedRegDeanOn = null;
                        studOenrp.ApprovedDean = null;
                        studOenrp.ApprovedDeanOn = null;
                        studOenrp.DisapprovedDean = null;
                        studOenrp.DisapprovedDeanOn = null;
                        studOenrp.ApprovedAcctg = null;
                        studOenrp.ApprovedAcctgOn = null;
                        studOenrp.NeededPayment = 0;
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                        studOenrp.EnrollmentDate = null;
                        studOenrp.SubmittedOn = null;
                    }
                    else if (updateStudentStatusRequest.new_status == 3)
                    {
                        studOenrp.ApprovedDean = null;
                        studOenrp.ApprovedDeanOn = null;
                        studOenrp.DisapprovedDean = null;
                        studOenrp.DisapprovedDeanOn = null;
                        studOenrp.ApprovedAcctg = null;
                        studOenrp.ApprovedAcctgOn = null;
                        studOenrp.NeededPayment = 0;
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                        studOenrp.EnrollmentDate = null;
                        studOenrp.SubmittedOn = null;
                    }
                    else if (updateStudentStatusRequest.new_status == 5)
                    {
                        studOenrp.ApprovedDean = null;
                        studOenrp.ApprovedDeanOn = null;
                        studOenrp.ApprovedAcctg = null;
                        studOenrp.ApprovedAcctgOn = null;
                        studOenrp.NeededPayment = 0;
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                    }
                    else if (updateStudentStatusRequest.new_status == 6)
                    {
                        studOenrp.ApprovedAcctg = null;
                        studOenrp.ApprovedAcctgOn = null;
                        studOenrp.NeededPayment = 0;
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                    }
                    else if (updateStudentStatusRequest.new_status == 8)
                    {
                        studOenrp.ApprovedCashier = null;
                        studOenrp.ApprovedCashierOn = null;
                    }

                    var hasOstsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == updateStudentStatusRequest.id_number && x.ActiveTerm == updateStudentStatusRequest.active_term).ToList();

                    if (hasOstsp != null && (updateStudentStatusRequest.new_status < 3))
                    {
                        foreach (Ostsp ostsp in hasOstsp)
                        {
                            var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == ostsp.EdpCode && x.ActiveTerm == updateStudentStatusRequest.active_term).FirstOrDefault();

                            if (ostsp.Status == 1)
                            {
                                schedule.Size = schedule.Size - 1;
                                schedule.PendingEnrolled = schedule.PendingEnrolled - 1;
                            }
                            else if (ostsp.Status == 3)
                            {
                                schedule.Size = schedule.Size - 1;
                                schedule.OfficialEnrolled = schedule.OfficialEnrolled - 1;
                            }

                            if (studOenrp.Section != null && !studOenrp.Section.Equals(String.Empty))
                            {
                                if (ostsp.Status == 0)
                                {
                                    schedule.Size = schedule.Size - 1;
                                    schedule.PendingEnrolled = schedule.PendingEnrolled - 1;
                                }
                            }

                            _ucOnlinePortalContext.Schedules.Update(schedule);
                            _ucOnlinePortalContext.SaveChanges();
                        }


                        studOenrp.Section = null;
                        _ucOnlinePortalContext.SaveChanges();

                        _ucOnlinePortalContext.Ostsps.RemoveRange(_ucOnlinePortalContext.Ostsps.Where(x => x.StudId == updateStudentStatusRequest.id_number && x.ActiveTerm == updateStudentStatusRequest.active_term));
                    }
                }

                _ucOnlinePortalContext.Oenrps.Update(studOenrp);
                _ucOnlinePortalContext.SaveChanges();
            }
            else
            {
                return new UpdateStudentStatusResponse { success = 0 };
            }

            return new UpdateStudentStatusResponse { success = 1 };
        }

        /*
       * Method to view classlist
       */
        public ManualEnrollmentResponse ManualEnrollment(ManualEnrollmentRequest manualEnrollmentRequest)
        {
            var studentOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == manualEnrollmentRequest.id_number && x.ActiveTerm == manualEnrollmentRequest.active_term);

            if (studentOenrp == null)
            {
                return new ManualEnrollmentResponse { success = 0 };
            }
            else
            {
                string edp_code = manualEnrollmentRequest.edp_codes;
                string[] split_edp = edp_code.Split(',');

                if (split_edp.Length > 1)
                {

                    foreach (string os in split_edp)
                    {
                        Ostsp ostpN = new Ostsp
                        {
                            StudId = manualEnrollmentRequest.id_number,
                            EdpCode = os,
                            Status = 0,
                            Remarks = null,
                            AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            ActiveTerm = manualEnrollmentRequest.active_term
                        };

                        _ucOnlinePortalContext.Ostsps.Add(ostpN);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
                else
                {
                    Ostsp ostpN = new Ostsp
                    {
                        StudId = manualEnrollmentRequest.id_number,
                        EdpCode = edp_code,
                        Status = 0,
                        Remarks = null,
                        AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        ActiveTerm = manualEnrollmentRequest.active_term
                    };

                    _ucOnlinePortalContext.Ostsps.Add(ostpN);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }
            return new ManualEnrollmentResponse { success = 1 };
        }

        public RequestPromissoryResponse RequestPromissory(RequestPromissoryRequest requestPromissoryRequest)
        {
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == requestPromissoryRequest.stud_id && x.ActiveTerm == requestPromissoryRequest.active_term).FirstOrDefault();

            if (studOenrp == null)
            {
                return new RequestPromissoryResponse { success = 0 };
            }
            else
            {
                double payPercent = studOenrp.NeededPayment.Value * .30;

                if (requestPromissoryRequest.promise_pay >= payPercent)
                {
                    studOenrp.RequestPromissory = 1;
                }
                else
                {
                    studOenrp.RequestPromissory = 2;
                }

                studOenrp.PromiPay = requestPromissoryRequest.promise_pay;

                Promissory newProm = new Promissory
                {
                    StudId = requestPromissoryRequest.stud_id,
                    PromiMessage = requestPromissoryRequest.message,
                    PromiDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    ActiveTerm = requestPromissoryRequest.active_term
                };

                _ucOnlinePortalContext.Update(studOenrp);
                _ucOnlinePortalContext.Promissories.Add(newProm);
                _ucOnlinePortalContext.SaveChanges();
            }

            return new RequestPromissoryResponse { success = 1 }; ;
        }

        public GetPromissoryListResponse GetPromissoryList(GetPromissoryListRequest getPromissoryListRequest)
        {
            int take = (int)getPromissoryListRequest.limit;
            int skip = (int)getPromissoryListRequest.limit * ((int)getPromissoryListRequest.page - 1);

            //always get status 5 -> for dean
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          join _promi in _ucOnlinePortalContext.Promissories
                          on Oenrp.StudId equals _promi.StudId
                          where (Oenrp.Status == 8 || Oenrp.Status == 10) && Oenrp.RequestPromissory == getPromissoryListRequest.status
                          && Oenrp.ActiveTerm == getPromissoryListRequest.active_term && _courseList.ActiveTerm == getPromissoryListRequest.active_term
                          && _promi.ActiveTerm == getPromissoryListRequest.active_term
                          select new GetPromissoryListResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              status = (short)Oenrp.RequestPromissory,
                              date = _promi.PromiDate,
                              message = _promi.PromiMessage,
                              promise_pay = Oenrp.PromiPay,
                              needed_payment = Oenrp.NeededPayment.Value
                          });

            if (!getPromissoryListRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getPromissoryListRequest.course_department && x.ActiveTerm == getPromissoryListRequest.active_term).ToList();
                var courses = courseList.Select(x => x.CourseCode).ToList();

                result = result.Where(x => courses.Contains(x.course_code));
            }

            var count = result.Count();

            if (getPromissoryListRequest.page != 0 && getPromissoryListRequest.limit != 0)
            {
                result = result.OrderBy(x => x.date).Skip(skip).Take(take);
            }

            return new GetPromissoryListResponse { students = result.ToList(), count = count };
        }

        public CorrectTotalUnitsResponse CorrectTotalUnits(CorrectTotalUnitsRequest correctTotalUnitsRequest)
        {
            int[] enrStat = { 6, 8, 10 };

            var studOenp = _ucOnlinePortalContext.Oenrps.Where(x => enrStat.Contains(x.Status) && x.ActiveTerm == correctTotalUnitsRequest.active_term && x.ActiveTerm == correctTotalUnitsRequest.active_term).ToList();

            foreach (Oenrp Oenrp in studOenp)
            {
                //always get status 5 -> for dean
                var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                              join Schedule in _ucOnlinePortalContext.Schedules
                              on Ostsp.EdpCode equals Schedule.EdpCode
                              where Ostsp.StudId == Oenrp.StudId &&
                              (Ostsp.Status == 1 || Ostsp.Status == 3)
                              && Ostsp.ActiveTerm == correctTotalUnitsRequest.active_term
                              && Schedule.ActiveTerm == correctTotalUnitsRequest.active_term
                              select new {
                                  units = Schedule.Units
                              }).ToList();

                decimal totalUnits = result.Select(x => Convert.ToDecimal(x.units)).Sum();

                var studO = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == Oenrp.StudId).FirstOrDefault();

                studO.Units = (short)totalUnits;
                _ucOnlinePortalContext.Update(studO);
                _ucOnlinePortalContext.SaveChanges();
            }

            return new CorrectTotalUnitsResponse { success = 1 };
        }

        public AddNotificationResponse AddNotification(AddNotificationRequest addNotificationRequest)
        {
            Notification newNotification = new Notification
            {
                StudId = addNotificationRequest.stud_id,
                NotifRead = 0,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = addNotificationRequest.from_sender + ":" + addNotificationRequest.message,
                ActiveTerm = addNotificationRequest.active_term
            };

            _ucOnlinePortalContext.Notifications.Add(newNotification);
            _ucOnlinePortalContext.SaveChanges();

            return new AddNotificationResponse { success = 1 };
        }

        public UpdateInfoResponse GetInfoUpdate(UpdateInfoRequest updateInfoRequest)
        {
            var loginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == updateInfoRequest.stud_id).FirstOrDefault();

            if (loginInfo != null)
            {
                UpdateInfoResponse response = new UpdateInfoResponse
                {
                    first_name = loginInfo.FirstName,
                    last_name = loginInfo.LastName,
                    middle_initial = loginInfo.Mi,
                    year_level = (short)loginInfo.Year,
                    dept = loginInfo.Dept,
                    course_code = loginInfo.CourseCode,
                    allowed_units = loginInfo.AllowedUnits.HasValue ? loginInfo.AllowedUnits.Value : 0
                };

                var classification = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == updateInfoRequest.stud_id && x.ActiveTerm == updateInfoRequest.active_term).FirstOrDefault();

                if (classification != null)
                    response.classification = classification.Classification;

                return response;
            }
            else
            {
                return null;
            }
        }

        public UpdateInforResponse UpdateInfor(UpdateInforRequest updateInforRequest)
        {
            var loginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == updateInforRequest.stud_id).FirstOrDefault();

            if (loginInfo != null)
            {
                loginInfo.FirstName = updateInforRequest.first_name;
                loginInfo.LastName = updateInforRequest.last_name;
                loginInfo.Mi = updateInforRequest.middle_initial;
                loginInfo.Year = (short)updateInforRequest.year_level;
                loginInfo.Dept = updateInforRequest.dept;
                loginInfo.CourseCode = updateInforRequest.course_code;
                loginInfo.AllowedUnits = (short)updateInforRequest.allowed_units;

                _ucOnlinePortalContext.LoginInfos.Update(loginInfo);
                _ucOnlinePortalContext.SaveChanges();
            }

            var oenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == updateInforRequest.stud_id && x.ActiveTerm == updateInforRequest.active_term).FirstOrDefault();

            if (oenrp != null)
            {
                oenrp.Dept = updateInforRequest.dept;
                oenrp.CourseCode = updateInforRequest.course_code;
                oenrp.YearLevel = (short)updateInforRequest.year_level;
                oenrp.Classification = updateInforRequest.classification;


                _ucOnlinePortalContext.Update(oenrp);
                _ucOnlinePortalContext.SaveChanges();
            }

            return new UpdateInforResponse { success = 1 };
        }

        public SetClosedSubjectResponse SetClosed(SetClosedSubjectRequest setClosedSubjectRequest)
        {
            var scheduleClosed = _ucOnlinePortalContext.Schedules.Where(x => x.MaxSize == x.Size && x.ActiveTerm == setClosedSubjectRequest.active_term).ToList();

            scheduleClosed.ForEach(x => x.Status = 5);

            _ucOnlinePortalContext.SaveChanges();

            return new SetClosedSubjectResponse { success = 1 };
        }

        public GetTeachersListResponse GetTeachersList(GetTeachersListRequest getTeachersListRequest)
        {
            var TeachersList = _ucOnlinePortalContext.LoginInfos.Where(x => x.UserType.Contains("FACULTY"));

            if (!getTeachersListRequest.id_number.Equals(String.Empty))
            {
                TeachersList = TeachersList.Where(x => x.StudId == getTeachersListRequest.id_number);
            }

            if (!getTeachersListRequest.id_number.Equals(String.Empty))
            {
                TeachersList = TeachersList.Where(x => (x.FirstName + x.LastName).Contains(getTeachersListRequest.name));
            }

            List<GetTeachersListResponse.Teachers> teachers = new List<GetTeachersListResponse.Teachers>();
            teachers = TeachersList.Select(x => new GetTeachersListResponse.Teachers
            {
                id_number = x.StudId,
                first_name = x.FirstName,
                last_name = x.LastName
            }).ToList();

            return new GetTeachersListResponse { teacherList = teachers };
        }

        public SaveTeachersLoadResponse SaveTeachersLoad(SaveTeachersLoadRequest saveTeachersLoadRequest)
        {
            var ostspSelected = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor == saveTeachersLoadRequest.id_number && x.ActiveTerm == saveTeachersLoadRequest.active_term).Select(x => x.EdpCode).ToList();

            var toDelete = ostspSelected.Except(saveTeachersLoadRequest.edp_codes).ToList();
            var toAdd = saveTeachersLoadRequest.edp_codes.Except(ostspSelected).ToList();

            if (toDelete.Count > 0)
            {
                foreach (string edp_code in toDelete)
                {
                    var removeInstructor = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edp_code && x.ActiveTerm == saveTeachersLoadRequest.active_term).FirstOrDefault();

                    removeInstructor.Instructor = "";

                    _ucOnlinePortalContext.Schedules.Update(removeInstructor);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            if (toAdd.Count > 0)
            {
                foreach (string edp_code in toAdd)
                {
                    var removeInstructor = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edp_code && x.ActiveTerm == saveTeachersLoadRequest.active_term).FirstOrDefault();

                    removeInstructor.Instructor = saveTeachersLoadRequest.id_number;

                    _ucOnlinePortalContext.Schedules.Update(removeInstructor);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            if (saveTeachersLoadRequest.edp_codes.Length == 0)
            {
                var schedulesToRemove = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor == saveTeachersLoadRequest.id_number && x.ActiveTerm == saveTeachersLoadRequest.active_term).Select(x => x.EdpCode).ToList();

                foreach (string edp_code in schedulesToRemove)
                {
                    var removeInstructor = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edp_code && x.ActiveTerm == saveTeachersLoadRequest.active_term).FirstOrDefault();

                    removeInstructor.Instructor = "";

                    _ucOnlinePortalContext.Schedules.Update(removeInstructor);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            return new SaveTeachersLoadResponse { success = 1 };
        }

        public GetTeachersLoadResponse GetTeachersLoad(GetTeachersLoadRequest getTeachersLoadRequest)
        {
            List<GetTeachersLoadResponse.Schedules> result = null;
            //Get user data
            var teachersLoad = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor == getTeachersLoadRequest.id_number && x.ActiveTerm == getTeachersLoadRequest.active_term);
            var teachersLoadBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.Instructor == getTeachersLoadRequest.id_number && x.ActiveTerm == getTeachersLoadRequest.active_term);

            //check if the the data exist
            if (teachersLoad == null && teachersLoadBe == null)
            {
                //return empty data
                return null;
            }
            else
            {
                //Get data from Ostsp and Schedules
                result = (from Schedules in _ucOnlinePortalContext.Schedules
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on Schedules.InternalCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          join _courselist in _ucOnlinePortalContext.CourseLists
                          on Schedules.CourseCode equals _courselist.CourseCode into course
                          from _courselist in course.DefaultIfEmpty()
                          where Schedules.Instructor == getTeachersLoadRequest.id_number
                          && _courselist.ActiveTerm == getTeachersLoadRequest.active_term && Schedules.ActiveTerm == getTeachersLoadRequest.active_term
                          select new GetTeachersLoadResponse.Schedules
                          {
                              edpcode = Schedules.EdpCode,
                              subject_name = Schedules.Description,
                              subject_type = Schedules.SubType,
                              days = Schedules.Days,
                              begin_time = Schedules.TimeStart,
                              end_time = Schedules.TimeEnd,
                              mdn = Schedules.Mdn,
                              m_begin_time = Schedules.MTimeStart,
                              m_end_time = Schedules.MTimeEnd,
                              m_days = Schedules.MDays,
                              size = Schedules.Size,
                              max_size = Schedules.MaxSize,
                              units = Schedules.Units,
                              room = Schedules.Room,
                              descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                              split_code = Schedules.SplitCode,
                              split_type = Schedules.SplitType,
                              section = Schedules.Section,
                              course_abbr = _courselist.CourseAbbr,
                              pending_enrolled = Schedules.PendingEnrolled.Value,
                              official_enrolled = Schedules.OfficialEnrolled.Value,
                              dept = _courselist.Department

                          }).ToList();

                //Get data from Ostsp and Schedules
                var result2 = (from Schedules in _ucOnlinePortalContext.SchedulesBes
                               join _subject_info in _ucOnlinePortalContext.SubjectInfos
                               on Schedules.InternalCode equals _subject_info.InternalCode into sched
                               from _subject_info in sched.DefaultIfEmpty()
                               join _courselist in _ucOnlinePortalContext.CourseLists
                               on Schedules.CourseCode equals _courselist.CourseCode into course
                               from _courselist in course.DefaultIfEmpty()
                               where Schedules.Instructor == getTeachersLoadRequest.id_number
                               && _courselist.ActiveTerm == getTeachersLoadRequest.active_term && Schedules.ActiveTerm == getTeachersLoadRequest.active_term
                               select new GetTeachersLoadResponse.Schedules
                               {
                                   edpcode = Schedules.EdpCode,
                                   subject_name = Schedules.Description,
                                   subject_type = Schedules.SubType,
                                   days = Schedules.Days,
                                   begin_time = Schedules.TimeStart,
                                   end_time = Schedules.TimeEnd,
                                   mdn = Schedules.Mdn,
                                   m_begin_time = Schedules.MTimeStart,
                                   m_end_time = Schedules.MTimeEnd,
                                   m_days = Schedules.MDays,
                                   size = Schedules.Size,
                                   max_size = Schedules.MaxSize,
                                   units = Schedules.Units,
                                   room = Schedules.Room,
                                   descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                                   split_code = Schedules.SplitCode,
                                   split_type = Schedules.SplitType,
                                   section = Schedules.Section,
                                   course_abbr = _courselist.CourseAbbr,
                                   pending_enrolled = Schedules.PendingEnrolled.Value,
                                   official_enrolled = Schedules.OfficialEnrolled.Value,
                                   dept = _courselist.Department
                               }).ToList();

                result.AddRange(result2);

                return new GetTeachersLoadResponse { schedules = result };
            }
        }

        public SaveAdjustmentResponse SaveAdjustment(SaveAdjustmentRequest saveAdjustmentRequest)
        {
            var oenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == saveAdjustmentRequest.id_number && x.ActiveTerm == saveAdjustmentRequest.active_term).FirstOrDefault();

            if (oenrp != null)
            {
                if (oenrp.AdjustmentCount == 0)
                {
                    return new SaveAdjustmentResponse { success = 0 };
                }
                else
                {
                    if (saveAdjustmentRequest.addEdpCodes.Length > 0)
                    {
                        foreach (string edp_code in saveAdjustmentRequest.addEdpCodes)
                        {

                            Ostsp ostpN = new Ostsp
                            {
                                StudId = saveAdjustmentRequest.id_number,
                                EdpCode = edp_code,
                                Status = 4,
                                Remarks = null,
                                AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                ActiveTerm = saveAdjustmentRequest.active_term
                            };

                            _ucOnlinePortalContext.Ostsps.Add(ostpN);
                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }

                    if (saveAdjustmentRequest.deleteEdpCodes.Length > 0)
                    {
                        foreach (string edp_code in saveAdjustmentRequest.deleteEdpCodes)
                        {
                            Ostsp ostpN = new Ostsp
                            {
                                StudId = saveAdjustmentRequest.id_number,
                                EdpCode = edp_code,
                                Status = 5,
                                Remarks = null,
                                AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                ActiveTerm = saveAdjustmentRequest.active_term
                            };

                            _ucOnlinePortalContext.Ostsps.Add(ostpN);
                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }

                    oenrp.AdjustmentCount = 0;
                    oenrp.AdjustmentOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    _ucOnlinePortalContext.Oenrps.Update(oenrp);
                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            Notification newNotif = new Notification
            {
                StudId = saveAdjustmentRequest.id_number,
                NotifRead = 0,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = Literals.REQUESTED_ADJUSTMENT,
                ActiveTerm = saveAdjustmentRequest.active_term
            };

            _ucOnlinePortalContext.Notifications.Add(newNotif);
            _ucOnlinePortalContext.SaveChanges();

            return new SaveAdjustmentResponse { success = 1 };
        }

        public GetAdjustmentListResponse GetAdjustmentlist(GetAdjustmentListRequest getAdjustmentListRequest)
        {
            int take = (int)getAdjustmentListRequest.limit;
            int skip = (int)getAdjustmentListRequest.limit * ((int)getAdjustmentListRequest.page - 1);

            //always get status 5 -> for dean
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          where Oenrp.AdjustmentCount == getAdjustmentListRequest.status && Oenrp.Status >= 6
                          && Oenrp.ActiveTerm == getAdjustmentListRequest.active_term && _courseList.ActiveTerm == getAdjustmentListRequest.active_term
                          select new GetAdjustmentListResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              status = (short)Oenrp.AdjustmentCount,
                              date = Oenrp.EnrollmentDate.Value
                          });

            if (!getAdjustmentListRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getAdjustmentListRequest.course_department && x.ActiveTerm == getAdjustmentListRequest.active_term).ToList();
                var courses = courseList.Select(x => x.CourseCode).ToList();

                result = result.Where(x => courses.Contains(x.course_code));
            }

            if (!getAdjustmentListRequest.course_code.Equals(String.Empty))
            {
                result = result.Where(x => x.course_code == getAdjustmentListRequest.course_code);
            }

            if (!getAdjustmentListRequest.year.Equals(String.Empty) && getAdjustmentListRequest.year > 0)
            {
                result = result.Where(x => x.course_year.Contains(getAdjustmentListRequest.year.ToString()));
            }

            if (!getAdjustmentListRequest.id_number.Equals(String.Empty))
            {
                result = result.Where(x => x.id_number == getAdjustmentListRequest.id_number);
            }

            if (!getAdjustmentListRequest.name.Equals(String.Empty))
            {
                result = result.Where(x => (x.firstname + x.lastname).Contains(getAdjustmentListRequest.name));
            }

            var count = result.Count();

            if (getAdjustmentListRequest.page != 0 && getAdjustmentListRequest.limit != 0)
            {
                result = result.OrderBy(x => x.date).Skip(skip).Take(take);
            }

            return new GetAdjustmentListResponse { students = result.ToList(), count = count };
        }

        public GetAdjustmentDetailResponse GetAdjustmentDetail(GetAdjustmentDetailRequest getAdjustmentDetailRequest)
        {
            var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                          join Schedules in _ucOnlinePortalContext.Schedules
                          on Ostsp.EdpCode equals Schedules.EdpCode
                          join _subject_info in _ucOnlinePortalContext.SubjectInfos
                          on Schedules.InternalCode equals _subject_info.InternalCode into sched
                          from _subject_info in sched.DefaultIfEmpty()
                          join _courselist in _ucOnlinePortalContext.CourseLists
                          on Schedules.CourseCode equals _courselist.CourseCode into course
                          from _courselist in course.DefaultIfEmpty()
                          where Ostsp.StudId == getAdjustmentDetailRequest.id_number
                          && (Ostsp.Status == 4 || Ostsp.Status == 5)
                          && Ostsp.ActiveTerm == getAdjustmentDetailRequest.active_term && Schedules.ActiveTerm == getAdjustmentDetailRequest.active_term
                          && _courselist.ActiveTerm == getAdjustmentDetailRequest.active_term
                          select new GetAdjustmentDetailResponse.Schedules
                          {
                              edp_code = Schedules.EdpCode,
                              subject_name = Schedules.Description,
                              subject_type = Schedules.SubType,
                              days = Schedules.Days,
                              begin_time = Schedules.TimeStart,
                              end_time = Schedules.TimeEnd,
                              mdn = Schedules.Mdn,
                              m_begin_time = Schedules.MTimeStart,
                              m_end_time = Schedules.MTimeEnd,
                              m_days = Schedules.MDays,
                              size = Schedules.Size,
                              max_size = Schedules.MaxSize,
                              units = Schedules.Units,
                              room = Schedules.Room,
                              descriptive_title = _subject_info.Descr1 + _subject_info.Descr2,
                              split_code = Schedules.SplitCode,
                              split_type = Schedules.SplitType,
                              section = Schedules.Section,
                              course_abbr = _courselist.CourseAbbr,
                              status = Ostsp.Status
                          });

            var addedOstsp = result.Where(x => x.status == 4).ToList();
            var deletedOstsp = result.Where(x => x.status == 5).ToList();

            return new GetAdjustmentDetailResponse { added = addedOstsp, removed = deletedOstsp };
        }

        public ApproveAdjustmentResponse ApproveAdjustment(ApproveAdjustmentRequest approveAdjustmentRequest)
        {
            var addOstsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 4 && x.ActiveTerm == approveAdjustmentRequest.active_term).Select(x => x.EdpCode).ToList();
            var removeOstsp = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 5 && x.ActiveTerm == approveAdjustmentRequest.active_term).Select(x => x.EdpCode).ToList();
            var studOenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.ActiveTerm == approveAdjustmentRequest.active_term).FirstOrDefault();

            if (approveAdjustmentRequest.approve == 1)
            {
                if (addOstsp.Count > 0)
                {
                    var full = _ucOnlinePortalContext.Schedules.Where(x => addOstsp.Contains(x.EdpCode) && x.Size >= x.MaxSize && x.ActiveTerm == approveAdjustmentRequest.active_term).Select(x => x.EdpCode).ToList();

                    if (full.Count > 0)
                    {
                        return new ApproveAdjustmentResponse { success = 0, edp_code = full };
                    }
                    else
                    {
                        foreach (String edpc in addOstsp)
                        {
                            var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edpc && x.ActiveTerm == approveAdjustmentRequest.active_term).FirstOrDefault();

                            if (studOenrp.Status == 10)
                            {
                                sched.Size = sched.Size + 1;
                                sched.OfficialEnrolled = sched.OfficialEnrolled + 1;

                                Ostsp ostpN = new Ostsp
                                {
                                    StudId = approveAdjustmentRequest.id_number,
                                    EdpCode = edpc,
                                    Status = 3,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = approveAdjustmentRequest.active_term
                                };

                                _ucOnlinePortalContext.Ostsps.Add(ostpN);
                            }
                            else
                            {
                                sched.Size = sched.Size + 1;
                                sched.PendingEnrolled = sched.PendingEnrolled + 1;

                                Ostsp ostpN = new Ostsp
                                {
                                    StudId = approveAdjustmentRequest.id_number,
                                    EdpCode = edpc,
                                    Status = 1,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = approveAdjustmentRequest.active_term
                                };

                                _ucOnlinePortalContext.Ostsps.Add(ostpN);
                            }

                            _ucOnlinePortalContext.Schedules.Update(sched);
                            _ucOnlinePortalContext.SaveChanges();

                        }

                        _ucOnlinePortalContext.Ostsps.RemoveRange(_ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 4 && x.ActiveTerm == approveAdjustmentRequest.active_term));
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }

                if (removeOstsp.Count > 0)
                {
                    foreach (String edpc in removeOstsp)
                    {
                        var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edpc && x.ActiveTerm == approveAdjustmentRequest.active_term).FirstOrDefault();

                        if (studOenrp.Status == 10)
                        {
                            sched.Size = sched.Size - 1;
                            sched.OfficialEnrolled = sched.OfficialEnrolled - 1;

                            var ostspR = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 3 && x.EdpCode == edpc && x.ActiveTerm == approveAdjustmentRequest.active_term);
                            ostspR.ToList().ForEach(x => x.Status = 2);
                        }
                        else
                        {
                            sched.Size = sched.Size - 1;
                            sched.PendingEnrolled = sched.PendingEnrolled - 1;

                            var ostspR = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 1 && x.EdpCode == edpc && x.ActiveTerm == approveAdjustmentRequest.active_term);
                            ostspR.ToList().ForEach(x => x.Status = 2);
                        }

                        _ucOnlinePortalContext.Schedules.Update(sched);
                        _ucOnlinePortalContext.SaveChanges();
                    }

                    _ucOnlinePortalContext.Ostsps.RemoveRange(_ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && x.Status == 5 && x.ActiveTerm == approveAdjustmentRequest.active_term));
                    _ucOnlinePortalContext.SaveChanges();
                }

                studOenrp.AdjustmentCount = 9;

                Notification newNotif = new Notification
                {
                    StudId = approveAdjustmentRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.APPROVE_ADJUSTMENT,
                    ActiveTerm = approveAdjustmentRequest.active_term
                };

                _ucOnlinePortalContext.Notifications.Add(newNotif);

                studOenrp.Section = "";
                _ucOnlinePortalContext.Oenrps.Update(studOenrp);
            }
            else
            {
                studOenrp.AdjustmentCount = 8;
                _ucOnlinePortalContext.Ostsps.RemoveRange(_ucOnlinePortalContext.Ostsps.Where(x => x.StudId == approveAdjustmentRequest.id_number && (x.Status == 4 || x.Status == 5) && x.ActiveTerm == approveAdjustmentRequest.active_term));

                Notification newNotif = new Notification
                {
                    StudId = approveAdjustmentRequest.id_number,
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = Literals.DISAPPROVE_ADJUSTMENT,
                    ActiveTerm = approveAdjustmentRequest.active_term
                };

                _ucOnlinePortalContext.Notifications.Add(newNotif);

            }

            _ucOnlinePortalContext.SaveChanges();

            return new ApproveAdjustmentResponse { success = 1, edp_code = null }; ;
        }

        public ActivateAdjustmentResponse ActivateAdjustment(ActivateAdjustmentRequest activateAdjustmentRequest)
        {
            var oenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == activateAdjustmentRequest.id_number && x.ActiveTerm == activateAdjustmentRequest.active_term).FirstOrDefault();

            oenrp.AdjustmentCount = 1;

            _ucOnlinePortalContext.Oenrps.Update(oenrp);
            _ucOnlinePortalContext.SaveChanges();

            return new ActivateAdjustmentResponse { success = 1 };
        }

        public GetCoursesOpenResponse GetCoursesOpen(GetCoursesOpenRequest getCoursesOpenRequest)
        {
            var colleges = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseActive == 1 && x.EnrollmentOpen == 1 && x.ActiveTerm == getCoursesOpenRequest.active_term).ToList();

            if (!getCoursesOpenRequest.department.Equals(String.Empty))
            {
                colleges = _ucOnlinePortalContext.CourseLists.Where(x => (x.Department == getCoursesOpenRequest.department && x.CourseActive == 1 && x.EnrollmentOpen == 1 && x.ActiveTerm == getCoursesOpenRequest.active_term)).ToList();
            }
            else if (!getCoursesOpenRequest.course_department.Equals(String.Empty) || !getCoursesOpenRequest.department_abbr.Equals(String.Empty))
            {
                colleges = _ucOnlinePortalContext.CourseLists.Where(x => (x.CourseDepartment == getCoursesOpenRequest.course_department || x.CourseDepartmentAbbr == getCoursesOpenRequest.department_abbr) && x.CourseActive == 1 && x.EnrollmentOpen == 1 && x.ActiveTerm == getCoursesOpenRequest.active_term).ToList();
            }

            var collegeResult = colleges.Select(x => new GetCoursesOpenResponse.college
            {
                college_id = x.CourseId,
                college_code = x.CourseCode,
                college_name = x.CourseAbbr + " - " + x.CourseDescription,
                year_limit = x.CourseYearLimit,
                department = x.Department
            }).ToList();

            return new GetCoursesOpenResponse { colleges = collegeResult };
        }

        public TransferSectionResponse TransferSection(TransferSectionRequest transferSectionRequest)
        {
            var ostspStudent = _ucOnlinePortalContext.Ostsps.Where(x => x.StudId == transferSectionRequest.id_number && (x.Status == 0 || x.Status == 1 || x.Status == 3) && x.ActiveTerm == transferSectionRequest.active_term);
            var oenrp = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == transferSectionRequest.id_number && x.ActiveTerm == transferSectionRequest.active_term).FirstOrDefault();

            ostspStudent.ToList().ForEach(x => x.Status = 2);

            if (oenrp.Status < 6)
            {
                return new TransferSectionResponse { success = 0 };
            }

            foreach (Ostsp ostsp in ostspStudent.ToList())
            {
                var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == ostsp.EdpCode && x.ActiveTerm == transferSectionRequest.active_term).FirstOrDefault();
                sched.Size = sched.Size - 1;

                if (oenrp.Status == 10)
                {
                    sched.OfficialEnrolled = sched.OfficialEnrolled - 1;
                }
                else
                {
                    sched.PendingEnrolled = sched.PendingEnrolled - 1;
                }

                _ucOnlinePortalContext.Schedules.Update(sched);
                _ucOnlinePortalContext.SaveChanges();
            }

            var sectionOSTP = _ucOnlinePortalContext.Schedules.Where(x => x.Section == transferSectionRequest.section && x.CourseCode == transferSectionRequest.course_code && x.ActiveTerm == transferSectionRequest.active_term).Select(x => x.EdpCode).ToList();

            foreach (string edpcode in sectionOSTP)
            {
                var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edpcode && x.ActiveTerm == transferSectionRequest.active_term).FirstOrDefault();
                sched.Size = sched.Size + 1;

                if (oenrp.Status == 10)
                {
                    Ostsp ostpN = new Ostsp
                    {
                        StudId = transferSectionRequest.id_number,
                        EdpCode = edpcode,
                        Status = 3,
                        Remarks = null,
                        AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        ActiveTerm = transferSectionRequest.active_term
                    };

                    sched.OfficialEnrolled = sched.OfficialEnrolled + 1;

                    _ucOnlinePortalContext.Schedules.Update(sched);
                    _ucOnlinePortalContext.Ostsps.Add(ostpN);
                }
                else
                {
                    Ostsp ostpN = new Ostsp
                    {
                        StudId = transferSectionRequest.id_number,
                        EdpCode = edpcode,
                        Status = 1,
                        Remarks = null,
                        AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                        ActiveTerm = transferSectionRequest.active_term
                    };

                    sched.PendingEnrolled = sched.OfficialEnrolled + 1;

                    _ucOnlinePortalContext.Schedules.Update(sched);
                    _ucOnlinePortalContext.Ostsps.Add(ostpN);
                }


                oenrp.Section = transferSectionRequest.section;
                _ucOnlinePortalContext.Oenrps.Update(oenrp);
                _ucOnlinePortalContext.SaveChanges();
            }

            return new TransferSectionResponse { success = 1 };
        }

        public CorrectLabAndLecResponse CorrectLabAndLec(CorrectLecAndLabRequest correctLecAndLabRequest)
        {
            var subLecAndLab = _ucOnlinePortalContext.Schedules.Where(x => x.SplitType.Length > 0 && x.ActiveTerm == correctLecAndLabRequest.active_term).OrderBy(x => x.SplitCode).Select(x => x.SplitCode).Distinct().ToList();

            foreach (string edpcode in subLecAndLab)
            {
                var schedules = _ucOnlinePortalContext.Schedules.Where(x => x.SplitCode == edpcode && x.ActiveTerm == correctLecAndLabRequest.active_term).ToList();

                if (schedules.Count == 2)
                {
                    if (schedules[0].Size == schedules[1].Size)
                    {
                        continue;
                    }

                    if (schedules[0].Size > schedules[1].Size)
                    {
                        var studentOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == schedules[0].EdpCode && x.ActiveTerm == correctLecAndLabRequest.active_term).ToList();

                        foreach (Ostsp ostp in studentOstp)
                        {
                            var found = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == schedules[1].EdpCode && x.StudId == ostp.StudId && x.ActiveTerm == correctLecAndLabRequest.active_term).Count();

                            if (found < 1)
                            {
                                Ostsp ostpIns = new Ostsp
                                {
                                    StudId = ostp.StudId,
                                    EdpCode = schedules[1].EdpCode,
                                    Status = ostp.Status,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = correctLecAndLabRequest.active_term
                                };

                                var studentOs = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == ostp.StudId && x.ActiveTerm == correctLecAndLabRequest.active_term).FirstOrDefault();
                                var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == schedules[1].EdpCode && x.ActiveTerm == correctLecAndLabRequest.active_term).FirstOrDefault();

                                if (ostp.Status == 0)
                                {
                                    if (studentOs.Section != null && !studentOs.Section.Equals(""))
                                    {
                                        sched.PendingEnrolled = sched.PendingEnrolled + 1;
                                        sched.Size = sched.Size + 1;
                                    }
                                }
                                else if (ostp.Status == 1)
                                {
                                    sched.PendingEnrolled = sched.PendingEnrolled + 1;
                                    sched.Size = sched.Size + 1;
                                }
                                else if (ostp.Status == 3)
                                {
                                    sched.OfficialEnrolled = sched.OfficialEnrolled + 1;
                                    sched.Size = sched.Size + 1;
                                }

                                _ucOnlinePortalContext.Schedules.Update(sched);
                                _ucOnlinePortalContext.Ostsps.Add(ostpIns);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        var studentOstp = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == schedules[1].EdpCode && x.ActiveTerm == correctLecAndLabRequest.active_term).ToList();

                        foreach (Ostsp ostp in studentOstp)
                        {
                            var found = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == schedules[0].EdpCode && x.StudId == ostp.StudId && x.ActiveTerm == correctLecAndLabRequest.active_term).Count();

                            if (found < 1)
                            {
                                Ostsp ostpIns = new Ostsp
                                {
                                    StudId = ostp.StudId,
                                    EdpCode = schedules[0].EdpCode,
                                    Status = ostp.Status,
                                    Remarks = null,
                                    AdjustedOn = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    ActiveTerm = correctLecAndLabRequest.active_term
                                };

                                var studentOs = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == ostp.StudId && x.ActiveTerm == correctLecAndLabRequest.active_term).FirstOrDefault();
                                var sched = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == schedules[0].EdpCode && x.ActiveTerm == correctLecAndLabRequest.active_term).FirstOrDefault();

                                if (ostp.Status == 0)
                                {
                                    if (studentOs.Section != null && !studentOs.Section.Equals(""))
                                    {
                                        sched.PendingEnrolled = sched.PendingEnrolled + 1;
                                        sched.Size = sched.Size + 1;
                                    }
                                }
                                else if (ostp.Status == 1)
                                {
                                    sched.PendingEnrolled = sched.PendingEnrolled + 1;
                                    sched.Size = sched.Size + 1;
                                }
                                else if (ostp.Status == 3)
                                {
                                    sched.OfficialEnrolled = sched.OfficialEnrolled + 1;
                                    sched.Size = sched.Size + 1;
                                }

                                _ucOnlinePortalContext.Schedules.Update(sched);

                                _ucOnlinePortalContext.Ostsps.Add(ostpIns);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }
                    }
                }
            }

            return new CorrectLabAndLecResponse { success = 1 };
        }

        public SendNotificationForDissolvedResponse SendNotificationDissolved(SendNotificationRequest sendNotificationRequest)
        {
            var schedulesDeferred = _ucOnlinePortalContext.Schedules.Where(x => x.Status == 2 && x.ActiveTerm == sendNotificationRequest.active_term).Select(x => x.EdpCode).ToList();

            foreach (String edpcodes in schedulesDeferred)
            {
                var ostspSched = _ucOnlinePortalContext.Ostsps.Where(x => x.EdpCode == edpcodes && x.ActiveTerm == sendNotificationRequest.active_term).ToList();
                var schedDiss = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edpcodes && x.ActiveTerm == sendNotificationRequest.active_term).FirstOrDefault();

                foreach (Ostsp ostp in ostspSched)
                {
                    if (ostp.Status != 2 || ostp.Status != 4 || ostp.Status != 5)
                    {
                        Notification newNotif = new Notification
                        {
                            StudId = ostp.StudId,
                            NotifRead = 0,
                            Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            Message = Literals.DISSOLVED_SUBJECT + "" + schedDiss.EdpCode + "-" + schedDiss.Description + ".Please cancel the subject.",
                            ActiveTerm = sendNotificationRequest.active_term
                        };

                        _ucOnlinePortalContext.Notifications.Add(newNotif);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
            }

            return new SendNotificationForDissolvedResponse { success = 1 };
        }

        public UserLMSReportResponse CreateLMSUserReport(UserLMSReportRequest userLMSReportRequest)
        {
            var config = _ucOnlinePortalContext.Configs.FirstOrDefault();
            string[] newClass = { "H", "T", "R" };
            int[] status = { 6, 8, 10 };
            string[] exempt = { "JD", "JT", "PD" };

            if (config.CampusLms.Equals("UCLM"))
            {
                var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _course in _ucOnlinePortalContext.CourseLists
                              on _loginInfo.CourseCode equals _course.CourseCode
                              where _loginInfo.Dept == "CL" && _loginInfo.UserType == "STUDENT" && status.Contains(Oenrp.Status)
                              && Oenrp.ActiveTerm == userLMSReportRequest.active_term
                              && _course.ActiveTerm == userLMSReportRequest.active_term
                              select new UserLMSReportResponse.User
                              {
                                  username = "uclm-" + _loginInfo.StudId,
                                  firstname = _loginInfo.FirstName,
                                  lastname = _loginInfo.LastName,
                                  email = _loginInfo.Email,
                                  password = "UC1234",
                                  cohort1 = "SY2020-S2",
                                  cohort2 = config.CampusLms,
                                  cohort3 = "UCLM-" + _course.CourseAbbr,
                                  classification = Oenrp.Classification
                              });

                if (userLMSReportRequest.old_student == 1)
                {
                    result = result.Where(x => !newClass.Contains(x.classification));
                }
                else
                {
                    result = result.Where(x => newClass.Contains(x.classification));
                }

                return new UserLMSReportResponse { users = result.OrderBy(x => x.cohort3).ToList() };
            }
            else
            {
                var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _course in _ucOnlinePortalContext.CourseLists
                              on _loginInfo.CourseCode equals _course.CourseCode
                              where _loginInfo.Dept == "CL" && _loginInfo.UserType == "STUDENT" && status.Contains(Oenrp.Status)
                              && !exempt.Contains(_loginInfo.CourseCode)
                              && Oenrp.ActiveTerm == userLMSReportRequest.active_term
                              && _course.ActiveTerm == userLMSReportRequest.active_term
                              select new UserLMSReportResponse.User
                              {
                                  username = "UCB-" + _loginInfo.StudId,
                                  firstname = _loginInfo.FirstName,
                                  lastname = _loginInfo.LastName,
                                  email = _loginInfo.Email,
                                  password = "UC" + _loginInfo.StudId.Substring(4, 4),
                                  cohort1 = "SY2020-S2",
                                  cohort2 = config.CampusLms,
                                  cohort3 = "UCB-" + _course.CourseAbbr,
                                  classification = Oenrp.Classification
                              });

                if (userLMSReportRequest.old_student == 1)
                {
                    result = result.Where(x => !newClass.Contains(x.classification));
                }
                else
                {
                    result = result.Where(x => newClass.Contains(x.classification));
                }

                return new UserLMSReportResponse { users = result.OrderBy(x => x.cohort3).ToList() };
            }
        }

        public CourseLMSReportResponse CreateLMSCourseReport(CourseLMSReportRequest courseLMSReportRequest)
        {
            var config = _ucOnlinePortalContext.Configs.FirstOrDefault();
            int[] statusActive = { 0, 1, 5 };
            string[] exempt = { "JD", "JT", "PD" };

            var coursesCL = _ucOnlinePortalContext.CourseLists.Where(x => x.Department == "CL" && x.CourseActive == 1 && !exempt.Contains(x.CourseCode) && x.ActiveTerm == courseLMSReportRequest.active_term).Select(x => x.CourseCode).ToList();

            if (config.CampusLms.Equals("UCLM"))
            {
                var result = (from Schedule in _ucOnlinePortalContext.Schedules
                              join _courselist in _ucOnlinePortalContext.CourseLists
                              on Schedule.CourseCode equals _courselist.CourseCode
                              join _subjectinfo in _ucOnlinePortalContext.SubjectInfos
                              on Schedule.InternalCode equals _subjectinfo.InternalCode into sched
                              from _subject_info in sched.DefaultIfEmpty()
                              where coursesCL.Contains(Schedule.CourseCode) && Schedule.Size > 0
                              && Schedule.ActiveTerm == courseLMSReportRequest.active_term
                              && _courselist.ActiveTerm == courseLMSReportRequest.active_term
                              select new CourseLMSReportResponse.course
                              {
                                  shortname = "UCLM-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  fullname = Schedule.EdpCode + "-" + _subject_info.Descr1.ToUpper().Trim() + " " + _subject_info.Descr2.ToUpper().Trim(),
                                  category = Function.categoryIdLM(_courselist.CourseAbbr),
                                  idnumber = "UCLM-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  status = Schedule.Status,
                                  instructor = Schedule.Instructor.Length
                              });

                if (courseLMSReportRequest.dissolved == 1)
                {
                    result = result.Where(x => x.status == 2);
                }
                else
                {
                    result = result.Where(x => statusActive.Contains(x.status) && x.instructor > 0);
                }

                return new CourseLMSReportResponse { courses = result.ToList() };
            }
            else
            {
                var result = (from Schedule in _ucOnlinePortalContext.Schedules
                              join _courselist in _ucOnlinePortalContext.CourseLists
                              on Schedule.CourseCode equals _courselist.CourseCode
                              join _subjectinfo in _ucOnlinePortalContext.SubjectInfos
                              on Schedule.InternalCode equals _subjectinfo.InternalCode into sched
                              from _subject_info in sched.DefaultIfEmpty()
                              where coursesCL.Contains(Schedule.CourseCode) && Schedule.Size > 0
                              && Schedule.ActiveTerm == courseLMSReportRequest.active_term
                              && _courselist.ActiveTerm == courseLMSReportRequest.active_term
                              select new CourseLMSReportResponse.course
                              {
                                  shortname = "UCB-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  fullname = Schedule.EdpCode + "-" + _subject_info.Descr1.ToUpper().Trim() + " " + _subject_info.Descr2.ToUpper().Trim(),
                                  category = Function.categoryIdBanilad(_courselist.CourseAbbr),
                                  idnumber = "UCB-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  status = Schedule.Status,
                                  instructor = Schedule.Instructor.Length
                              });

                if (courseLMSReportRequest.dissolved == 1)
                {
                    result = result.Where(x => x.status == 2);
                }
                else
                {
                    result = result.Where(x => statusActive.Contains(x.status));
                }

                return new CourseLMSReportResponse { courses = result.ToList() };
            }
        }

        public EnrolledLMSReportResponse CreateLMSEnrolledReport(EnrolledLMSReportRequest enrolledLMSReportRequest)
        {
            var config = _ucOnlinePortalContext.Configs.FirstOrDefault();
            int[] statusActive = { 1, 3 };
            int[] notStat = { 4, 5 };
            string[] exempt = { "JD", "JT", "PD" };

            var coursesCL = _ucOnlinePortalContext.CourseLists.Where(x => x.Department == "CL" && x.CourseActive == 1 && !exempt.Contains(x.CourseCode) && x.ActiveTerm == enrolledLMSReportRequest.active_term).Select(x => x.CourseCode).ToList();

            if (config.CampusLms.Equals("UCLM"))
            {
                var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                              join Schedule in _ucOnlinePortalContext.Schedules
                              on Ostsp.EdpCode equals Schedule.EdpCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Ostsp.StudId equals _loginInfo.StudId
                              where coursesCL.Contains(Schedule.CourseCode) && !notStat.Contains(Ostsp.Status) && !exempt.Contains(_loginInfo.CourseCode)
                              && Ostsp.ActiveTerm == enrolledLMSReportRequest.active_term
                              && Schedule.ActiveTerm == enrolledLMSReportRequest.active_term
                              select new EnrolledLMSReportResponse.enroll
                              {
                                  col1 = Function.statDesc(Ostsp.Status),
                                  col2 = "student",
                                  col3 = "uclm-" + Ostsp.StudId,
                                  col4 = "UCLM-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  status = Ostsp.Status
                              });

                if (enrolledLMSReportRequest.delete == 1)
                {
                    result = result.Where(x => x.status == 2);
                }
                else
                {
                    result = result.Where(x => statusActive.Contains(x.status));
                }

                return new EnrolledLMSReportResponse { enrolled = result.ToList() };
            }
            else
            {
                var result = (from Ostsp in _ucOnlinePortalContext.Ostsps
                              join Schedule in _ucOnlinePortalContext.Schedules
                              on Ostsp.EdpCode equals Schedule.EdpCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Ostsp.StudId equals _loginInfo.StudId
                              where coursesCL.Contains(Schedule.CourseCode) && !notStat.Contains(Ostsp.Status) && !exempt.Contains(_loginInfo.CourseCode)
                              && Ostsp.ActiveTerm == enrolledLMSReportRequest.active_term
                              && Schedule.ActiveTerm == enrolledLMSReportRequest.active_term
                              select new EnrolledLMSReportResponse.enroll
                              {
                                  col1 = Function.statDesc(Ostsp.Status),
                                  col2 = "student",
                                  col3 = "UCB-" + Ostsp.StudId,
                                  col4 = "UCB-" + Schedule.Description.ToUpper().Trim() + "-" + Schedule.EdpCode,
                                  status = Ostsp.Status
                              });

                if (enrolledLMSReportRequest.delete == 1)
                {
                    result = result.Where(x => x.status == 2);
                }
                else
                {
                    result = result.Where(x => statusActive.Contains(x.status));
                }

                return new EnrolledLMSReportResponse { enrolled = result.ToList() };
            }
        }

        public TeachersLMSReportResponse CreateTeachersLoadReport(CreateTeachersLoadReportRequest createTeachersLoadReportRequest)
        {
            var config = _ucOnlinePortalContext.Configs.FirstOrDefault();
            string[] exempt = { "JD", "JT", "PD" };
            var coursesCL = _ucOnlinePortalContext.CourseLists.Where(x => x.Department == "CL" && x.CourseActive == 1 && !exempt.Contains(x.CourseCode) && x.ActiveTerm == createTeachersLoadReportRequest.active_term).Select(x => x.CourseCode).ToList();

            if (config.CampusLms.Equals("UCLM"))
            {
                var result = (from Schedules in _ucOnlinePortalContext.Schedules
                              join _loginfo in _ucOnlinePortalContext.LoginInfos
                              on Schedules.Instructor equals _loginfo.StudId
                              where Schedules.Instructor.Length > 0 && _loginfo.UserType == "FACULTY" && coursesCL.Contains(Schedules.CourseCode)
                              && Schedules.ActiveTerm == createTeachersLoadReportRequest.active_term
                              select new TeachersLMSReportResponse.teacher
                              {
                                  username = _loginfo.LastName + "," + _loginfo.FirstName,
                                  course1 = "UCLM-" + Schedules.Description.ToUpper().Trim() + "-" + Schedules.EdpCode,
                                  role1 = "editingteacher"
                              });

                return new TeachersLMSReportResponse { teachers = result.ToList() };
            }
            else
            {
                var result = (from Schedules in _ucOnlinePortalContext.Schedules
                              join _loginfo in _ucOnlinePortalContext.LoginInfos
                              on Schedules.Instructor equals _loginfo.StudId
                              where Schedules.Instructor.Length > 0 && _loginfo.UserType == "FACULTY" && coursesCL.Contains(Schedules.CourseCode)
                              && Schedules.ActiveTerm == createTeachersLoadReportRequest.active_term
                              select new TeachersLMSReportResponse.teacher
                              {
                                  username = _loginfo.LastName + "," + _loginfo.FirstName,
                                  course1 = "UCB-" + Schedules.Description.ToUpper().Trim() + "-" + Schedules.EdpCode,
                                  role1 = "editingteacher"
                              });

                return new TeachersLMSReportResponse { teachers = result.ToList() };
            }
        }

        public GetSHSAssessmentResponse GetSHSAssessment(GetSHSAssessmentRequest getSHSAssessmentRequest)
        {
            var result = (from AssessmentSh in _ucOnlinePortalContext.AssessmentShes
                          where AssessmentSh.StudId == getSHSAssessmentRequest.id_number
                          && AssessmentSh.ActiveTerm == getSHSAssessmentRequest.active_term
                          select new GetSHSAssessmentResponse.Exam
                          {
                              ExamName = AssessmentSh.Exam,
                              OldAccount = AssessmentSh.OldAccount,
                              FeeTuition = AssessmentSh.FeeTuition,
                              FeeLab = AssessmentSh.FeeLab,
                              FeeReg = AssessmentSh.FeeReg,
                              FeeMiscOthers = AssessmentSh.FeeMiscOthers,
                              FeeTotal = AssessmentSh.FeeTotal,
                              TotalDue = AssessmentSh.TotalDue,
                              Payment = AssessmentSh.Payment,
                              ExcessPayment = AssessmentSh.ExcessPayment.HasValue ? AssessmentSh.ExcessPayment.Value : 0,
                              Discount = AssessmentSh.Discount,
                              Adjustment = AssessmentSh.Adjustment,
                              AdjustmentCredit = AssessmentSh.AdjustmentCredit.HasValue ? AssessmentSh.AdjustmentCredit.Value : 0,
                              AdjustmentDebit = AssessmentSh.AdjustmentDebit.HasValue ? AssessmentSh.AdjustmentDebit.Value : 0,
                              Balance = AssessmentSh.Balance,
                              GovernmentSubsidy = AssessmentSh.GovernmentSubsidy.HasValue ? AssessmentSh.GovernmentSubsidy.Value : 0,
                              StudShare = AssessmentSh.StudShare,
                              StudShareBal = AssessmentSh.StudShareBal,
                              AmountDue = AssessmentSh.AmountDue
                          });

            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getSHSAssessmentRequest.active_term).FirstOrDefault();

            if (getSHSAssessmentRequest.exam != "" && getSHSAssessmentRequest.exam != null)
            {
                if (DateTime.Now.Date <= config.Prelim)
                {
                    getSHSAssessmentRequest.exam = "P";
                }
                else if (DateTime.Now.Date <= config.Midterm)
                {
                    getSHSAssessmentRequest.exam = "M";
                }
                else if (DateTime.Now.Date <= config.Semifinal)
                {
                    getSHSAssessmentRequest.exam = "S";
                }
                else
                {
                    getSHSAssessmentRequest.exam = "F";
                }
            }

            if (getSHSAssessmentRequest.exam != "" && getSHSAssessmentRequest.exam != null)
            {
                result = result.Where(x => x.ExamName == getSHSAssessmentRequest.exam);
            }

            return new GetSHSAssessmentResponse { exams = result.ToList() };
        }

        public GetCLAssessmentResponse GetCLAssessment(GetCLAssessmentRequest getCLAssessmentRequest)
        {
            var result = (from AssessmentCl in _ucOnlinePortalContext.AssessmentCls
                          where AssessmentCl.StudId == getCLAssessmentRequest.id_number
                          && AssessmentCl.ActiveTerm == getCLAssessmentRequest.active_term
                          select new GetCLAssessmentResponse.Exam
                          {
                              ExamName = AssessmentCl.Exam,
                              OldAccount = AssessmentCl.OldAccount,
                              FeeTuition = AssessmentCl.FeeTuition,
                              FeeLab = AssessmentCl.FeeLab,
                              FeeReg = AssessmentCl.FeeReg,
                              FeeMisc = AssessmentCl.FeeMisc,
                              AssessTotal = AssessmentCl.AssessTotal,
                              Payment = AssessmentCl.Payment,
                              ExcessPayment = AssessmentCl.ExcessPayment.HasValue ? AssessmentCl.ExcessPayment.Value : 0,
                              Discount = AssessmentCl.Discount,
                              Adjustment = AssessmentCl.Adjustment,
                              AdjustmentCredit = AssessmentCl.AdjustmentCredit.HasValue ? AssessmentCl.AdjustmentCredit.Value : 0,
                              AdjustmentDebit = AssessmentCl.AdjustmentDebit.HasValue ? AssessmentCl.AdjustmentDebit.Value : 0,
                              Balance = AssessmentCl.Balance,
                              AmountDue = AssessmentCl.AmountDue
                          });

            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getCLAssessmentRequest.active_term).FirstOrDefault();

            if (getCLAssessmentRequest.exam != "" && getCLAssessmentRequest.exam != null)
            {
                if (DateTime.Now.Date <= config.Prelim)
                {
                    getCLAssessmentRequest.exam = "P";
                }
                else if (DateTime.Now.Date <= config.Midterm)
                {
                    getCLAssessmentRequest.exam = "M";
                }
                else if (DateTime.Now.Date <= config.Semifinal)
                {
                    getCLAssessmentRequest.exam = "S";
                }
                else
                {
                    getCLAssessmentRequest.exam = "F";
                }
            }

            if (getCLAssessmentRequest.exam != "" && getCLAssessmentRequest.exam != null)
            {
                result = result.Where(x => x.ExamName == getCLAssessmentRequest.exam);
            }

            return new GetCLAssessmentResponse { exams = result.ToList() };
        }

        public GetBEAssessmentResponse GetBEssessment(GetBEAssessmentRequest getBEAssessmentRequest)
        {
            var result = (from AssessmentBe in _ucOnlinePortalContext.AssessmentBes
                          where AssessmentBe.StudId == getBEAssessmentRequest.id_number
                          && AssessmentBe.ActiveTerm == getBEAssessmentRequest.active_term
                          select new GetBEAssessmentResponse.Exam
                          {
                              ExamName = AssessmentBe.Exam,
                              OldAccount = AssessmentBe.OldAccount,
                              FeeTuition = AssessmentBe.FeeTuition,
                              FeeLab = AssessmentBe.FeeLab,
                              FeeReg = AssessmentBe.FeeReg,
                              FeeMiscOthers = AssessmentBe.FeeMiscOthers,
                              FeeTotal = AssessmentBe.FeeTotal,
                              TotalDue = AssessmentBe.TotalDue,
                              Payment = AssessmentBe.Payment,
                              Discount = AssessmentBe.Discount,
                              Adjustment = AssessmentBe.Adjustment,
                              Balance = AssessmentBe.Balance,
                              StudShare = AssessmentBe.StudShare,
                              StudShareBal = AssessmentBe.StudShareBal,
                              AmountDue = AssessmentBe.AmountDue
                          });

            if (getBEAssessmentRequest.exam != "" && getBEAssessmentRequest.exam != null)
            {
                result = result.Where(x => x.ExamName == getBEAssessmentRequest.exam);
            }

            return new GetBEAssessmentResponse { exams = result.ToList() };
        }

        public GetBasicEdMonthResponse GetBasicEdMonth(GetBasicEdMonthRequest getBasicEdMonthRequest)
        {
            var configBasic = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getBasicEdMonthRequest.active_term).FirstOrDefault();

            return new GetBasicEdMonthResponse { start_month = new DateTime(2021, configBasic.BasicStart, 1).ToString("MMMM"), end_month = new DateTime(2021, configBasic.BasicEnd, 1).ToString("MMMM") };
        }

        public GetStudentBalancePerCategoryResponse GetStudentBalancePerCategory(GetStudentBalancePerCategoryRequest getStudentBalancePerCategoryRequest)
        {
            var isShs = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getStudentBalancePerCategoryRequest.course_department && x.ActiveTerm == getStudentBalancePerCategoryRequest.active_term).Select(x => x.Department).FirstOrDefault();
            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getStudentBalancePerCategoryRequest.active_term).FirstOrDefault();
            int[] status = { 6, 8, 10 };

            String current_exam = String.Empty;

            if (DateTime.Now.Date <= config.Prelim)
            {
                current_exam = "P";
            }
            else if (DateTime.Now.Date <= config.Midterm)
            {
                current_exam = "M";
            }
            else if (DateTime.Now.Date <= config.Semifinal)
            {
                current_exam = "S";
            }
            else
            {
                current_exam = "F";
            }

            if (isShs.Equals("SH"))
            {
                var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                              join _courselist in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courselist.CourseCode
                              join _logininfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _logininfo.StudId
                              join AssessmentSh in _ucOnlinePortalContext.AssessmentShes
                              on Oenrp.StudId equals AssessmentSh.StudId into assessment
                              from AssessmentSh in assessment.DefaultIfEmpty()
                              where _courselist.CourseDepartmentAbbr == getStudentBalancePerCategoryRequest.course_department && AssessmentSh.Exam == current_exam && Oenrp.Status > 5
                              && Oenrp.ActiveTerm == getStudentBalancePerCategoryRequest.active_term && AssessmentSh.ActiveTerm == getStudentBalancePerCategoryRequest.active_term
                              && _courselist.ActiveTerm == getStudentBalancePerCategoryRequest.active_term
                              select new GetStudentBalancePerCategoryResponse.student_info
                              {
                                  id_number = _logininfo.StudId,
                                  lastname = _logininfo.LastName,
                                  first_name = _logininfo.FirstName,
                                  course_year = _courselist.CourseAbbr + " - " + _logininfo.Year,
                                  total_assessment = AssessmentSh.StudShare,
                                  total_balance = AssessmentSh.StudShareBal,
                                  due = AssessmentSh.AmountDue,
                                  email = _logininfo.Email,
                                  mobile_number = _logininfo.MobileNumber,
                                  status = Oenrp.Status
                              });

                if (config.CampusLms.Equals("UCLM"))
                {
                    result = result.Where(x => status.Contains(x.status));
                }
                else
                {
                    result = result.Where(x => x.status == 10);
                }

                var Category1 = result.Where(x => x.due > 10000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();
                var Category2 = result.Where(x => x.due >= 5000 && x.due <= 10000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();
                var Category3 = result.Where(x => x.due < 5000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();

                return new GetStudentBalancePerCategoryResponse { category_1 = Category1, category_2 = Category2, category_3 = Category3 };

            }
            else
            {
                var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                              join _courselist in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courselist.CourseCode
                              join _logininfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _logininfo.StudId
                              join AssessmentCl in _ucOnlinePortalContext.AssessmentCls
                              on Oenrp.StudId equals AssessmentCl.StudId into assessment
                              from AssessmentCl in assessment.DefaultIfEmpty()
                              where _courselist.CourseDepartmentAbbr == getStudentBalancePerCategoryRequest.course_department && AssessmentCl.Exam == current_exam && Oenrp.Status > 5
                              && Oenrp.ActiveTerm == getStudentBalancePerCategoryRequest.active_term && AssessmentCl.ActiveTerm == getStudentBalancePerCategoryRequest.active_term
                              && _courselist.ActiveTerm == getStudentBalancePerCategoryRequest.active_term
                              select new GetStudentBalancePerCategoryResponse.student_info
                              {
                                  id_number = _logininfo.StudId,
                                  lastname = _logininfo.LastName,
                                  first_name = _logininfo.FirstName,
                                  course_year = _courselist.CourseAbbr + " - " + _logininfo.Year,
                                  total_assessment = AssessmentCl.AssessTotal,
                                  total_balance = AssessmentCl.Balance,
                                  due = AssessmentCl.AmountDue,
                                  email = _logininfo.Email,
                                  mobile_number = _logininfo.MobileNumber,
                                  status = Oenrp.Status
                              });

                if (config.CampusLms.Equals("UCLM"))
                {
                    result = result.Where(x => status.Contains(x.status));
                }
                else
                {
                    result = result.Where(x => x.status == 10);
                }

                var Category1 = result.Where(x => x.due > 10000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();
                var Category2 = result.Where(x => x.due >= 5000 && x.due <= 10000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();
                var Category3 = result.Where(x => x.due < 5000).OrderBy(x => x.course_year).ThenBy(x => x.due).ToList();

                return new GetStudentBalancePerCategoryResponse { category_1 = Category1, category_2 = Category2, category_3 = Category3 };
            }
        }

        public ViewPermitListResponse ViewPermitList(ViewPermitListRequest viewPermitListRequest)
        {
            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == viewPermitListRequest.active_term).FirstOrDefault();

            String current_exam = String.Empty;

            if (DateTime.Now.Date <= config.Prelim)
            {
                current_exam = "P";
            }
            else if (DateTime.Now.Date <= config.Midterm)
            {
                current_exam = "M";
            }
            else if (DateTime.Now.Date <= config.Semifinal)
            {
                current_exam = "S";
            }
            else
            {
                current_exam = "F";
            }

            var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == viewPermitListRequest.edp_code && x.ActiveTerm == viewPermitListRequest.active_term).FirstOrDefault();

            if (schedule != null)
            {
                var courseSHS = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == schedule.CourseCode && x.ActiveTerm == viewPermitListRequest.active_term).FirstOrDefault();

                var result = (from _ostsp in _ucOnlinePortalContext.Ostsps
                              join Schedule in _ucOnlinePortalContext.Schedules
                              on _ostsp.EdpCode equals Schedule.EdpCode
                              join _loginfo in _ucOnlinePortalContext.LoginInfos
                              on _ostsp.StudId equals _loginfo.StudId
                              join _course in _ucOnlinePortalContext.CourseLists
                              on _loginfo.CourseCode equals _course.CourseCode
                              where _ostsp.EdpCode == viewPermitListRequest.edp_code && (_ostsp.Status == 1 || _ostsp.Status == 3)
                              && _ostsp.ActiveTerm == viewPermitListRequest.active_term && Schedule.ActiveTerm == viewPermitListRequest.active_term
                              && _course.ActiveTerm == viewPermitListRequest.active_term
                              select new ViewPermitListResponse.Enrolled
                              {
                                  id_number = _loginfo.StudId,
                                  last_name = _loginfo.LastName,
                                  firstname = _loginfo.FirstName,
                                  course_year = _course.CourseAbbr + " " + _loginfo.Year,
                                  mobile_number = _loginfo.MobileNumber,
                                  email = _loginfo.Email,
                                  has_permit = _ucOnlinePortalContext.AssessmentCls.Where(x => x.StudId == _loginfo.StudId && x.Exam == current_exam && x.AmountDue < config.PermitCutoff && x.ActiveTerm == viewPermitListRequest.active_term).Count() == 1 ? 1 : _ucOnlinePortalContext.AssessmentShes.Where(x => x.StudId == _loginfo.StudId && x.Exam == current_exam && x.AmountDue < config.PermitCutoff && x.ActiveTerm == viewPermitListRequest.active_term).Count() == 1 ? 1 : 0,
                                  applied_promi = 0
                              }).ToList();


                List<ViewPermitListResponse.Enrolled> enrolledNew = new List<ViewPermitListResponse.Enrolled>();

                foreach (ViewPermitListResponse.Enrolled nNew in result)
                {
                    ViewPermitListResponse.Enrolled nStud = new ViewPermitListResponse.Enrolled
                    {
                        id_number = nNew.id_number,
                        last_name = nNew.last_name,
                        firstname = nNew.firstname,
                        course_year = nNew.course_year,
                        mobile_number = nNew.mobile_number,
                        email = nNew.email,
                        has_permit = nNew.has_permit,
                        applied_promi = appliedPromi(nNew.id_number, current_exam),
                    };

                    enrolledNew.Add(nStud);
                }



                if (courseSHS.Department == "CL")
                {
                    if (current_exam == "P")
                    {
                        current_exam = "PRELIM";
                    }
                    else if (current_exam == "M")
                    {
                        current_exam = "MIDTERM";
                    }
                    else if (current_exam == "S")
                    {
                        current_exam = "SEMI-FINALS";
                    }
                    else
                    {
                        current_exam = "FINALS";
                    }
                }
                else
                {
                    if (current_exam == "P")
                    {
                        current_exam = "3RD MASTERY";
                    }
                    else if (current_exam == "M")
                    {
                        current_exam = "3RD QUARTERLY";
                    }
                    else if (current_exam == "S")
                    {
                        current_exam = "4TH MASTERY";
                    }
                    else
                    {
                        current_exam = "4TH QUARTERLY";
                    }
                }

                ViewPermitListResponse response = new ViewPermitListResponse
                {
                    edp_code = schedule.EdpCode,
                    subject_name = schedule.Description + " " + schedule.SubType,
                    time_info = schedule.TimeStart + " - " + schedule.TimeEnd + " " + schedule.Mdn + " " + schedule.Days,
                    units = schedule.Units.ToString(),
                    enrolled = enrolledNew.OrderBy(x => x.last_name).ToList(),
                    subject_size = schedule.Size,
                    exam_type = current_exam
                };

                return response;
            }

            return null;
        }

        public GetPaymentResponse GetPayments(GetPaymentRequest getPaymentRequest)
        {
            var payments = (from _payments in _ucOnlinePortalContext.Attachments
                            where _payments.StudId == getPaymentRequest.id_number && _payments.Type == "Payment"
                            && _payments.ActiveTerm == getPaymentRequest.active_term
                            select new GetPaymentResponse.image_file
                            {
                                file_name = _payments.Filename,
                                file_id = _payments.AttachmentId,
                                status = (int)_payments.Acknowledged.Value
                            }).OrderBy(x => x.file_id).ToList();

            if (getPaymentRequest.status == 0)
            {
                payments = payments.Where(x => x.status == 0).OrderBy(x => x.file_id).ToList();
            }
            else if (getPaymentRequest.status == 1)
            {
                payments = payments.Where(x => x.status == 1 || x.status == 2).OrderBy(x => x.file_id).ToList();
            }

            if (getPaymentRequest.exam_type != "A")
            {
                payments = payments.Where(x => x.file_name.Contains("[payment]_[" + getPaymentRequest.exam_type.ToUpper() + "]_")).ToList();
            }

            return new GetPaymentResponse { images = payments };
        }

        public RequestExamPromiResponse RequestExamPromi(RequestExamPromiRequest requestExamPromiRequest)
        {
            double amountDue = 0;

            if (requestExamPromiRequest.department == "CL")
            {
                var CollegeAss = _ucOnlinePortalContext.AssessmentCls.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.Exam == requestExamPromiRequest.exam && x.ActiveTerm == requestExamPromiRequest.active_term).FirstOrDefault();
                if (CollegeAss != null)
                {
                    amountDue = CollegeAss.AmountDue;
                }
                else
                {
                    return new RequestExamPromiResponse { success = 0 };
                }
            }
            else if (requestExamPromiRequest.department == "SH")
            {
                var seniorAss = _ucOnlinePortalContext.AssessmentShes.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.Exam == requestExamPromiRequest.exam && x.ActiveTerm == requestExamPromiRequest.active_term).FirstOrDefault();
                if (seniorAss != null)
                {
                    amountDue = seniorAss.AmountDue;
                }
                else
                {
                    return new RequestExamPromiResponse { success = 0 };
                }
            }
            else
            {
                var basicAss = _ucOnlinePortalContext.AssessmentBes.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.Exam == requestExamPromiRequest.exam && x.ActiveTerm == requestExamPromiRequest.active_term).FirstOrDefault();
                if (basicAss != null)
                {
                    amountDue = basicAss.AmountDue;
                }
                else
                {
                    return new RequestExamPromiResponse { success = 0 };
                }
            }

            int toWho = requestExamPromiRequest.promise_pay > (amountDue * .30) ? 1 : 2;
            int hasData = 0;

            if (requestExamPromiRequest.exam.ToUpper().Equals("P"))
            {
                hasData = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.RequestPrelimDate != null && x.ActiveTerm == requestExamPromiRequest.active_term).Count();
            }
            else if (requestExamPromiRequest.exam.ToUpper().Equals("M"))
            {
                hasData = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.RequestMidtermDate != null && x.ActiveTerm == requestExamPromiRequest.active_term).Count();
            }
            else if (requestExamPromiRequest.exam.ToUpper().Equals("S"))
            {
                hasData = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.RequestSemiDate != null && x.ActiveTerm == requestExamPromiRequest.active_term).Count();
            }
            else
            {
                hasData = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.RequestFinalDate != null && x.ActiveTerm == requestExamPromiRequest.active_term).Count();
            }

            if (hasData == 1)
            {
                return new RequestExamPromiResponse { success = 0 };
            }
            else
            {
                Promissory promi = new Promissory
                {
                    StudId = requestExamPromiRequest.stud_id,
                    PromiMessage = requestExamPromiRequest.message,
                    PromiDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    ActiveTerm = requestExamPromiRequest.active_term
                };

                _ucOnlinePortalContext.Promissories.Add(promi);
                _ucOnlinePortalContext.SaveChanges();

                int promiId = promi.PromiId;

                var hasPromiData = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == requestExamPromiRequest.stud_id && x.ActiveTerm == requestExamPromiRequest.active_term).FirstOrDefault();

                if (requestExamPromiRequest.exam.ToUpper().Equals("P"))
                {
                    if (hasPromiData == null)
                    {
                        ExamPromissory examProm = new ExamPromissory
                        {
                            StudId = requestExamPromiRequest.stud_id,
                            PrelimPromiId = promiId,
                            RequestPrelim = (short)toWho,
                            RequestPrelimDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            RequestPrelimAmount = requestExamPromiRequest.promise_pay,
                            ActiveTerm = requestExamPromiRequest.active_term
                        };

                        _ucOnlinePortalContext.ExamPromissories.Add(examProm);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                    else
                    {
                        hasPromiData.PrelimPromiId = promiId;
                        hasPromiData.RequestPrelim = (short)toWho;
                        hasPromiData.RequestPrelimDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        hasPromiData.RequestPrelimAmount = requestExamPromiRequest.promise_pay;
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
                else if (requestExamPromiRequest.exam.ToUpper().Equals("M"))
                {
                    if (hasPromiData == null)
                    {
                        ExamPromissory examProm = new ExamPromissory
                        {
                            StudId = requestExamPromiRequest.stud_id,
                            MidtermPromiId = promiId,
                            RequestMidterm = (short)toWho,
                            RequestMidtermDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            RequestMidtermAmount = requestExamPromiRequest.promise_pay,
                            ActiveTerm = requestExamPromiRequest.active_term
                        };

                        _ucOnlinePortalContext.ExamPromissories.Add(examProm);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                    else
                    {
                        hasPromiData.MidtermPromiId = promiId;
                        hasPromiData.RequestMidterm = (short)toWho;
                        hasPromiData.RequestMidtermDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        hasPromiData.RequestMidtermAmount = requestExamPromiRequest.promise_pay;
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
                else if (requestExamPromiRequest.exam.ToUpper().Equals("S"))
                {
                    if (hasPromiData == null)
                    {
                        ExamPromissory examProm = new ExamPromissory
                        {
                            StudId = requestExamPromiRequest.stud_id,
                            SemiPromiId = promiId,
                            RequestSemi = (short)toWho,
                            RequestSemiDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            RequestSemiAmount = requestExamPromiRequest.promise_pay,
                            ActiveTerm = requestExamPromiRequest.active_term
                        };

                        _ucOnlinePortalContext.ExamPromissories.Add(examProm);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                    else
                    {
                        hasPromiData.SemiPromiId = promiId;
                        hasPromiData.RequestSemi = (short)toWho;
                        hasPromiData.RequestSemiDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        hasPromiData.RequestSemiAmount = requestExamPromiRequest.promise_pay;
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
                else
                {
                    if (hasPromiData == null)
                    {
                        ExamPromissory examProm = new ExamPromissory
                        {
                            StudId = requestExamPromiRequest.stud_id,
                            SemiPromiId = promiId,
                            RequestFinal = (short)toWho,
                            RequestFinalDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                            RequestFinalAmount = requestExamPromiRequest.promise_pay,
                            ActiveTerm = requestExamPromiRequest.active_term
                        };

                        _ucOnlinePortalContext.ExamPromissories.Add(examProm);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                    else
                    {
                        hasPromiData.SemiPromiId = promiId;
                        hasPromiData.RequestSemi = (short)toWho;
                        hasPromiData.RequestSemiDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        hasPromiData.RequestFinalAmount = requestExamPromiRequest.promise_pay;
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
            }

            return new RequestExamPromiResponse { success = 1 };
        }

        public GetExamPromissoryListResponse GetExamPromisorryList(GetExamPromisorryListRequest getExamPromisorryListRequest)
        {
            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getExamPromisorryListRequest.active_Term).FirstOrDefault();

            if (DateTime.Now.Date <= config.Prelim)
            {
                getExamPromisorryListRequest.exam = "P";
            }
            else if (DateTime.Now.Date <= config.Midterm)
            {
                getExamPromisorryListRequest.exam = "M";
            }
            else if (DateTime.Now.Date <= config.Semifinal)
            {
                getExamPromisorryListRequest.exam = "S";
            }
            else
            {
                getExamPromisorryListRequest.exam = "F";
            }

            int take = (int)getExamPromisorryListRequest.limit;
            int skip = (int)getExamPromisorryListRequest.limit * ((int)getExamPromisorryListRequest.page - 1);

            //always get status 5 -> for dean
            IQueryable<GetExamPromissoryListResponse.Student> result = null;

            if (getExamPromisorryListRequest.department.Equals("CL"))
            {
                if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("P"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.PrelimPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentCls
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestPrelim == getExamPromisorryListRequest.status && _promi.RequestPrelimDate != null && _assess.Exam == "P"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestPrelim.Value,
                                  date = _promi.RequestPrelimDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestPrelimAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("M"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.MidtermPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentCls
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestMidterm == getExamPromisorryListRequest.status && _promi.RequestMidtermDate != null && _assess.Exam == "M"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestMidterm.Value,
                                  date = _promi.RequestMidtermDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestMidtermAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("S"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.SemiPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentCls
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestSemi == getExamPromisorryListRequest.status && _promi.RequestSemiDate != null && _assess.Exam == "S"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestSemi.Value,
                                  date = _promi.RequestSemiDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestSemiAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.FinalPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentCls
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestFinal == getExamPromisorryListRequest.status && _promi.RequestFinalDate != null && _assess.Exam == "F"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestFinal.Value,
                                  date = _promi.RequestFinalDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestFinalAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
            }
            else
            {
                if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("P"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.PrelimPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentShes
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestPrelim == getExamPromisorryListRequest.status && _promi.RequestPrelimDate != null && _assess.Exam == "P"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestPrelim.Value,
                                  date = _promi.RequestPrelimDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestPrelimAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("M"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.MidtermPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentShes
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestMidterm == getExamPromisorryListRequest.status && _promi.RequestMidtermDate != null && _assess.Exam == "M"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestMidterm.Value,
                                  date = _promi.RequestMidtermDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestMidtermAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else if (getExamPromisorryListRequest.exam.ToString().ToUpper().Equals("S"))
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.SemiPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentShes
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestSemi == getExamPromisorryListRequest.status && _promi.RequestSemiDate != null && _assess.Exam == "S"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestSemi.Value,
                                  date = _promi.RequestSemiDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestSemiAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
                else
                {
                    result = (from _promi in _ucOnlinePortalContext.ExamPromissories
                              join Oenrp in _ucOnlinePortalContext.Oenrps
                              on _promi.StudId equals Oenrp.StudId
                              join _courseList in _ucOnlinePortalContext.CourseLists
                              on Oenrp.CourseCode equals _courseList.CourseCode
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on Oenrp.StudId equals _loginInfo.StudId
                              join _promiMes in _ucOnlinePortalContext.Promissories
                              on _promi.FinalPromiId equals _promiMes.PromiId
                              join _assess in _ucOnlinePortalContext.AssessmentShes
                              on _promi.StudId equals _assess.StudId
                              where (Oenrp.Status == 8 || Oenrp.Status == 10) && _promi.RequestFinal == getExamPromisorryListRequest.status && _promi.RequestFinalDate != null && _assess.Exam == "F"
                              && _promi.ActiveTerm == getExamPromisorryListRequest.active_Term && Oenrp.ActiveTerm == getExamPromisorryListRequest.active_Term
                              && _courseList.ActiveTerm == getExamPromisorryListRequest.active_Term && _promiMes.ActiveTerm == getExamPromisorryListRequest.active_Term
                              select new GetExamPromissoryListResponse.Student
                              {
                                  id_number = Oenrp.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  mi = _loginInfo.Mi,
                                  suffix = _loginInfo.Suffix,
                                  classification = Utils.Function.getClassification(Oenrp.Classification),
                                  course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                                  course_code = Oenrp.CourseCode,
                                  status = _promi.RequestFinal.Value,
                                  date = _promi.RequestFinalDate.Value,
                                  message = _promiMes.PromiMessage,
                                  promise_pay = _promi.RequestFinalAmount.Value,
                                  needed_payment = (int)_assess.AmountDue
                              });
                }
            }

            if (!getExamPromisorryListRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getExamPromisorryListRequest.course_department).ToList();
                var courses = courseList.Select(x => x.CourseCode).ToList();

                result = result.Where(x => courses.Contains(x.course_code));
            }

            var count = result.Count();

            if (getExamPromisorryListRequest.page != 0 && getExamPromisorryListRequest.limit != 0)
            {
                result = result.OrderBy(x => x.date).Skip(skip).Take(take);
            }

            return new GetExamPromissoryListResponse { students = result.ToList(), count = count };
        }

        public GetStudentPaymentListResponse GetStudentPaymentList(GetStudentPaymentListRequest getStudentPaymentListRequest)
        {
            var config = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == getStudentPaymentListRequest.active_term).FirstOrDefault();

            if (DateTime.Now.Date <= config.Prelim)
            {
                getStudentPaymentListRequest.exam = "P";
            }
            else if (DateTime.Now.Date <= config.Midterm)
            {
                getStudentPaymentListRequest.exam = "M";
            }
            else if (DateTime.Now.Date <= config.Semifinal)
            {
                getStudentPaymentListRequest.exam = "S";
            }
            else
            {
                getStudentPaymentListRequest.exam = "F";
            }

            int take = (int)getStudentPaymentListRequest.limit;
            int skip = (int)getStudentPaymentListRequest.limit * ((int)getStudentPaymentListRequest.page - 1);

            //always get status 5 -> for dean
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          join _attachment in _ucOnlinePortalContext.Attachments
                          on Oenrp.StudId equals _attachment.StudId
                          where (Oenrp.Status == 8 || Oenrp.Status == 10)
                          && Oenrp.ActiveTerm == getStudentPaymentListRequest.active_term && _courseList.ActiveTerm == getStudentPaymentListRequest.active_term
                          && _attachment.ActiveTerm == getStudentPaymentListRequest.active_term
                          select new GetStudentPaymentListResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              approved_promi_amount = 0,
                              pending_exam_promi = 0,
                              status = (int)_attachment.Acknowledged
                          });

            if (getStudentPaymentListRequest.status == 0)
            {
                result = result.Where(x => x.status == 0);
            }
            else
            {
                result = result.Where(x => x.status == 1 || x.status == 2);
            }

            if (!getStudentPaymentListRequest.course_department.Equals(String.Empty))
            {
                var courseList = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == getStudentPaymentListRequest.course_department && x.ActiveTerm == getStudentPaymentListRequest.active_term).ToList();
                var courses = courseList.Select(x => x.CourseCode).ToList();

                result = result.Where(x => courses.Contains(x.course_code));
            }

            var newRes = result.ToList();
            var nnRe = newRes.GroupBy(x => x.id_number).Select(x => x.First());

            List<GetStudentPaymentListResponse.Student> newStudentList = new List<GetStudentPaymentListResponse.Student>();

            foreach (GetStudentPaymentListResponse.Student student in nnRe)
            {
                GetStudentPaymentListResponse.Student nStud = new GetStudentPaymentListResponse.Student
                {
                    id_number = student.id_number,
                    lastname = student.lastname,
                    firstname = student.firstname,
                    mi = student.mi,
                    suffix = student.suffix,
                    classification = student.classification,
                    course_year = student.course_year,
                    course_code = student.course_code,
                    pending_exam_promi = promiStatus(student.id_number, getStudentPaymentListRequest.exam),
                    approved_promi_amount = promiAmount(student.id_number, getStudentPaymentListRequest.exam)
                };

                newStudentList.Add(nStud);
            }

            var count = newStudentList.Count();

            if (getStudentPaymentListRequest.page != 0 && getStudentPaymentListRequest.limit != 0)
            {
                newStudentList = (List<GetStudentPaymentListResponse.Student>)newStudentList.Skip(skip).Take(take);
            }

            return new GetStudentPaymentListResponse { students = newStudentList, count = count };
        }

        public AcknowledgePaymentResponse AcknowledgePayment(AcknowledgePaymentRequest acknowledgePaymentRequest)
        {
            var fileN = _ucOnlinePortalContext.Attachments.Where(x => x.Filename == acknowledgePaymentRequest.filename && x.ActiveTerm == acknowledgePaymentRequest.active_term).FirstOrDefault();

            Notification newNotif = new Notification
            {
                StudId = acknowledgePaymentRequest.filename.Substring(0, 8),
                NotifRead = 0,
                Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                Message = "Cashier acknowledged your payment : " + acknowledgePaymentRequest.filename,
                ActiveTerm = acknowledgePaymentRequest.active_term
            };

            if (acknowledgePaymentRequest.duplicate! != null && acknowledgePaymentRequest.duplicate == 1)
            {
                fileN.Acknowledged = 2;

                newNotif = new Notification
                {
                    StudId = acknowledgePaymentRequest.filename.Substring(0, 8),
                    NotifRead = 0,
                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                    Message = "You have a duplicate payment attached : " + acknowledgePaymentRequest.filename,
                    ActiveTerm = acknowledgePaymentRequest.active_term
                };
            }
            else if (acknowledgePaymentRequest.duplicate! != null && acknowledgePaymentRequest.duplicate == 0)
            {
                fileN.Acknowledged = 0;
            }
            else
            {
                fileN.Acknowledged = 1;
            }

            _ucOnlinePortalContext.Notifications.Add(newNotif);
            _ucOnlinePortalContext.Attachments.Update(fileN);
            _ucOnlinePortalContext.SaveChanges();

            return new AcknowledgePaymentResponse { success = 1 };
        }


        public int appliedPromi(string stud_id, string exam)
        {
            int toReturn = 0;

            if (exam.Equals("P"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && (x.RequestPrelim == 1 || x.RequestPrelim == 2 || x.RequestPrelim == 3)).Count();
            }
            else if (exam.Equals("M"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && (x.RequestMidterm == 1 || x.RequestMidterm == 2 || x.RequestMidterm == 3)).Count();
            }
            else if (exam.Equals("S"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && (x.RequestSemi == 1 || x.RequestSemi == 2 || x.RequestSemi == 3)).Count();
            }
            else
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && (x.RequestFinal == 1 || x.RequestFinal == 2 || x.RequestFinal == 3)).Count();
            }

            return toReturn;
        }

        public int promiStatus(string stud_id, string exam)
        {
            int toReturn = 0;

            if (exam.Equals("P"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelimDate != null).Count() == 1 ? 1 : 0;

                if (toReturn != 0)
                {
                    toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelim == 3).Count() == 1 ? 3 : 1;
                }
            }
            else if (exam.Equals("M"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestMidtermDate != null).Count() == 1 ? 1 : 0;

                if (toReturn != 0)
                {
                    toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestMidterm == 3).Count() == 1 ? 3 : 1;
                }
            }
            else if (exam.Equals("S"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestSemiDate != null).Count() == 1 ? 1 : 0;

                if (toReturn != 0)
                {
                    toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestSemi == 3).Count() == 1 ? 3 : 1;
                }
            }
            else
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestFinalDate != null).Count() == 1 ? 1 : 0;

                if (toReturn != 0)
                {
                    toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestFinal == 3).Count() == 1 ? 3 : 1;
                }
            }

            return toReturn;
        }

        public int promiAmount(string stud_id, string exam)
        {

            int? toReturn = 0;

            if (exam.Equals("P"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelim == 3).Select(x => x.RequestPrelimAmount).FirstOrDefault();
            }
            else if (exam.Equals("M"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelim == 3).Select(x => x.RequestPrelimAmount).FirstOrDefault();
            }
            else if (exam.Equals("S"))
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelim == 3).Select(x => x.RequestPrelimAmount).FirstOrDefault();
            }
            else
            {
                toReturn = _ucOnlinePortalContext.ExamPromissories.Where(x => x.StudId == stud_id && x.RequestPrelim == 3).Select(x => x.RequestPrelimAmount).FirstOrDefault();
            }

            if (toReturn != null)
            {
                toReturn = toReturn.Value;
            }
            else
            {
                toReturn = 0;
            }

            return toReturn.Value;
        }

        public UploadGradesResponse UploadGrades(UploadGradesRequest uploadGradesRequest)
        {
            var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == uploadGradesRequest.edp_code && x.ActiveTerm == uploadGradesRequest.active_term).FirstOrDefault();
            var scheduleBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == uploadGradesRequest.edp_code && x.ActiveTerm == uploadGradesRequest.active_term).FirstOrDefault();

            if (schedule == null && scheduleBe == null)
            {
                return new UploadGradesResponse { success = 0 };
            }
            else
            {
                string isSHS = "";

                if (schedule != null)
                {
                    isSHS = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == schedule.CourseCode && x.ActiveTerm == uploadGradesRequest.active_term).Select(x => x.Department).FirstOrDefault();
                }
                else
                {
                    isSHS = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == scheduleBe.CourseCode && x.ActiveTerm == uploadGradesRequest.active_term).Select(x => x.Department).FirstOrDefault();
                }


                if (isSHS.Equals("SH"))
                {
                    foreach (UploadGradesRequest.grades grade in uploadGradesRequest.stud_grades)
                    {
                        var GradesExist = _ucOnlinePortalContext.GradesShes.Where(x => x.StudId == grade.stud_id && x.EdpCode == uploadGradesRequest.edp_code && x.ActiveTerm == uploadGradesRequest.active_term).FirstOrDefault();
                        var GradeEvalExist = _ucOnlinePortalContext.GradeEvaluations.Where(x => x.StudId == grade.stud_id && x.IntCode == schedule.InternalCode && x.Term == uploadGradesRequest.active_term).FirstOrDefault();

                        if (grade.grade != "")
                        {
                            String exa = "";
                            GradesSh doneUpload = null;

                            if (uploadGradesRequest.exam == 1)
                            {
                                exa = "Third Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesShes.Where(x => x.StudId == grade.stud_id && x.Grade1 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }
                            else if (uploadGradesRequest.exam == 2)
                            {
                                exa = "Fourth Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesShes.Where(x => x.StudId == grade.stud_id && x.Grade2 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }

                            if (doneUpload == null)
                            {

                                Notification newNotif = new Notification
                                {
                                    StudId = grade.stud_id,
                                    NotifRead = 0,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Message = "Your Teacher has uploaded your " + exa + " grades for : " + schedule.EdpCode + " - " + schedule.Description,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.Notifications.Add(newNotif);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }

                        if (uploadGradesRequest.exam == 1)
                        {
                            if (GradesExist == null)
                            {
                                GradesSh gradeSh = new GradesSh
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade1 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradesShes.Add(gradeSh);
                            }
                            else
                            {
                                GradesExist.Grade1 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesShes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluation gradeEval = new GradeEvaluation
                                {
                                    StudId = grade.stud_id,
                                    IntCode = schedule.InternalCode,
                                    MidtermGrade = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.MidtermGrade = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluations.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                        else
                        {

                            if (GradesExist == null)
                            {
                                GradesSh gradeSh = new GradesSh
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade2 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.GradesShes.Add(gradeSh);
                            }
                            else
                            {
                                GradesExist.Grade2 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesShes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluation gradeEval = new GradeEvaluation
                                {
                                    StudId = grade.stud_id,
                                    IntCode = schedule.InternalCode,
                                    FinalGrade = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.FinalGrade = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluations.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }

                    }
                }
                else if (isSHS.Equals("JH") || isSHS.Equals("BE"))
                {
                    foreach (UploadGradesRequest.grades grade in uploadGradesRequest.stud_grades)
                    {
                        var GradesExist = _ucOnlinePortalContext.GradesBes.Where(x => x.StudId == grade.stud_id && x.EdpCode == uploadGradesRequest.edp_code && x.ActiveTerm == uploadGradesRequest.active_term).FirstOrDefault();
                        var GradeEvalExist = _ucOnlinePortalContext.GradeEvaluationBes.Where(x => x.StudId == grade.stud_id && x.IntCode == scheduleBe.InternalCode && x.Term == uploadGradesRequest.active_term).FirstOrDefault();

                        if (grade.grade != "")
                        {
                            String exa = "";
                            GradesBe doneUpload = null;

                            if (uploadGradesRequest.exam == 1)
                            {
                                exa = "First Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesBes.Where(x => x.StudId == grade.stud_id && x.Grade1 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }
                            else if (uploadGradesRequest.exam == 2)
                            {
                                exa = "Second Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesBes.Where(x => x.StudId == grade.stud_id && x.Grade2 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }
                            else if (uploadGradesRequest.exam == 3)
                            {
                                exa = "Third Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesBes.Where(x => x.StudId == grade.stud_id && x.Grade3 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }
                            else if (uploadGradesRequest.exam == 4)
                            {
                                exa = "Fourth Quarter";
                                doneUpload = _ucOnlinePortalContext.GradesBes.Where(x => x.StudId == grade.stud_id && x.Grade4 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }

                            if (doneUpload == null)
                            {
                                Notification newNotif = new Notification
                                {
                                    StudId = grade.stud_id,
                                    NotifRead = 0,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Message = "Your Teacher has uploaded your " + exa + " grades for : " + scheduleBe.EdpCode + " - " + scheduleBe.Description,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.Notifications.Add(newNotif);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }

                        if (uploadGradesRequest.exam == 1)
                        {
                            if (GradesExist == null)
                            {
                                GradesBe gradeBe = new GradesBe
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade1 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradesBes.Add(gradeBe);
                            }
                            else
                            {
                                GradesExist.Grade1 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesBes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluationBe gradeEval = new GradeEvaluationBe
                                {
                                    StudId = grade.stud_id,
                                    IntCode = scheduleBe.InternalCode,
                                    Grade1 = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluationBes.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.Grade1 = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluationBes.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                        else if (uploadGradesRequest.exam == 2)
                        {
                            if (GradesExist == null)
                            {
                                GradesBe gradeBe = new GradesBe
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade2 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradesBes.Add(gradeBe);
                            }
                            else
                            {
                                GradesExist.Grade2 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesBes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluationBe gradeEval = new GradeEvaluationBe
                                {
                                    StudId = grade.stud_id,
                                    IntCode = scheduleBe.InternalCode,
                                    Grade2 = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluationBes.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.Grade2 = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluationBes.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                        else if (uploadGradesRequest.exam == 3)
                        {
                            if (GradesExist == null)
                            {
                                GradesBe gradeBe = new GradesBe
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade3 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradesBes.Add(gradeBe);
                            }
                            else
                            {
                                GradesExist.Grade3 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesBes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluationBe gradeEval = new GradeEvaluationBe
                                {
                                    StudId = grade.stud_id,
                                    IntCode = scheduleBe.InternalCode,
                                    Grade3 = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluationBes.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.Grade3 = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluationBes.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                        else if (uploadGradesRequest.exam == 4)
                        {
                            if (GradesExist == null)
                            {
                                GradesBe gradeBe = new GradesBe
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade4 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradesBes.Add(gradeBe);
                            }
                            else
                            {
                                GradesExist.Grade4 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesBes.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluationBe gradeEval = new GradeEvaluationBe
                                {
                                    StudId = grade.stud_id,
                                    IntCode = scheduleBe.InternalCode,
                                    Grade4 = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluationBes.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.Grade4 = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluationBes.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    foreach (UploadGradesRequest.grades grade in uploadGradesRequest.stud_grades)
                    {
                        var GradesExist = _ucOnlinePortalContext.GradesCls.Where(x => x.StudId == grade.stud_id && x.EdpCode == uploadGradesRequest.edp_code && x.ActiveTerm == uploadGradesRequest.active_term).FirstOrDefault();
                        var GradeEvalExist = _ucOnlinePortalContext.GradeEvaluations.Where(x => x.StudId == grade.stud_id && x.IntCode == schedule.InternalCode && x.Term == uploadGradesRequest.active_term).FirstOrDefault();

                        if (grade.grade != "")
                        {
                            String exa = "";
                            GradesCl doneUpload = null;

                            if (uploadGradesRequest.exam == 1)
                            {
                                exa = "Midterm";
                                doneUpload = _ucOnlinePortalContext.GradesCls.Where(x => x.StudId == grade.stud_id && x.Grade1 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }
                            else if (uploadGradesRequest.exam == 2)
                            {
                                exa = "Final";
                                doneUpload = _ucOnlinePortalContext.GradesCls.Where(x => x.StudId == grade.stud_id && x.Grade2 == grade.grade && x.ActiveTerm == x.ActiveTerm).FirstOrDefault();
                            }

                            if (doneUpload == null)
                            {
                                Notification newNotif = new Notification
                                {
                                    StudId = grade.stud_id,
                                    NotifRead = 0,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Message = "Your Teacher has uploaded your " + exa + " grades for : " + schedule.EdpCode + " - " + schedule.Description,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.Notifications.Add(newNotif);
                                _ucOnlinePortalContext.SaveChanges();
                            }
                        }

                        if (uploadGradesRequest.exam == 1)
                        {
                            if (GradesExist == null)
                            {
                                GradesCl gradeCL = new GradesCl
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade1 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.GradesCls.Add(gradeCL);
                            }
                            else
                            {
                                GradesExist.Grade1 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesCls.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluation gradeEval = new GradeEvaluation
                                {
                                    StudId = grade.stud_id,
                                    IntCode = schedule.InternalCode,
                                    MidtermGrade = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.MidtermGrade = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluations.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                        else
                        {
                            if (GradesExist == null)
                            {
                                GradesCl gradeCL = new GradesCl
                                {
                                    StudId = grade.stud_id,
                                    EdpCode = uploadGradesRequest.edp_code,
                                    Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")),
                                    Grade2 = grade.grade,
                                    ActiveTerm = uploadGradesRequest.active_term
                                };

                                _ucOnlinePortalContext.GradesCls.Add(gradeCL);
                            }
                            else
                            {
                                GradesExist.Grade2 = grade.grade;
                                GradesExist.Dte = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                _ucOnlinePortalContext.GradesCls.Update(GradesExist);
                            }

                            if (GradeEvalExist == null)
                            {
                                GradeEvaluation gradeEval = new GradeEvaluation
                                {
                                    StudId = grade.stud_id,
                                    IntCode = schedule.InternalCode,
                                    FinalGrade = grade.grade,
                                    Term = uploadGradesRequest.active_term
                                };
                                _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                            }
                            else
                            {
                                GradeEvalExist.FinalGrade = grade.grade;
                                _ucOnlinePortalContext.GradeEvaluations.Update(GradeEvalExist);
                            }

                            _ucOnlinePortalContext.SaveChanges();
                        }
                    }
                }

                return new UploadGradesResponse { success = 1 };
            }
        }

        public UploadCoreValuesResponse UploadCoreValues(UploadCoreValuesRequest uploadCoreValuesRequest)
        {
            if (uploadCoreValuesRequest.department.Equals("SH"))
            {
                if (uploadCoreValuesRequest.exam == 1)
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreShExist = _ucOnlinePortalContext.CoreShes.Where(x => x.StudId == cores.stud_id && x.Exam == "1" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreShExist == null)
                        {
                            CoreSh coreSh = new CoreSh
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreShes.Add(coreSh);
                        }
                        else
                        {
                            coreShExist.Innovation1 = cores.innovation_1;
                            coreShExist.Innovation2 = cores.innovation_2;
                            coreShExist.Innovation3 = cores.innovation_3;
                            coreShExist.Camaraderie = cores.camaraderie;
                            coreShExist.Alignment = cores.alignment;
                            coreShExist.Respect = cores.respect;
                            coreShExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreShes.Update(coreShExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 }; ;
                }
                else
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreShExist = _ucOnlinePortalContext.CoreShes.Where(x => x.StudId == cores.stud_id && x.Exam == "2" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreShExist == null)
                        {
                            CoreSh coreSh = new CoreSh
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreShes.Add(coreSh);
                        }
                        else
                        {
                            coreShExist.Innovation1 = cores.innovation_1;
                            coreShExist.Innovation2 = cores.innovation_2;
                            coreShExist.Innovation3 = cores.innovation_3;
                            coreShExist.Camaraderie = cores.camaraderie;
                            coreShExist.Alignment = cores.alignment;
                            coreShExist.Respect = cores.respect;
                            coreShExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreShes.Update(coreShExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 }; ;

                }
            }
            else
            {
                if (uploadCoreValuesRequest.exam == 1)
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreBeExist = _ucOnlinePortalContext.CoreBes.Where(x => x.StudId == cores.stud_id && x.Exam == "1" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreBeExist == null)
                        {
                            CoreBe coreBe = new CoreBe
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreBes.Add(coreBe);
                        }
                        else
                        {
                            coreBeExist.Innovation1 = cores.innovation_1;
                            coreBeExist.Innovation2 = cores.innovation_2;
                            coreBeExist.Innovation3 = cores.innovation_3;
                            coreBeExist.Camaraderie = cores.camaraderie;
                            coreBeExist.Alignment = cores.alignment;
                            coreBeExist.Respect = cores.respect;
                            coreBeExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreBes.Update(coreBeExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 }; ;
                }
                else if (uploadCoreValuesRequest.exam == 2)
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreBeExist = _ucOnlinePortalContext.CoreBes.Where(x => x.StudId == cores.stud_id && x.Exam == "2" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreBeExist == null)
                        {
                            CoreBe coreBe = new CoreBe
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreBes.Add(coreBe);
                        }
                        else
                        {
                            coreBeExist.Innovation1 = cores.innovation_1;
                            coreBeExist.Innovation2 = cores.innovation_2;
                            coreBeExist.Innovation3 = cores.innovation_3;
                            coreBeExist.Camaraderie = cores.camaraderie;
                            coreBeExist.Alignment = cores.alignment;
                            coreBeExist.Respect = cores.respect;
                            coreBeExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreBes.Update(coreBeExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 }; ;
                }
                else if (uploadCoreValuesRequest.exam == 3)
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreBeExist = _ucOnlinePortalContext.CoreBes.Where(x => x.StudId == cores.stud_id && x.Exam == "3" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreBeExist == null)
                        {
                            CoreBe coreBe = new CoreBe
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreBes.Add(coreBe);
                        }
                        else
                        {
                            coreBeExist.Innovation1 = cores.innovation_1;
                            coreBeExist.Innovation2 = cores.innovation_2;
                            coreBeExist.Innovation3 = cores.innovation_3;
                            coreBeExist.Camaraderie = cores.camaraderie;
                            coreBeExist.Alignment = cores.alignment;
                            coreBeExist.Respect = cores.respect;
                            coreBeExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreBes.Update(coreBeExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 };
                }
                else if (uploadCoreValuesRequest.exam == 4)
                {
                    foreach (UploadCoreValuesRequest.cores cores in uploadCoreValuesRequest.stud_core)
                    {
                        var coreBeExist = _ucOnlinePortalContext.CoreBes.Where(x => x.StudId == cores.stud_id && x.Exam == "4" && x.ActiveTerm == uploadCoreValuesRequest.active_term).FirstOrDefault();

                        if (coreBeExist == null)
                        {
                            CoreBe coreBe = new CoreBe
                            {
                                StudId = cores.stud_id,
                                Innovation1 = cores.innovation_1,
                                Innovation2 = cores.innovation_2,
                                Innovation3 = cores.innovation_3,
                                Camaraderie = cores.camaraderie,
                                Alignment = cores.alignment,
                                Respect = cores.respect,
                                Excellence = cores.excellence,
                                Exam = uploadCoreValuesRequest.exam.ToString(),
                                ActiveTerm = uploadCoreValuesRequest.active_term
                            };

                            _ucOnlinePortalContext.CoreBes.Add(coreBe);
                        }
                        else
                        {
                            coreBeExist.Innovation1 = cores.innovation_1;
                            coreBeExist.Innovation2 = cores.innovation_2;
                            coreBeExist.Innovation3 = cores.innovation_3;
                            coreBeExist.Camaraderie = cores.camaraderie;
                            coreBeExist.Alignment = cores.alignment;
                            coreBeExist.Respect = cores.respect;
                            coreBeExist.Excellence = cores.excellence;

                            _ucOnlinePortalContext.CoreBes.Update(coreBeExist);
                        }

                        _ucOnlinePortalContext.SaveChanges();
                    }

                    return new UploadCoreValuesResponse { success = 1 };
                }
            }

            return new UploadCoreValuesResponse { success = 0 };
        }

        public GradeReportResponse GradeReport(GradeReportRequest gradeReportRequest)
        {
            List<GradeReportResponse.grade_report> gradeRep = new List<GradeReportResponse.grade_report>();

            var schedules = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor.Length > 0).OrderBy(x => x.Instructor).Select(x => x.Instructor).Distinct().ToList();

            if (!gradeReportRequest.id_number.Equals(""))
            {
                schedules = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor.Length > 0 && x.Instructor == gradeReportRequest.id_number).OrderBy(x => x.Instructor).Select(x => x.Instructor).Distinct().ToList();
            }

            if (schedules.Count == 0)
            {
                schedules = _ucOnlinePortalContext.SchedulesBes.Where(x => x.Instructor.Length > 0 && x.Instructor == gradeReportRequest.id_number).OrderBy(x => x.Instructor).Select(x => x.Instructor).Distinct().ToList();
            }

            List<GradeReportResponse.grade_report> reportGrades = new List<GradeReportResponse.grade_report>();

            var configDate = _ucOnlinePortalContext.Configs.Where(x => x.ActiveTerm == gradeReportRequest.active_term).FirstOrDefault();

            DateTime dateReport = new DateTime();

            if (gradeReportRequest.exam == 1 || gradeReportRequest.exam == 3)
            {
                dateReport = configDate.Grade1Due.Value;
            }
            else if (gradeReportRequest.exam == 2 || gradeReportRequest.exam == 4)
            {
                dateReport = configDate.Grade2Due.Value;
            }

            foreach (string insid in schedules)
            {
                List<string> schedulesIns = new List<string>();

                if (gradeReportRequest.department.Equals("CL") || gradeReportRequest.department.Equals("SH"))
                {
                    schedulesIns = (from Schedule in _ucOnlinePortalContext.Schedules
                                    join _courselist in _ucOnlinePortalContext.CourseLists
                                    on Schedule.CourseCode equals _courselist.CourseCode
                                    where Schedule.ActiveTerm == gradeReportRequest.active_term
                                    && _courselist.ActiveTerm == gradeReportRequest.active_term
                                    && Schedule.Instructor == insid
                                    && Schedule.SplitType != "C"
                                    select Schedule.EdpCode + " - " + Schedule.Description).ToList();
                }
                else
                {
                    schedulesIns = (from Schedule in _ucOnlinePortalContext.SchedulesBes
                                    join _courselist in _ucOnlinePortalContext.CourseLists
                                    on Schedule.CourseCode equals _courselist.CourseCode
                                    where Schedule.ActiveTerm == gradeReportRequest.active_term
                                    && _courselist.ActiveTerm == gradeReportRequest.active_term
                                    && Schedule.Instructor == insid
                                    && Schedule.SplitType != "C"
                                    select Schedule.EdpCode + " - " + Schedule.Description).ToList();
                }


                var schedulesIn = _ucOnlinePortalContext.Schedules.Where(x => x.Instructor == insid).ToList();
                var schedulesInBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.Instructor == insid).ToList();

                GradeReportResponse.subjects all_subs = new GradeReportResponse.subjects
                {
                    count = schedulesIns.Count,
                    subjs = schedulesIns
                };

                int submitted = 0;
                int late_submitted = 0;
                int not_submitted = 0;

                List<string> submittedlst = new List<string>();
                List<string> late_submittedlst = new List<string>();
                List<string> not_submittedlst = new List<string>();


                if (schedulesIn.Count > 0)
                {
                    foreach (Schedule sched in schedulesIn)
                    {
                        var dept = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == sched.CourseCode && x.ActiveTerm == gradeReportRequest.active_term).Select(x => x.Department).FirstOrDefault();

                        if (dept == "CL")
                        {
                            GradesCl submittedGrades = new GradesCl();

                            if (gradeReportRequest.exam == 1)
                            {
                                submittedGrades = _ucOnlinePortalContext.GradesCls.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null && !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade1 != "" && x.Grade1 != null)).FirstOrDefault();
                            }
                            else if (gradeReportRequest.exam == 2)
                            {
                                submittedGrades = _ucOnlinePortalContext.GradesCls.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null && !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade2 != "" && x.Grade2 != null)).FirstOrDefault();
                            }

                            if (submittedGrades != null)
                            {
                                if (submittedGrades.Dte.Date > dateReport.Date)
                                {
                                    late_submitted++;

                                    int days = (submittedGrades.Dte - dateReport.Date).Days;

                                    if (days == 1)
                                    {
                                        late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " day ");
                                    }
                                    else
                                    {
                                        late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " days ");
                                    }
                                }
                                else
                                {
                                    submitted++;
                                    submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                                }
                            }
                            else
                            {
                                not_submitted++;
                                not_submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                            }

                        }
                        else if (dept == "SH")
                        {
                            GradesSh submittedGrades = new GradesSh();

                            if (gradeReportRequest.exam == 1)
                            {
                                submittedGrades = _ucOnlinePortalContext.GradesShes.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null && !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade1 != "" && x.Grade1 != null)).FirstOrDefault();
                            }
                            else if (gradeReportRequest.exam == 2)
                            {
                                submittedGrades = _ucOnlinePortalContext.GradesShes.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null && !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade2 != "" && x.Grade2 != null)).FirstOrDefault();
                            }

                            if (submittedGrades != null)
                            {
                                if (submittedGrades.Dte.Date > dateReport.Date)
                                {
                                    late_submitted++;

                                    int days = (submittedGrades.Dte - dateReport.Date).Days;

                                    if (days == 1)
                                    {
                                        late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " day ");
                                    }
                                    else
                                    {
                                        late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " days ");
                                    }
                                }
                                else
                                {
                                    submitted++;
                                    submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                                }
                            }
                            else
                            {
                                not_submitted++;
                                not_submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                            }
                        }
                    }
                }

                if (schedulesInBe.Count > 0)
                {

                    foreach (SchedulesBe sched in schedulesInBe)
                    {
                        GradesBe submittedGrades = new GradesBe();

                        if (gradeReportRequest.exam == 3)
                        {
                            submittedGrades = _ucOnlinePortalContext.GradesBes.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null || !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade3 != "" && x.Grade3 != null)).FirstOrDefault();
                        }
                        else if (gradeReportRequest.exam == 4)
                        {
                            submittedGrades = _ucOnlinePortalContext.GradesBes.Where(x => x.EdpCode == sched.EdpCode && (x.Dte != null || !x.Dte.Equals("")) && x.ActiveTerm == gradeReportRequest.active_term && (x.Grade4 != "" && x.Grade4 != null)).FirstOrDefault();
                        }

                        if (submittedGrades != null)
                        {
                            if (submittedGrades.Dte.Date > dateReport.Date)
                            {
                                late_submitted++;

                                int days = (submittedGrades.Dte - dateReport.Date).Days;

                                if (days == 1)
                                {
                                    late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " day ");
                                }
                                else
                                {
                                    late_submittedlst.Add(sched.EdpCode + " - " + sched.Description + " - " + days + " days ");
                                }
                            }
                            else
                            {
                                submitted++;
                                submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                            }
                        }
                        else
                        {
                            not_submitted++;
                            not_submittedlst.Add(sched.EdpCode + " - " + sched.Description);
                        }
                    }
                }

                GradeReportResponse.subjects submitteGd = new GradeReportResponse.subjects
                {
                    count = submitted,
                    subjs = submittedlst
                };

                GradeReportResponse.subjects LatesubmitteGd = new GradeReportResponse.subjects
                {
                    count = late_submitted,
                    subjs = late_submittedlst
                };

                GradeReportResponse.subjects notsubmitteGd = new GradeReportResponse.subjects
                {
                    count = not_submitted,
                    subjs = not_submittedlst
                };

                var loginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == insid).FirstOrDefault();

                if (loginInfo != null)
                {
                    GradeReportResponse.grade_report gradeReport = new GradeReportResponse.grade_report
                    {
                        stud_id = insid,
                        lastname = loginInfo.LastName,
                        firstname = loginInfo.FirstName,
                        deadline = dateReport.Date.ToString(),
                        is_overdue = dateReport.Date > DateTime.Now.Date ? 0 : 1,
                        total_subjects = all_subs,
                        late_submitted = LatesubmitteGd,
                        not_submitted = notsubmitteGd,
                        submitted = submitteGd
                    };
                    gradeRep.Add(gradeReport);
                }

            }
            return new GradeReportResponse { gradeR = gradeRep };
        }

        public ViewGradesResponse ViewGrades(ViewGradesRequest viewGradesRequest)
        {
            var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == viewGradesRequest.edp_code && x.ActiveTerm == viewGradesRequest.active_term).FirstOrDefault();
            var scheduleBe = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == viewGradesRequest.edp_code && x.ActiveTerm == viewGradesRequest.active_term).FirstOrDefault();

            IQueryable<ViewGradesResponse.student_grade> grades = null;

            if (schedule != null || scheduleBe != null)
            {
                var department = "";

                if (schedule != null)
                {
                    department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == schedule.CourseCode && x.ActiveTerm == viewGradesRequest.active_term).Select(x => x.Department).FirstOrDefault();

                }

                if (scheduleBe != null)
                {
                    if (department.Equals(""))
                    {
                        department = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseCode == scheduleBe.CourseCode && x.ActiveTerm == viewGradesRequest.active_term).Select(x => x.Department).FirstOrDefault();
                    }
                }

                if (department.Equals("CL"))
                {
                    grades = (from _gradescl in _ucOnlinePortalContext.GradesCls
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on _gradescl.StudId equals _loginInfo.StudId
                              where _gradescl.EdpCode == viewGradesRequest.edp_code
                              && _gradescl.ActiveTerm == viewGradesRequest.active_term
                              select new ViewGradesResponse.student_grade
                              {
                                  id_number = _loginInfo.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  grade1 = _gradescl.Grade1,
                                  grade2 = _gradescl.Grade2
                              });
                }
                else if (department.Equals("SH"))
                {
                    grades = (from GradesSh in _ucOnlinePortalContext.GradesShes
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on GradesSh.StudId equals _loginInfo.StudId
                              where GradesSh.EdpCode == viewGradesRequest.edp_code
                              && GradesSh.ActiveTerm == viewGradesRequest.active_term
                              select new ViewGradesResponse.student_grade
                              {
                                  id_number = _loginInfo.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  grade1 = GradesSh.Grade1,
                                  grade2 = GradesSh.Grade2
                              });
                }
                else if (department.Equals("JH") || department.Equals("BE"))
                {
                    var bTerm = int.Parse(viewGradesRequest.active_term) % 10 == 2;
                    string bTermTo = "";

                    if (bTerm)
                    {
                        bTermTo = viewGradesRequest.active_term.Substring(0, 4) + "1";
                    }
                    else
                    {
                        bTermTo = viewGradesRequest.active_term.Substring(0, 4) + "2";
                    }

                    var eval = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == viewGradesRequest.edp_code && (x.ActiveTerm == viewGradesRequest.active_term || x.ActiveTerm == bTermTo)).FirstOrDefault();

                    grades = (from GradesBe in _ucOnlinePortalContext.GradeEvaluationBes
                              join _loginInfo in _ucOnlinePortalContext.LoginInfos
                              on GradesBe.StudId equals _loginInfo.StudId
                              where GradesBe.IntCode == eval.InternalCode
                              && (GradesBe.Term == viewGradesRequest.active_term || GradesBe.Term == bTermTo)
                              select new ViewGradesResponse.student_grade
                              {
                                  id_number = _loginInfo.StudId,
                                  lastname = _loginInfo.LastName,
                                  firstname = _loginInfo.FirstName,
                                  grade1 = GradesBe.Grade1,
                                  grade2 = GradesBe.Grade2,
                                  grade3 = GradesBe.Grade3,
                                  grade4 = GradesBe.Grade4
                              });
                }

                return new ViewGradesResponse { student_grades = grades.ToList() };
            }
            else
            {
                return new ViewGradesResponse { student_grades = null };
            }
        }

        public ActiveTermResponse GetActiveTerm()
        {
            var active_terms = _ucOnlinePortalContext.Configs.Select(x => x.ActiveTerms).FirstOrDefault();

            return new ActiveTermResponse { active_terms = active_terms };
        }

        public AssignSectionResponse AssignSectionAdviser(AssignSectionRequest assignSectionRequest)
        {
            var sectionExist = _ucOnlinePortalContext.AdviserSections.Where(x => x.Section == assignSectionRequest.section && x.ActiveTerm == assignSectionRequest.active_term).FirstOrDefault();

            if (sectionExist != null)
            {
                sectionExist.Instructor = assignSectionRequest.instructor_id;
                sectionExist.Department = assignSectionRequest.department;
                sectionExist.ActiveTerm = assignSectionRequest.active_term;
                sectionExist.Section = assignSectionRequest.section;

                _ucOnlinePortalContext.AdviserSections.Update(sectionExist);
            }
            else
            {
                AdviserSection adSec = new AdviserSection
                {
                    Section = assignSectionRequest.section,
                    Department = assignSectionRequest.department,
                    Instructor = assignSectionRequest.instructor_id,
                    ActiveTerm = assignSectionRequest.active_term
                };

                _ucOnlinePortalContext.AdviserSections.Add(adSec);
            }

            _ucOnlinePortalContext.SaveChanges();

            return new AssignSectionResponse { success = 1 };
        }

        public ViewAdviserResponse ViewAdviser(ViewAdviserRequest viewAdviserRequest)
        {
            if (viewAdviserRequest.section.Equals(""))
            {
                var adviser = (from AdviserSection in _ucOnlinePortalContext.AdviserSections
                               join _loginInfo in _ucOnlinePortalContext.LoginInfos
                               on AdviserSection.Instructor equals _loginInfo.StudId
                               where AdviserSection.Department == viewAdviserRequest.department
                               && AdviserSection.ActiveTerm == viewAdviserRequest.active_term
                               select new ViewAdviserResponse.Section
                               {
                                   section = AdviserSection.Section,
                                   id_number = _loginInfo.StudId,
                                   name = _loginInfo.LastName + ", " + _loginInfo.FirstName
                               });

                return new ViewAdviserResponse { sections = adviser.ToList() };
            }
            else
            {
                var adviser = (from AdviserSection in _ucOnlinePortalContext.AdviserSections
                               join _loginInfo in _ucOnlinePortalContext.LoginInfos
                               on AdviserSection.Instructor equals _loginInfo.StudId
                               where AdviserSection.Department == viewAdviserRequest.department
                               && AdviserSection.Section == viewAdviserRequest.section
                               && AdviserSection.ActiveTerm == viewAdviserRequest.active_term
                               select new ViewAdviserResponse.Section
                               {
                                   section = AdviserSection.Section,
                                   id_number = _loginInfo.StudId,
                                   name = _loginInfo.LastName + ", " + _loginInfo.FirstName
                               });

                return new ViewAdviserResponse { sections = adviser.ToList() };
            }
        }

        public TransferGradeResponse TransferGrade(TransferGradeRequest transferGradeRequest)
        {
            var ToTransfer = _ucOnlinePortalContext.GradesCls.Where(x => x.ActiveTerm == transferGradeRequest.active_term).ToList();
            var ToTransferSh = _ucOnlinePortalContext.GradesShes.Where(x => x.ActiveTerm == transferGradeRequest.active_term).ToList();
            var ToTransferBe = _ucOnlinePortalContext.GradesBes.Where(x => x.ActiveTerm == transferGradeRequest.active_term).ToList();

            foreach (GradesCl gcl in ToTransfer)
            {
                var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == gcl.EdpCode && x.ActiveTerm == transferGradeRequest.active_term).FirstOrDefault();

                if (schedule != null)
                {
                    var gradeExist = _ucOnlinePortalContext.GradeEvaluations.Where(x => x.StudId == gcl.StudId && x.IntCode == schedule.InternalCode && x.Term == transferGradeRequest.active_term).FirstOrDefault();

                    if (gradeExist == null)
                    {
                        GradeEvaluation gradeEval = new GradeEvaluation
                        {
                            StudId = gcl.StudId,
                            MidtermGrade = gcl.Grade1,
                            FinalGrade = gcl.Grade2,
                            IntCode = schedule.InternalCode,
                            Term = transferGradeRequest.active_term
                        };

                        _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                    }
                    else
                    {
                        gradeExist.MidtermGrade = gcl.Grade1;
                        gradeExist.FinalGrade = gcl.Grade2;

                        _ucOnlinePortalContext.GradeEvaluations.Update(gradeExist);
                    }

                    _ucOnlinePortalContext.SaveChanges();
                }
            }

            foreach (GradesSh gcl in ToTransferSh)
            {
                var schedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == gcl.EdpCode && x.ActiveTerm == transferGradeRequest.active_term).FirstOrDefault();

                if (schedule != null)
                {
                    var gradeExist = _ucOnlinePortalContext.GradeEvaluations.Where(x => x.StudId == gcl.StudId && x.IntCode == schedule.InternalCode && x.Term == transferGradeRequest.active_term).FirstOrDefault();

                    if (gradeExist == null)
                    {
                        GradeEvaluation gradeEval = new GradeEvaluation
                        {
                            StudId = gcl.StudId,
                            MidtermGrade = gcl.Grade1,
                            FinalGrade = gcl.Grade2,
                            IntCode = schedule.InternalCode,
                            Term = transferGradeRequest.active_term
                        };

                        _ucOnlinePortalContext.GradeEvaluations.Add(gradeEval);
                    }
                    else
                    {
                        gradeExist.MidtermGrade = gcl.Grade1;
                        gradeExist.FinalGrade = gcl.Grade2;

                        _ucOnlinePortalContext.GradeEvaluations.Update(gradeExist);
                    }

                    _ucOnlinePortalContext.SaveChanges();
                }
            }


            foreach (GradesBe gcl in ToTransferBe)
            {
                var schedule = _ucOnlinePortalContext.SchedulesBes.Where(x => x.EdpCode == gcl.EdpCode && x.ActiveTerm == transferGradeRequest.active_term).FirstOrDefault();

                if (schedule != null)
                {
                    var gradeExist = _ucOnlinePortalContext.GradeEvaluationBes.Where(x => x.StudId == gcl.StudId && x.IntCode == schedule.InternalCode && x.Term == transferGradeRequest.active_term).FirstOrDefault();

                    if (gradeExist == null)
                    {
                        GradeEvaluationBe gradeEval = new GradeEvaluationBe
                        {
                            StudId = gcl.StudId,
                            Grade1 = gcl.Grade1,
                            Grade2 = gcl.Grade2,
                            Grade3 = gcl.Grade3,
                            Grade4 = gcl.Grade4,
                            IntCode = schedule.InternalCode,
                            Term = transferGradeRequest.active_term
                        };

                        _ucOnlinePortalContext.GradeEvaluationBes.Add(gradeEval);
                    }
                    else
                    {
                        gradeExist.Grade1 = gcl.Grade1;
                        gradeExist.Grade2 = gcl.Grade2;
                        gradeExist.Grade3 = gcl.Grade3;
                        gradeExist.Grade4 = gcl.Grade4;

                        _ucOnlinePortalContext.GradeEvaluationBes.Update(gradeExist);
                    }

                    _ucOnlinePortalContext.SaveChanges();
                }
            }


            return new TransferGradeResponse { success = 1 };
        }

        public ViewTeachersPerDepartmentResponse ViewTeachersPerDept(ViewTeachersPerDepartmentRequest viewTeachersPerDepartmentRequest)
        {
            List<string> id_numbers = new List<string>();

            var courses = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == viewTeachersPerDepartmentRequest.dept & x.ActiveTerm == viewTeachersPerDepartmentRequest.active_term).Select(x => x.CourseCode).ToList();
            var dept = _ucOnlinePortalContext.CourseLists.Where(x => x.CourseDepartmentAbbr == viewTeachersPerDepartmentRequest.dept & x.ActiveTerm == viewTeachersPerDepartmentRequest.active_term).Select(x => x.Department).FirstOrDefault();

            if (dept.Equals("CL") || dept.Equals("SH"))
            {
                id_numbers = _ucOnlinePortalContext.Schedules.Where(x => courses.Contains(x.CourseCode) && x.ActiveTerm == viewTeachersPerDepartmentRequest.active_term).Select(x => x.Instructor).Distinct().ToList();
            }
            else
            {
                id_numbers = _ucOnlinePortalContext.SchedulesBes.Where(x => courses.Contains(x.CourseCode) && x.ActiveTerm == viewTeachersPerDepartmentRequest.active_term).Select(x => x.Instructor).Distinct().ToList();
            }

            var id_num = _ucOnlinePortalContext.TeacherDepts.Where(x => x.Dept == viewTeachersPerDepartmentRequest.dept).Select(x => x.IdNumber).ToList();

            id_numbers.AddRange(id_num);

            id_numbers = id_numbers.Distinct().ToList();

            return new ViewTeachersPerDepartmentResponse { id_numbers = id_numbers };
        }

        public GetCurriculumResponse GetCurriculum(GetCurriculumRequest getCurriculumRequest)  //id_number
        {
            //var getStudentInfo = _ucOnlinePortalContext._212studentInfos.Where(x => x.StudId == getCurriculumRequest.id_number).FirstOrDefault();
            //check if the the data exist
            //var need to get active term
            //var getEnrollmentTerm = _ucOnlinePortalContext.
            var getLoginInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == getCurriculumRequest.id_number).FirstOrDefault();
            if (getLoginInfo == null)
            {
                //return empty data
                return new GetCurriculumResponse { };
            }
            /* 
             if (getLoginInfo.CurrYear == null)
                 return new GetCurriculumResponse { };
            */
            var getUnits = 0;
            if (getLoginInfo.AllowedUnits != null)
            { //getStudent_ornrp.Units 0
                getUnits = (int)getLoginInfo.AllowedUnits;
            }
            var curr_year = 0;
            if (getLoginInfo.CurrYear != null) {
                curr_year = (int)getLoginInfo.CurrYear;
            }
            var subjects = (from subject in _ucOnlinePortalContext.SubjectInfos
                            where (subject.CourseCode == getLoginInfo.CourseCode && (subject.CurriculumYear == curr_year || subject.CurriculumYear == getCurriculumRequest.year)) // && subject.CurriculumYear == 
                            select new GetCurriculumResponse.Subjects
                            {
                                internal_code = subject.InternalCode,
                                subject_name = subject.SubjectName,
                                subject_type = subject.SubjectType,
                                descr_1 = subject.Descr1,
                                descr_2 = subject.Descr2,
                                units = Convert.ToString(subject.Units),
                                semester = Convert.ToString(subject.Semester),
                                year_level = Convert.ToInt32(subject.YearLevel), //added convert
                                course_code = subject.CourseCode,
                                split_code = subject.SplitCode,
                                split_type = subject.SplitType
                            }).ToList();

            var remarks = (from remark in _ucOnlinePortalContext.Requisites
                           join subject in _ucOnlinePortalContext.SubjectInfos
                           on remark.RequisiteCode equals subject.InternalCode
                           join curriculum in _ucOnlinePortalContext.Curricula
                           on subject.CurriculumYear equals curriculum.Year
                           where curriculum.IsDeployed == 1
                           select new GetCurriculumResponse.Requisites
                           {
                               internal_code = remark.InternalCode,
                               subject_code = subject.SubjectName,
                               requisites = remark.RequisiteCode,
                               requisite_type = remark.RequisiteType
                           }).ToList();

            var grades = (from subject in _ucOnlinePortalContext.SubjectInfos
                          join grade in _ucOnlinePortalContext.GradeEvaluations
                          on subject.InternalCode equals grade.IntCode
                          where grade.StudId == getCurriculumRequest.id_number
                          select new GetCurriculumResponse.Grades
                          {
                              internal_code = grade.IntCode,
                              eval_id = grade.GradeEvalId,
                              subject_code = subject.SubjectName,
                              final_grade = grade.FinalGrade
                          }).ToList();

            var schedules = (from schedule in _ucOnlinePortalContext._212schedules
                             join subject_info in _ucOnlinePortalContext.SubjectInfos
                             on schedule.InternalCode equals subject_info.InternalCode
                             join curriculum in _ucOnlinePortalContext.Curricula
                             on subject_info.CurriculumYear equals curriculum.Year
                             where curriculum.IsDeployed == 1
                             select new GetCurriculumResponse.Schedules
                             {
                                 internal_code = schedule.InternalCode,
                                 edp_code = schedule.EdpCode,
                                 subject_code = schedule.Description,
                                 subject_type = schedule.SubType,
                                 time_start = schedule.TimeStart,
                                 time_end = schedule.TimeEnd,
                                 mdn = schedule.Mdn,
                                 days = schedule.Days,
                                 split_type = schedule.SplitType,
                                 split_code = schedule.SplitCode,
                                 course_code = schedule.CourseCode,
                                 section = schedule.Section,
                                 room = schedule.Room
                             }).ToList();

            //var subjects = getStudentCourse.CourseCode;
            return new GetCurriculumResponse { success = 1, subjects = subjects, requisites = remarks, grades = grades, schedules = schedules, units = getUnits, curr_year = curr_year };
        }

        public StudentSubjectResponse RequestSubject(StudentSubjectRequest studentRequest)  //id_number
        {
            if (studentRequest == null)
            {
                return new StudentSubjectResponse { success = 0 };
            }

            var getSubjectInfo = _ucOnlinePortalContext.SubjectInfos.Where(x => x.InternalCode == studentRequest.internal_code).FirstOrDefault();

            if (getSubjectInfo == null)
            {
                return new StudentSubjectResponse { success = 0 };
            }

            var checkIfExistSubjectRequest = _ucOnlinePortalContext.RequestSchedules.Where(x => x.InternalCode == studentRequest.internal_code && x.Term == studentRequest.term).FirstOrDefault();

            if (checkIfExistSubjectRequest == null)
            {
                if (getSubjectInfo.SplitCode != null || getSubjectInfo.SplitCode != "")
                {
                    //continuation -- Lab to be re
                }

                RequestSchedule subjectRequest = new RequestSchedule
                {
                    SubjectName = getSubjectInfo.SubjectName,
                    TimeStart = studentRequest.time_start,
                    TimeEnd = studentRequest.time_end,
                    Mdn = studentRequest.mdn,
                    Days = studentRequest.days,
                    Rtype = studentRequest.rtype,
                    MTimeEnd = studentRequest.m_time_start,
                    MTimeStart = studentRequest.m_time_end,
                    Size = 0,
                    Status = 0,
                    InternalCode = studentRequest.internal_code,
                    Term = studentRequest.term
                };
                _ucOnlinePortalContext.RequestSchedules.Add(subjectRequest);
                _ucOnlinePortalContext.SaveChanges();
            }
            var updateSubjectRequest = _ucOnlinePortalContext.RequestSchedules.Where(x => x.InternalCode == studentRequest.internal_code && x.Term == studentRequest.term).FirstOrDefault();
            var checkIfExist = _ucOnlinePortalContext.StudentRequests.Where(x => x.InternalCode == studentRequest.internal_code && x.StudId == studentRequest.id_number && x.Term == studentRequest.term).FirstOrDefault();

            if (checkIfExist == null)
            {
                StudentRequest studentSubjectRequest = new StudentRequest
                {
                    StudId = studentRequest.id_number,
                    InternalCode = studentRequest.internal_code,
                    Term = studentRequest.term
                };

                var size = updateSubjectRequest.Size;
                size += 1;
                updateSubjectRequest.Size = size;
                _ucOnlinePortalContext.RequestSchedules.Update(updateSubjectRequest);
                _ucOnlinePortalContext.SaveChanges();

                _ucOnlinePortalContext.StudentRequests.Add(studentSubjectRequest);
            }

            _ucOnlinePortalContext.SaveChanges();
            return new StudentSubjectResponse { success = 1 };
        }
        public GetSubjectReqResponse GetRequestSubject(GetSubjectReqRequest getRequest)
        {
            if (getRequest == null)
            {
                return new GetSubjectReqResponse { };
            }

            var request = (from rsubjects in _ucOnlinePortalContext.RequestSchedules
                           join subject in _ucOnlinePortalContext.SubjectInfos
                           on rsubjects.InternalCode equals subject.InternalCode
                           join course in _ucOnlinePortalContext.CourseLists
                           on subject.CourseCode equals course.CourseCode
                           where (rsubjects.Term == getRequest.term)
                           select new GetSubjectReqResponse.RequestedSubject
                           {
                               subject_name = rsubjects.SubjectName,
                               desc_1 = subject.Descr1,
                               desc_2 = subject.Descr2,
                               time_start = rsubjects.TimeStart,
                               time_end = rsubjects.TimeEnd,
                               mdn = rsubjects.Mdn,
                               days = rsubjects.Days,
                               course_code = course.CourseCode,
                               course_abbr = course.CourseAbbr,
                               size = (int)rsubjects.Size,
                               rtype = Convert.ToInt32(rsubjects.Rtype), //added convert
                               m_time_start = rsubjects.MTimeStart,
                               m_time_end = rsubjects.MTimeEnd,
                               status = Convert.ToInt32(rsubjects.Status), // added convert
                               internal_code = rsubjects.InternalCode,
                               edp_code = rsubjects.EdpCode
                           }).ToList();

            return new GetSubjectReqResponse { request = request };
        }

        public GetStudentReqResponse GetStudentSubjectRequest(GetStudentReqRequest getRequest)
        {
            if (getRequest == null)
            {
                return new GetStudentReqResponse { };
            }

            var request = (from rsubjects in _ucOnlinePortalContext.RequestSchedules
                           join subject in _ucOnlinePortalContext.StudentRequests
                           on rsubjects.InternalCode equals subject.InternalCode
                           where (subject.StudId == getRequest.id_number && rsubjects.Term == getRequest.term)
                           select new GetStudentReqResponse.RequestedSubject
                           {
                               subject_name = rsubjects.SubjectName,
                               time_start = rsubjects.TimeStart,
                               time_end = rsubjects.TimeEnd,
                               mdn = rsubjects.Mdn,
                               days = rsubjects.Days,
                               rtype = Convert.ToInt32(rsubjects.Rtype), //added convert
                               m_time_start = rsubjects.MTimeStart,
                               m_time_end = rsubjects.MTimeEnd,
                               size = (int)rsubjects.Size,
                               status = Convert.ToInt32(rsubjects.Status), //added convert
                               internal_code = rsubjects.InternalCode
                           }).ToList();

            var studentInfo = _ucOnlinePortalContext.LoginInfos.Where(x => x.StudId == getRequest.id_number).FirstOrDefault();

            var filtered = (from subject in _ucOnlinePortalContext.SubjectInfos
                            where (subject.CourseCode == studentInfo.CourseCode && subject.SplitType == "S" && subject.CurriculumYear == studentInfo.CurrYear)
                            select new GetStudentReqResponse.FilteredSubject
                            {
                                subject_name = subject.SubjectName,
                                internal_code = subject.InternalCode
                            }).ToList();

            var schedulesOstp = _ucOnlinePortalContext.RequestSchedules.ToList();
            var selected = schedulesOstp.Select(x => x.InternalCode).ToList();


            return new GetStudentReqResponse { request = request, filtered = filtered };
        }

        public AddStudentReqResponse AddSubjectRequest(AddStudentReqRequest getRequest)
        {
            if (getRequest == null)
                return new AddStudentReqResponse { success = 0 };

            var updateRequestedSubject = _ucOnlinePortalContext.RequestSchedules.Where(x=> x.Term == getRequest.term && x.InternalCode == getRequest.internal_code).FirstOrDefault();

            StudentRequest studentSubjectRequest = new StudentRequest
            {
                StudId = getRequest.id_number,
                InternalCode = getRequest.internal_code,
                Term = getRequest.term
            };
            var size = updateRequestedSubject.Size;
            size += 1;
            updateRequestedSubject.Size = size;
            _ucOnlinePortalContext.RequestSchedules.Update(updateRequestedSubject);
            _ucOnlinePortalContext.StudentRequests.Add(studentSubjectRequest);
            _ucOnlinePortalContext.SaveChanges();

            return new AddStudentReqResponse { success = 1 };
        }

        public CancelSubjectReqResponse CancelSubjectRequest(CancelSubjectReqRequest getRequest)
        {
            if (getRequest == null)
                return new CancelSubjectReqResponse { success = 0 };

            var findTmpLogin = _ucOnlinePortalContext.StudentRequests.Where(x => x.StudId == getRequest.id_number && x.InternalCode == getRequest.internal_code && x.Term == getRequest.term).FirstOrDefault();
            _ucOnlinePortalContext.StudentRequests.Remove(findTmpLogin);
            var getStudentRequest = _ucOnlinePortalContext.RequestSchedules.Where(x => x.InternalCode == getRequest.internal_code && x.Term == getRequest.term).FirstOrDefault();
            var size = getStudentRequest.Size;
            size -= 1;
            getStudentRequest.Size = size;
            if (size == 0)
            {
                //var getSubjectRequest = _ucOnlinePortalContext.RequestSchedules
                _ucOnlinePortalContext.RequestSchedules.Remove(getStudentRequest);
            }
            else {
                _ucOnlinePortalContext.RequestSchedules.Update(getStudentRequest);
            }
            _ucOnlinePortalContext.SaveChanges();

            return new CancelSubjectReqResponse { success = 1 };
        }
        public GetAllCurriculumResponse GetAllCurriculum()
        {
            var year = (from curriculum in _ucOnlinePortalContext.Curricula
                        select new GetAllCurriculumResponse.SchoolYear
                        {
                            year = Convert.ToInt16(curriculum.Year), //added convert
                            isDeployed = Convert.ToInt16(curriculum.IsDeployed) //added convert
                        }).ToList();
            var courses = (from course in _ucOnlinePortalContext.CourseLists
                           select new GetAllCurriculumResponse.Courses
                           {
                               course_code = course.CourseCode,
                               course_description = course.CourseDescription,
                               course_abbr = course.CourseAbbr,
                               course_year_limit = course.CourseYearLimit,
                               course_department = course.CourseDepartment,
                               course_department_abbr = course.CourseDepartmentAbbr,
                               course_active = course.CourseActive,
                               department = course.Department,
                               enrollment_open = Convert.ToInt16(course.EnrollmentOpen) //added convert
                           }).ToList();


            var departments = (from department in _ucOnlinePortalContext.CourseLists
                               select new GetAllCurriculumResponse.Departments
                               {
                                   course_department = department.CourseDepartment,
                                   course_department_abbr = department.CourseDepartmentAbbr,
                                   department = department.Department
                               }).Distinct().ToList();

            var latest_cur = _ucOnlinePortalContext.Curricula.Max(x => x.Year);

            //var group_departments = departments
            return new GetAllCurriculumResponse { year = year, courses = courses, departments = departments, current_curriculum = Convert.ToInt16(latest_cur) }; //added convert
        }

        public GetCourseInfoResponse GetCourseInfo(GetCourseInfoRequest getRequest)
        {
            if (getRequest == null)
            {
                return new GetCourseInfoResponse { };
            }
            if (getRequest.department != null)
            {
                var filteredCourseList = (from subject in _ucOnlinePortalContext.SubjectInfos
                                          join course in _ucOnlinePortalContext.CourseLists
                                          on subject.CourseCode equals course.CourseCode
                                          where (course.CourseDepartmentAbbr == getRequest.department && subject.CurriculumYear == getRequest.curr_year)
                                          select new GetCourseInfoResponse.Courses
                                          {
                                              course_code = subject.CourseCode,
                                              course_description = course.CourseDescription,
                                              course_abbr = course.CourseAbbr,
                                              year_limit = course.CourseYearLimit,
                                              course_department = course.CourseDepartment,
                                              course_department_abbr = course.CourseDepartmentAbbr
                                          }).Distinct().ToList();

                return new GetCourseInfoResponse { courses = filteredCourseList };
            }

            var getCourseList = (from subject in _ucOnlinePortalContext.SubjectInfos
                                 join course in _ucOnlinePortalContext.CourseLists
                                 on subject.CourseCode equals course.CourseCode
                                 where (subject.CurriculumYear == getRequest.curr_year)
                                 select new GetCourseInfoResponse.Courses
                                 {
                                     course_code = subject.CourseCode,
                                     course_description = course.CourseDescription,
                                     course_abbr = course.CourseAbbr,
                                     year_limit = course.CourseYearLimit,
                                     course_department = course.CourseDepartment,
                                     course_department_abbr = course.CourseDepartmentAbbr
                                 }).Distinct().ToList();

            return new GetCourseInfoResponse { courses = getCourseList };
        }
        public static string GenerateAlphaNumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool checkInternalCode(string int_code)
        {
            bool found = false;
            var checkCurriculumIfExist = _ucOnlinePortalContext.SubjectInfos.Where(x => x.InternalCode == int_code).FirstOrDefault();
            if (checkCurriculumIfExist != null)
                found = true;
            return found;
        }
        public string GenerateInternalCode(string course)
        {
            string newCode = GenerateAlphaNumeric(6 - course.Length);
            string newInternalCode = course;

            newInternalCode += newCode;

            if (checkInternalCode(newInternalCode))
            {
                return GenerateInternalCode(course);
            }

            return newInternalCode;
        }
        public AddCurriculumResponse AddCurriculum(AddCurriculumRequest getRequest)
        {
            if (getRequest.curr_year == 0)
                return new AddCurriculumResponse { success = 0 };

            ///var checkCurriculumIfExist = _ucOnlinePortalContext.Curricula.Where(x => x.Year == getRequest.curr_year ).FirstOrDefault();
            var checkCurriculumIfExist = _ucOnlinePortalContext.Curricula.Where(x => x.Year == getRequest.curr_year).FirstOrDefault();
            if (checkCurriculumIfExist == null)
            {
                Curriculum curr = new Curriculum
                {
                    Year = Convert.ToInt16(getRequest.curr_year),
                    IsDeployed = 1
                };
                _ucOnlinePortalContext.Curricula.Add(curr);
                _ucOnlinePortalContext.SaveChanges();
                //return new AddCurriculumResponse { success = 0 };
            }

            foreach (AddCurriculumRequest.Subjects subjects in getRequest.subjects)
            {
                var checkIfExist = _ucOnlinePortalContext.SubjectInfos.Where(x => x.SubjectName == subjects.subject && x.CurriculumYear == getRequest.curr_year && x.CourseCode == getRequest.course).FirstOrDefault();
                if (checkIfExist == null)
                {
                    //Check if lab unit != 0 and save subject
                    if (subjects.lab != 0)
                    {
                        //save lab to subject_info 
                        SubjectInfo subjectInfoLab = new SubjectInfo
                        {
                            InternalCode = GenerateInternalCode(getRequest.course),
                            SubjectName = subjects.subject,
                            SubjectType = "L",
                            Descr1 = subjects.description,
                            Descr2 = null,
                            Units = Convert.ToInt16(subjects.lab),
                            Semester = Convert.ToInt16(subjects.semester),
                            CourseCode = getRequest.course,
                            YearLevel = subjects.year,
                            SplitType = "C",
                            CurriculumYear = getRequest.curr_year
                        };
                        _ucOnlinePortalContext.SubjectInfos.Add(subjectInfoLab);
                        _ucOnlinePortalContext.SaveChanges();
                    }

                    var getInternalCodeLab = _ucOnlinePortalContext.SubjectInfos.Where(x => x.CurriculumYear == getRequest.curr_year && x.SubjectName == subjects.subject && x.SplitType == "C").FirstOrDefault();
                    string internal_code = null;
                    if (getInternalCodeLab != null)
                    {
                        internal_code = getInternalCodeLab.InternalCode;
                    }
                    // save lecture to subject_info
                    SubjectInfo subjectInfo = new SubjectInfo
                    {
                        InternalCode = GenerateInternalCode(getRequest.course),
                        SubjectName = subjects.subject,
                        SubjectType = null,
                        Descr1 = subjects.description,
                        Descr2 = null,
                        Units = Convert.ToInt16(subjects.lec),
                        Semester = Convert.ToInt16(subjects.semester),
                        CourseCode = getRequest.course,
                        YearLevel = subjects.year,
                        SplitType = "S",
                        CurriculumYear = getRequest.curr_year,
                        SplitCode = internal_code
                    };

                    _ucOnlinePortalContext.SubjectInfos.Add(subjectInfo);
                    _ucOnlinePortalContext.SaveChanges();

                    if (subjects.lab != 0)
                    {
                        var getLab = _ucOnlinePortalContext.SubjectInfos.Where(x => x.SubjectName == subjects.subject && x.CurriculumYear == getRequest.curr_year && x.SubjectType == "L").FirstOrDefault();
                        var updateSubject = _ucOnlinePortalContext.SubjectInfos.Where(x => x.CurriculumYear == getRequest.curr_year && x.SubjectName == subjects.subject && x.SplitType == "S").FirstOrDefault();
                        if (getLab == null)
                        {
                            return new AddCurriculumResponse { success = 0 };
                        }
                        updateSubject.SplitCode = getLab.InternalCode;
                        getLab.SplitCode = updateSubject.InternalCode;


                        _ucOnlinePortalContext.SubjectInfos.Update(updateSubject);
                        _ucOnlinePortalContext.SaveChanges();
                    }
                }
                else
                {
                    /*if (subjects.lab != 0)
                    {
                        var updateSubject = _ucOnlinePortalContext.SubjectInfos.Where(x => x.CurriculumYear == getRequest.curr_year && x.SubjectName == subjects.subject).FirstOrDefault();
                        if(updateSubject == null)
                            return new AddCurriculumResponse { success = 0 };
                        updateSubject.Descr1 = 
                        _ucOnlinePortalContext.SubjectInfos.Update(updateSubject);
                        _ucOnlinePortalContext.SaveChanges();
                    }*/
                    return new AddCurriculumResponse { success = 0 };
                }
            }
            return new AddCurriculumResponse { success = 1 };
        }
        public CloseCurriculumReponse CloseCurriculum(CloseCurriculumRequest getRequest)
        {
            var updateCurriculum = _ucOnlinePortalContext.Curricula.Where(x => x.Year == getRequest.curr_year).FirstOrDefault();
            if (updateCurriculum == null)
            {
                return new CloseCurriculumReponse { success = 0 };
            }
            updateCurriculum.IsDeployed = 0;
            _ucOnlinePortalContext.Curricula.Update(updateCurriculum);
            _ucOnlinePortalContext.SaveChanges();

            return new CloseCurriculumReponse { success = 1 };
        }


        public GetSubjectInfoResponse GetSubjectInfo(GetSubjectInfoRequest getRequest)
        {
            var subjects = (from subject in _ucOnlinePortalContext.SubjectInfos
                            join curriculum in _ucOnlinePortalContext.Curricula
                            on subject.CurriculumYear equals curriculum.Year
                            where (curriculum.IsDeployed == 1 && subject.CurriculumYear == getRequest.curr_year && getRequest.course_code == subject.CourseCode) // && subject.CurriculumYear == 
                            select new GetSubjectInfoResponse.Subjects
                            {
                                internal_code = subject.InternalCode,
                                subject_name = subject.SubjectName,
                                subject_type = subject.SubjectType,
                                descr_1 = subject.Descr1,
                                descr_2 = subject.Descr2,
                                units = Convert.ToString(subject.Units),
                                semester = Convert.ToString(subject.Semester),
                                year_level = Convert.ToInt32(subject.YearLevel), //added convert
                                course_code = subject.CourseCode,
                                split_code = subject.SplitCode,
                                split_type = subject.SplitType
                            }).ToList();

            var remarks = (from remark in _ucOnlinePortalContext.Requisites
                           join subject in _ucOnlinePortalContext.SubjectInfos
                           on remark.RequisiteCode equals subject.InternalCode
                           join curriculum in _ucOnlinePortalContext.Curricula
                           on subject.CurriculumYear equals curriculum.Year
                           where (curriculum.IsDeployed == 1)
                           select new GetSubjectInfoResponse.Requisites
                           {
                               internal_code = remark.InternalCode,
                               subject_code = subject.SubjectName,
                               requisites = remark.RequisiteCode,
                               requisite_type = remark.RequisiteType
                           }).ToList();

            return new GetSubjectInfoResponse { subjects = subjects, requisites = remarks };
        }

        public RemoveRequisiteResponse RemovePrerequisite(RemoveRequisiteRequest getRequest)
        {
            var checkIfExist = _ucOnlinePortalContext.Requisites.Where(x => x.InternalCode == getRequest.internal_code && x.RequisiteCode == getRequest.requisite && x.RequisiteType == getRequest.requisite_type).FirstOrDefault();
            if (checkIfExist == null)
            {
                return new RemoveRequisiteResponse { success = 0 };
            }
            _ucOnlinePortalContext.Requisites.Remove(checkIfExist);
            _ucOnlinePortalContext.SaveChanges();

            return new RemoveRequisiteResponse { success = 1 };
        }
        public SaveRequisiteResponse SavePrerequisite(SaveRequisiteRequest savePrerequisiteRequest)
        {
            if (savePrerequisiteRequest.internal_code == null || savePrerequisiteRequest.requisite == null)
            {
                return new SaveRequisiteResponse { success = 0 };
            }
            Requisite requisite = new Requisite
            {
                InternalCode = savePrerequisiteRequest.internal_code,
                RequisiteCode = savePrerequisiteRequest.requisite,
                RequisiteType = savePrerequisiteRequest.requisite_type
            };
            _ucOnlinePortalContext.Requisites.Add(requisite);
            _ucOnlinePortalContext.SaveChanges();
            return new SaveRequisiteResponse { success = 1 };
        }

        public GetEquivalenceResponse GetEquivalence(GetEquivalenceRequest getEquivalenceRequest)
        {
            if (getEquivalenceRequest.description == null)
            {
                return new GetEquivalenceResponse { };
            }

            var equivalences = (from subjectInfo in _ucOnlinePortalContext.SubjectInfos
                                join curr in _ucOnlinePortalContext.Curricula
                                on subjectInfo.CurriculumYear equals curr.Year
                                join course in _ucOnlinePortalContext.CourseLists
                                on subjectInfo.CourseCode equals course.CourseCode
                                where (curr.IsDeployed == 1 && (subjectInfo.Descr1.Contains(getEquivalenceRequest.description) || subjectInfo.Descr2.Contains(getEquivalenceRequest.description)))
                                select new GetEquivalenceResponse.Equivalence
                                {
                                    internal_code = subjectInfo.InternalCode,
                                    subject = subjectInfo.SubjectName,
                                    descr_1 = subjectInfo.Descr1,
                                    desc_2 = subjectInfo.Descr2,
                                    subject_type = subjectInfo.SubjectType,
                                    units = subjectInfo.Units,
                                    split_code = subjectInfo.SplitCode,
                                    split_type = subjectInfo.SplitType,
                                    semester = (short)subjectInfo.Semester,
                                    year = (short)subjectInfo.YearLevel,
                                    curr_year = (int)subjectInfo.CurriculumYear,
                                    course = course.CourseAbbr
                                }).ToList();

            //var temp = equivalences.

            return new GetEquivalenceResponse { equivalences = equivalences };
        }
        public SearchSubjectResponse SearchSubject(SearchSubjectRequest searchSubjectRequest)
        {
            if (searchSubjectRequest == null)
            {
                return new SearchSubjectResponse { };
            }
            var subjects = (from subjectInfo in _ucOnlinePortalContext.SubjectInfos
                            join curr in _ucOnlinePortalContext.Curricula
                            on subjectInfo.CurriculumYear equals curr.Year
                            join course in _ucOnlinePortalContext.CourseLists
                            on subjectInfo.CourseCode equals course.CourseCode
                            where (curr.IsDeployed == 1 && (subjectInfo.SubjectName.Contains(searchSubjectRequest.subject_name)))
                            select new SearchSubjectResponse.Subjects
                            {
                                internal_code = subjectInfo.InternalCode,
                                subject = subjectInfo.SubjectName,
                                descr_1 = subjectInfo.Descr1,
                                desc_2 = subjectInfo.Descr2,
                                subject_type = subjectInfo.SubjectType,
                                units = subjectInfo.Units,
                                split_code = subjectInfo.SplitCode,
                                split_type = subjectInfo.SplitType,
                                semester = (short)subjectInfo.Semester,
                                year = (short)subjectInfo.YearLevel,
                                curr_year = (int)subjectInfo.CurriculumYear,
                                course = course.CourseAbbr
                            }).ToList();
            return new SearchSubjectResponse { subjects = subjects };
        }

        public AddEquivalenceResponse AddEquivalence(AddEquivalenceRequest addEquivalenceRequest)
        {
            if (addEquivalenceRequest == null)
                return new AddEquivalenceResponse { success = 0 };

            Equivalence equival = new Equivalence
            {
                InternalCode = addEquivalenceRequest.internal_code,
                EquivalCode = addEquivalenceRequest.equivalence_code
            };
            _ucOnlinePortalContext.Equivalences.Add(equival);
            _ucOnlinePortalContext.SaveChanges();

            return new AddEquivalenceResponse { success = 1 };
        }
        public RemoveEquivalenceResponse RemoveEquivalence(RemoveEquivalenceRequest removeEquivalenceRequest)
        {
            var checkIfExist = _ucOnlinePortalContext.Equivalences.Where(x => x.InternalCode == removeEquivalenceRequest.internal_code && x.InternalCode == removeEquivalenceRequest.internal_code && x.EquivalCode == removeEquivalenceRequest.equivalence_code).FirstOrDefault();
            if (checkIfExist == null)
            {
                return new RemoveEquivalenceResponse { success = 0 };
            }
            _ucOnlinePortalContext.Equivalences.Remove(checkIfExist);
            _ucOnlinePortalContext.SaveChanges();

            return new RemoveEquivalenceResponse { success = 1 };
        }

        public GetSubjectEquivalenceResponse GetSubjectEquivalence(GetSubjectEquivalenceRequest equivalenceRequest)
        {
            if (equivalenceRequest == null)
                return new GetSubjectEquivalenceResponse { };

            var subjects = (from subject in _ucOnlinePortalContext.SubjectInfos
                            join equival in _ucOnlinePortalContext.Equivalences
                            on subject.InternalCode equals equival.EquivalCode
                            where equival.InternalCode == equivalenceRequest.internal_code
                            select new GetSubjectEquivalenceResponse.Subjects
                            {
                                internal_code = subject.InternalCode,
                                subject_name = subject.SubjectName,
                                descr_1 = subject.Descr1,
                                descr_2 = subject.Descr2,
                                units = subject.Units,
                                year = (int)subject.YearLevel,
                                semester = (int)subject.Semester,
                                curr_year = (int)subject.CurriculumYear,
                                subject_type = subject.SubjectType,
                                split_code = subject.SplitCode,
                                split_type = subject.SplitType
                            }).ToList();


            return new GetSubjectEquivalenceResponse { subjects = subjects };
        }


        public ViewStudentDeanEvaluationResponse ViewStudentEvaluation(ViewStudentDeanEvaluationRequest viewStudentDeanEvaluationRequest)
        {
            //settings for pagination
            int take = (int)viewStudentDeanEvaluationRequest.limit;
            int skip = (int)viewStudentDeanEvaluationRequest.limit * ((int)viewStudentDeanEvaluationRequest.page - 1);

            //populate initial response object
            var result = (from Oenrp in _ucOnlinePortalContext.Oenrps
                          join _loginInfo in _ucOnlinePortalContext.LoginInfos
                          on Oenrp.StudId equals _loginInfo.StudId
                          join _courseList in _ucOnlinePortalContext.CourseLists
                          on Oenrp.CourseCode equals _courseList.CourseCode
                          where Oenrp.ActiveTerm == viewStudentDeanEvaluationRequest.active_term && _courseList.ActiveTerm == viewStudentDeanEvaluationRequest.active_term
                          join _attach in _ucOnlinePortalContext.Attachments
                          on Oenrp.StudId equals _attach.StudId into gattach
                          from _attach in gattach.DefaultIfEmpty()
                          where Oenrp.Status == 11
                          select new ViewStudentDeanEvaluationResponse.Student
                          {
                              id_number = Oenrp.StudId,
                              lastname = _loginInfo.LastName,
                              firstname = _loginInfo.FirstName,
                              mi = _loginInfo.Mi,
                              suffix = _loginInfo.Suffix,
                              classification = Utils.Function.getClassification(Oenrp.Classification),
                              classification_abbr = Oenrp.Classification,
                              course_year = _courseList.CourseAbbr + " " + Oenrp.YearLevel,
                              course_code = Oenrp.CourseCode,
                              status = Oenrp.Status,
                              year_level = Oenrp.YearLevel,
                              submitted_on = Oenrp.SubmittedOn,
                              registered_on = Oenrp.RegisteredOn,
                              approved_reg_registrar = Oenrp.ApprovedRegRegistrar,
                              approved_reg_registrar_on = Oenrp.ApprovedRegRegistrarOn,
                              disapproved_reg_registrar = Oenrp.DisapprovedRegRegistrar,
                              disaproved_reg_registrar_on = Oenrp.DisapprovedRegRegistrarOn,
                              approved_dean_reg = Oenrp.ApprovedRegDean,
                              approved_dean_reg_on = Oenrp.ApprovedRegDeanOn,
                              disapproved_reg_dean = Oenrp.DisapprovedRegDean,
                              disapproved_reg_dean_on = Oenrp.DisapprovedRegDeanOn,
                              approved_dean = Oenrp.ApprovedDean,
                              approved_dean_on = Oenrp.ApprovedDeanOn,
                              disapproved_dean = Oenrp.DisapprovedDean,
                              disapproved_dean_on = Oenrp.DisapprovedDeanOn,
                              approved_accounting = Oenrp.ApprovedAcctg,
                              approved_accounting_on = Oenrp.ApprovedAcctgOn,
                              approved_cashier = Oenrp.ApprovedCashier,
                              approved_cashier_on = Oenrp.ApprovedCashierOn,
                              request_deblock = (short)Oenrp.RequestDeblock,
                              request_overload = (short)Oenrp.RequestOverload,
                              needed_payment = (int)Oenrp.NeededPayment,
                              promi_pay = (int)Oenrp.PromiPay,
                              has_payment = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == Oenrp.StudId && x.Type.Equals("Payment") && x.ActiveTerm == viewStudentDeanEvaluationRequest.active_term).Take(1).Count(),
                              has_promissory = _ucOnlinePortalContext.Oenrps.Where(x => x.StudId == Oenrp.StudId && x.RequestPromissory == 3 && x.ActiveTerm == viewStudentDeanEvaluationRequest.active_term).Count(),
                              profile = _ucOnlinePortalContext.Attachments.Where(x => x.StudId == Oenrp.StudId && x.Type == "2x2 ID Picture" && x.ActiveTerm == viewStudentDeanEvaluationRequest.active_term).Select(x => x.Filename).FirstOrDefault(),
                              enrollmentDate = Oenrp.EnrollmentDate
                          });

            var count = result.Count();

            if (viewStudentDeanEvaluationRequest.page != 0 && viewStudentDeanEvaluationRequest.limit != 0)
            {
                result = result.OrderBy(x => x.id_number).Skip(skip).Take(take);
            }

            return new ViewStudentDeanEvaluationResponse { students = result.ToList(), count = count };
        }

        public GetStudentListResponse GetStudentList (GetStudentListRequest getRequest)
        {
            var getStudentList = (from studentrequest in _ucOnlinePortalContext.StudentRequests
                                  join logininfo in _ucOnlinePortalContext.LoginInfos
                                  on studentrequest.StudId equals logininfo.StudId
                                  join courselist in _ucOnlinePortalContext.CourseLists
                                  on logininfo.CourseCode equals courselist.CourseCode
                                  where (studentrequest.InternalCode == getRequest.internal_code && studentrequest.Term == getRequest.term)
                                  select new GetStudentListResponse.Students
                                  { 
                                    id_number = studentrequest.StudId,
                                    firstname = logininfo.FirstName,
                                    lastname = logininfo.LastName,
                                    course = courselist.CourseAbbr,
                                    year = (int)logininfo.Year,
                                    contact = logininfo.MobileNumber,
                                    fb = logininfo.Facebook,
                                    email = logininfo.Email
                                  }).ToList();

            return new GetStudentListResponse { students = getStudentList};
        }

        public static string GenerateNumeric()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool checkEDPCode(string edp_code)
        {
            bool found = false;
            var check212schedule = _ucOnlinePortalContext._212schedules.Where(x => x.EdpCode == edp_code).FirstOrDefault();
            var checkschedule = _ucOnlinePortalContext.Schedules.Where(x => x.EdpCode == edp_code).FirstOrDefault();

            if (check212schedule != null && checkschedule != null)
                found = true;

            return found;
        }
        public string GenerateEDPCode()
        {
            string newCode = GenerateNumeric();

            if (checkEDPCode(newCode))
            {
                return GenerateEDPCode();
            }

            return newCode;
        }

        public UpdateRequestScheduleStatusResponse UpdateRequestStatus (UpdateRequestScheduleStatusRequest getRequest)
        {
            if (getRequest == null)
                return new UpdateRequestScheduleStatusResponse { success = 0 };
            var updateRequest = _ucOnlinePortalContext.RequestSchedules.Where(x => x.InternalCode == getRequest.internal_code && x.Term == getRequest.term).FirstOrDefault();
            var status = updateRequest.Status;
            status += 1;

            var edpcode = GenerateEDPCode();

            if (status == 3)
            {
                var getSubjectInfo = _ucOnlinePortalContext.SubjectInfos.Where(x => x.InternalCode == getRequest.internal_code).FirstOrDefault();
                Schedule sched = new Schedule
                {
                    EdpCode = edpcode,
                    Description = getSubjectInfo.SubjectName,
                    InternalCode = getRequest.internal_code,
                    SubType = getSubjectInfo.SubjectType,
                    Units = getSubjectInfo.Units,
                    TimeStart = updateRequest.TimeStart,
                    TimeEnd = updateRequest.TimeEnd,
                    Mdn = updateRequest.Mdn,
                    Days = updateRequest.Days,
                    MTimeStart = updateRequest.MTimeStart,
                    MTimeEnd = updateRequest.MTimeEnd,
                    MDays = "XX",
                    Size = 0,
                    PendingEnrolled = 0,
                    OfficialEnrolled = 0,
                    MaxSize = 20,
                    Instructor = "",
                    CourseCode = getSubjectInfo.CourseCode,
                    Section = "",
                    Room = "ONLINE",
                    Instructor2 = "",
                    Deployed = 1,
                    Status = 1,
                    SplitType = "",
                    SplitCode = "",
                    //IsGened = 0,
                    ActiveTerm = getRequest.term
                };
                _ucOnlinePortalContext.Schedules.Add(sched);

            }
            //Insert to table
            updateRequest.Status = status;
            updateRequest.EdpCode = edpcode;
            _ucOnlinePortalContext.RequestSchedules.Update(updateRequest);
            _ucOnlinePortalContext.SaveChanges();
            return new UpdateRequestScheduleStatusResponse { success = 1 };
        }
        

    }    
}
