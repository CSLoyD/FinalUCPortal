using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UCPortal.BusinessLogic.Enrollment;
using UCPortal.RequestResponse.Request;
using UCPortal.RequestResponse.Response;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using UCPortal.Authenticator;

namespace UCPortal.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("enrollment")]
    public class EnrollmentController : ControllerBase
    {
        private IEnrollmentManagement _enrollmentManagement;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public EnrollmentController(IEnrollmentManagement enrollmentManagement, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _enrollmentManagement = enrollmentManagement;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /*
         * Endpoint for Registration of new student
         * 
         */
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegistrationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new RegistrationResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RegistrationRequest>(serialized_req);

            //await result from function RegisterStudent
            var result = await Task.FromResult(_enrollmentManagement.RegisterStudent(converted_req));

            //return login reponse
            return Ok(new RegistrationResponse { success = result.success });
        }     

        /*
        * Endpoint for getting the Department
        * 
        */
        [HttpPost]
        [Route("department")]
        public async Task<IActionResult> GetDepartment(GetDepartmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetDepartmentResponse { departments = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetDepartmentRequest>(serialized_req);

            //await result from function SaveEnrollmentData
            var result = await Task.FromResult(_enrollmentManagement.GetDepartment(converted_req));

            var response = result.departments.Select(x =>
            {
                var rsRes = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var rRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetDepartmentResponse.department>(rsRes);
                return rRes;
            }).ToList();

            //return login reponse
            return Ok(new GetDepartmentResponse { departments = response });
        }


        /*
        * Endpoint for getting the Department
        * 
        */
        [HttpPost]
        [Route("course")]
        public async Task<IActionResult> GetCourses([FromBody] GetCoursesRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetCoursesResponse { colleges = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCoursesRequest>(serialized_req);

            //await result from function SaveEnrollmentData
            var result = await Task.FromResult(_enrollmentManagement.GetCourses(converted_req));

            //convert DTO to response
            var response = result.colleges.Select(x =>
            {
                var rsRes = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var rRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCoursesResponse.college>(rsRes);
                return rRes;
            }).ToList();

            //return login reponse
            return Ok(new GetCoursesResponse { colleges = response });
        }


        /*
        * Endpoint for getting the Department
        * 
        */
        [HttpPost]
        [Route("course/open")]
        public async Task<IActionResult> GetCoursesOpen([FromBody] GetCoursesOpenRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetCoursesOpenResponse { colleges = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCoursesOpenRequest>(serialized_req);

            //await result from function SaveEnrollmentData
            var result = await Task.FromResult(_enrollmentManagement.GetCoursesOpen(converted_req));

            //convert DTO to response
            var response = result.colleges.Select(x =>
            {
                var rsRes = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var rRes = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCoursesOpenResponse.college>(rsRes);
                return rRes;
            }).ToList();

            //return login reponse
            return Ok(new GetCoursesOpenResponse { colleges = response });
        }

        /*
        * Endpoint for Registration of new student
        * 
        */
        [HttpPost]
        [Route("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] SaveEnrollmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new RegistrationResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SaveEnrollmentRequest>(serialized_req);

            //await result from function SaveEnrollmentData
            var result = await Task.FromResult(_enrollmentManagement.SaveEnrollmentData(converted_req));

            //return login reponse
            return Ok(new SaveEnrollmentResponse { success = result.success });
        }

        /*
        * Endpoint for saving payments
        * 
       */
        [HttpPost]
        [Route("enroll/payments")]
        public async Task<IActionResult> SavePayments(SavePaymentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SavePaymentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.SavePayments(converted_req));

            return Ok(new SaveEnrollmentResponse { success = result.succcess });
        }

        /*
        * Endpoint for getting schedule
        * 
        */
        [HttpPost]
        [Route("schedule")]
        public async Task<IActionResult> ViewSchedule([FromBody] ViewScheduleRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ViewScheduleResponse { schedules = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewScheduleRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ViewSchedules(converted_req));

            //convert DTO to response
            var response = result.schedules.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewScheduleResponse.schedule>(rSched);
                return cSched;
            }).ToList();

            return Ok(new ViewScheduleResponse { schedules = response.OrderBy(x => x.section).ThenBy(x => x.course_code).ToList(), count = result.count });
        }

        /*
        * Endpoint for getting schedule
        * 
        */
        [HttpPost]
        [Route("schedule/subject")]
        public async Task<IActionResult> ViewSubject([FromBody] SelectSubjectInfoRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SelectSubjectInfoRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SelectSubject(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<SelectSubjectInfoResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
        * Endpoint for getting schedule
        * 
        */
        [HttpPost]
        [Route("schedule/subject/update")]
        public async Task<IActionResult> UpdateSchedules([FromBody] UpdateSubjectRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UpdateSubjectResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateSubjectRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.UpdateSubject(converted_req));           

            return Ok(new UpdateSubjectResponse { success = result.success});
        }

        /*
       * Endpoint for getting the active sections per course
       * 
       */
        [HttpPost]
        [Route("schedule/section")]
        public async Task<IActionResult> GetActiveSection([FromBody] GetActiveSectionsRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetActiveSectionsResponse { sections = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetActiveSectionsRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetActiveSections(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetActiveSectionsResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("schedule/classlist")]
        public async Task<IActionResult> ViewClasslist(ViewClassListRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewClasslistRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewClasslist(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewClasslistResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
        * Endpoint for viewing classlist
        * 
        */
        [HttpPost]
        [Route("schedule/classlist/section")]
        public async Task<IActionResult> ViewClasslistPerSection(ViewClasslistPerSectionRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewClasslistPerSectionRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewClasslistPerSection(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewClasslistPerSectionResponse>(serialized_result);

            return Ok(converted_result);
        }

        [HttpPost]
        [Route("schedule/section/all")]
        public async Task<IActionResult> GetSections([FromBody] GetSectionRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetSectionResponse { section = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetSectionRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.GetSection(converted_req));

            //convert DTO to response
            var response = result.section.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSectionResponse.sections>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetSectionResponse { section = response });
        }

        [HttpPost]
        [Route("schedule/section/status")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeSchedStatusRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ChangeSchedStatusResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ChangeSchedStatusRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.ChangeSchedStatus(converted_req));

            return Ok(new ChangeSchedStatusResponse { success = result.success });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("student/studyload")]
        public async Task<IActionResult> ViewStudyLoad([FromBody] GetStudyLoadRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudyLoadResponse { schedules = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudyLoadRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudyLoad(converted_req));

            //convert DTO to response
            var response = result.schedules.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudyLoadResponse.Schedules>(rSched);
                return cSched;
            }).OrderBy(x => x.subject_name).ToList();

            return Ok(new GetStudyLoadResponse { schedules = response, has_nstp = result.has_nstp, has_pe = result.has_pe });
        }


        /*
        * Endpoint for getting the student status
        * 
        */
        [HttpPost]
        [Route("student/status")]
        public async Task<IActionResult> GetStudentStatus([FromBody] GetStudentStatusRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudentStatusResponse { status = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentStatusRequest>(serialized_req);
            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudentStatus(converted_req));

            //convert DTO to response
            var response = result.status.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentStatusResponse.Status>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetStudentStatusResponse { status = response, classification = result.classification, is_cancelled = result.is_cancelled, needed_payment = result.needed_payment, pending_promissory = result.pending_promissory, promi_pay = result.promi_pay,  adjustment_open = result.adjustment_open, enrollment_open = result.enrollment_open, has_adjustment = result.has_adjustment});
        }

        /*
        * Endpoint for getting the student status
        * 
        */
        [HttpPost]
        [Route("student/promissory/view")]
        public async Task<IActionResult> ViewPromissoryList([FromBody] GetPromissoryListRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetPromissoryListResponse { students = null, count = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetPromissoryListRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetPromissoryList(converted_req));

            //convert DTO to response
            var response = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPromissoryListResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetPromissoryListResponse { students = response, count = result.count });
        }

        /*
        * Endpoint for getting the student status
        * 
        */
        [HttpPost]
        [Route("student/promissory/exam")]
        public async Task<IActionResult> ViewExamPromissoryList([FromBody] GetExamPromisorryListRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetExamPromissoryListResponse { students = null, count = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetExamPromisorryListRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetExamPromisorryList(converted_req));

            //convert DTO to response
            var response = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetExamPromissoryListResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetExamPromissoryListResponse { students = response, count = result.count });
        }

        /*
        * Endpoint for getting all the student per status
        * 
        */
        [HttpPost]
        [Route("student/status/all")]
        public async Task<IActionResult> ViewStudentPerStatus([FromBody] ViewStudentPerStatusRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ViewStudentPerStatusResponse { students = null });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewStudentPerStatusRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ViewStudentStatus(converted_req));

            //convert DTO to response

            List<ViewStudentPerStatusResponse.Student> final = new List<ViewStudentPerStatusResponse.Student>();

            foreach (DTO.Response.ViewStudentPerStatusResponse.Student stud in result.students.ToList())
            {
                String dateF = String.Empty;

                if (stud.status.ToString().Equals("0") || stud.status.ToString().Equals("1") || stud.status.ToString().Equals("2") || stud.status.ToString().Equals("3") || stud.status.ToString().Equals("4") || stud.status.ToString().Equals("5") || stud.status.ToString().Equals("14"))
                {
                    dateF = stud.registered_on.Value.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    dateF = stud.enrollmentDate.Value.ToString("yyyy/MM/dd HH:mm:ss");
                }

                ViewStudentPerStatusResponse.Student newStudent = new ViewStudentPerStatusResponse.Student
                {
                    classification = stud.classification,
                    course_code = stud.course_code,
                    course_year = stud.course_year,
                    firstname = stud.firstname,
                    lastname = stud.lastname,
                    id_number = stud.id_number,
                    status = stud.status,
                    suffix = stud.suffix,
                    mi = stud.mi,
                    date = dateF,
                    request_deblock = stud.request_deblock,
                    request_overload = stud.request_overload,
                    has_payment = stud.has_payment,
                    profile = stud.profile,
                    needed_payment = stud.needed_payment.ToString(),
                    has_promissory = stud.has_promissory,
                    promi_pay = stud.promi_pay
                };

                final.Add(newStudent);
            }

            return Ok(new ViewStudentPerStatusResponse { students = final.OrderBy(x => DateTime.Parse(x.date)).ToList(), count = result.count });
        }

        /*
        * Endpoint for the registration info
        * 
        */
        [HttpPost]
        [Route("student/registration")]
        public async Task<IActionResult> ViewStudentRegistraton([FromBody] ViewStudentRegistrationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewStudentRegistrationRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ViewRegistration(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewStudentRegistrationResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
        * Endpoint for the registration info
        * 
        */
        [HttpPost]
        [Route("student/info")]
        public async Task<IActionResult> ViewOldStudentInfo([FromBody] ViewOldStudentInfoRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewOldStudentInfoRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ViewOldStudentInfo(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewOldStudentInfoResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
        * Endpoint for updating the status
        * 
        */
        [HttpPost]
        [Route("student/status/update")]
        public async Task<IActionResult> SetApproveOrDissaprove([FromBody] SetApproveOrDisapprovedRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new SetApproveOrDisapprovedResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SetApproveOrDisapprovedRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SetApproveOrDisapprove(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<SetApproveOrDisapprovedResponse>(serialized_result);

            return Ok(converted_result);
        }


        /*
        * Endpoint for uploading images
        * 
        */
        [HttpPost]
        [Route("student/image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UploadImageResponse { success = 0 });
            }

            string path = "D:\\Upload";
            foreach (var formFile in request.formFiles)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine( /*Environment.CurrentDirectory*/path, "storage", formFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new UploadImageResponse { success = 1, count = request.formFiles.Count });
        }

        /*
        * Endpoint for getting the request
        * 
        */
        [HttpPost]
        [Route("student/status/request")]
        public async Task<IActionResult> GetStudentRequest([FromBody] StudentReqRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new StudentReqResponse { students = null, count = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.StudentReqRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.GetStudentRequest(converted_req));

            //convert DTO to response
            var response = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentReqResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new StudentReqResponse { students = response, count = result.count });
        }


        /*
       * Endpoint for applying student request
       * 
       */
        [HttpPost]
        [Route("student/request")]
        public async Task<IActionResult> ApplyStudentRequest([FromBody] ApplyReqRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ApplyReqResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ApplyReqRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.ApplyRequest(converted_req));

            return Ok(new ApplyReqResponse { success = result.success });
        }

        /*
      * Endpoint for applying student request
      * 
      */
        [HttpPost]
        [Route("student/request/approve")]
        public async Task<IActionResult> ApproveStudentRequest([FromBody] ApproveReqRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ApproveReqResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ApproveReqRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.ApproveRequest(converted_req));

            return Ok(new ApproveReqResponse { success = result.success });
        }

        /*
      * Endpoint for applying student request
      * 
      */
        [HttpPost]
        [Route("student/request/promissory")]
        public async Task<IActionResult> RequestPromissory([FromBody] RequestPromissoryRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new RequestPromissoryResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RequestPromissoryRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.RequestPromissory(converted_req));

            return Ok(new RequestPromissoryResponse { success = result.success });
        }

        /*
     * Endpoint for applying student request
     * 
     */
        [HttpPost]
        [Route("student/request/exampromissory")]
        public async Task<IActionResult> RequestExamPromissory([FromBody] RequestExamPromiRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new RequestExamPromiResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RequestExamPromiRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.RequestExamPromi(converted_req));

            return Ok(new RequestExamPromiResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/cancel")]
        public async Task<IActionResult> CancelEnrollment([FromBody] CancelEnrollmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new CancelEnrollmentResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CancelEnrollmentRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.CancelEnrollment(converted_req));

            return Ok(new CancelEnrollmentResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/update")]
        public async Task<IActionResult> UpdateLoginInfo([FromBody] UpdateStudentInfoRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UpdateStudentInfoResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateStudentInfoRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.UpdateStudentInfo(converted_req));

            return Ok(new CancelEnrollmentResponse { success = result.success });
        }


        [HttpPost]
        [Route("student/evaluation")]
        public async Task<IActionResult> ViewStudentEvaluation([FromBody] ViewStudentEvaluationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ViewStudentEvaluationResponse { studentGrades = null});
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewStudentEvaluationRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.ViewEvaluation(converted_req));

            var response = result.studentGrades.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewStudentEvaluationResponse.Grades>(rSched);
                return cSched;
            }).ToList();

            return Ok(new ViewStudentEvaluationResponse { studentGrades = response });
        }

        [HttpPost]
        [Route("student/evaluation/be")]
        public async Task<IActionResult> ViewStudentEvaluationBE([FromBody] ViewStudentEvaluationBERequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ViewStudentEvaluationBEResponse { studentGrades = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewStudentEvaluationBERequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.ViewEvaluationBE(converted_req));

            var response = result.studentGrades.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewStudentEvaluationBEResponse.Grades>(rSched);
                return cSched;
            }).ToList();

            return Ok(new ViewStudentEvaluationBEResponse { studentGrades = response });
        }

        [HttpPost]
        [Route("adjustment")]
        public async Task<IActionResult> SaveAdjustment([FromBody] SaveAdjustmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new SaveAdjustmentResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SaveAdjustmentRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.SaveAdjustment(converted_req));

            return Ok(new SaveAdjustmentResponse { success = result.success });
        }

        [HttpPost]
        [Route("adjustment/details")]
        public async Task<IActionResult> GetAdjustmentDetails([FromBody] GetAdjustmentDetailRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetAdjustmentDetailRequest>(serialized_req);

            //await result from function GetStudentRequest
            var result = await Task.FromResult(_enrollmentManagement.GetAdjustmentDetail(converted_req));

            var response_add = result.added.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAdjustmentDetailResponse.Schedules>(rSched);
                return cSched;
            }).ToList();

            var response_remove = result.removed.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAdjustmentDetailResponse.Schedules>(rSched);
                return cSched;
            }).ToList();
            return Ok(new GetAdjustmentDetailResponse { added = response_add, removed = response_remove });
        }

        /*
        * Endpoint for getting the student status
        * 
        */
        [HttpPost]
        [Route("adjustment/view")]
        public async Task<IActionResult> ViewAdjustmentList([FromBody] GetAdjustmentListRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetAdjustmentListResponse { students = null, count = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetAdjustmentListRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetAdjustmentlist(converted_req));

            //convert DTO to response
            var response = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAdjustmentListResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetAdjustmentListResponse { students = response, count = result.count });
        }

        [HttpPost]
        [Route("adjustment/approve")]
        public async Task<IActionResult> ApproveAdjustment([FromBody] ApproveAdjustmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ApproveAdjustmentResponse { success = 0, edp_code = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ApproveAdjustmentRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ApproveAdjustment(converted_req));

            return Ok(new ApproveAdjustmentResponse { success = result.success, edp_code = result.edp_code });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("status/count")]
        public async Task<IActionResult> GetStatusCount(GetStatusCountRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStatusCountRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetStatusCount(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStatusCountResponse>(serialized_result);

            return Ok(converted_result);
        }


        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("status/enrollmentStatus")]
        public async Task<IActionResult> GetEnrollmentStatus(GetEnrollmentStatusRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetEnrollmentStatusRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetEnrollmentStatus(converted_req));
            //convert DTO to response
            var response = result.courseStat.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetEnrollmentStatusResponse.courseStatus>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetEnrollmentStatusResponse { courseStat = response});
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("notification/add")]
        public async Task<IActionResult> AddNotification(AddNotificationRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AddNotificationRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.AddNotification(converted_req));

            return Ok(new AddNotificationResponse { success = result.success });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("teachers/gradereport")]
        public async Task<IActionResult> ViewGradeReport(GradeReportRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GradeReportRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GradeReport(converted_req));

            var response = result.gradeR.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GradeReportResponse.grade_report>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GradeReportResponse { gradeR = response.OrderBy(x => x.lastname).ToList() });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("teachers/list")]
        public async Task<IActionResult> GetTeachersList(GetTeachersListRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetTeachersListRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetTeachersList(converted_req));

            var response = result.teacherList.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTeachersListResponse.Teachers>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetTeachersListResponse { teacherList = response });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("teachers/teachersLoad")]
        public async Task<IActionResult> SaveTeachersLoad(SaveTeachersLoadRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SaveTeachersLoadRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.SaveTeachersLoad(converted_req));;

            return Ok(new SaveTeachersLoadResponse { success = result.success });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("teachers/adviser")]
        public async Task<IActionResult> AssignSectionAdviser(AssignSectionRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AssignSectionRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.AssignSectionAdviser(converted_req)); ;

            return Ok(new AssignSectionResponse { success = result.success });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("teachers/adviser/view")]
        public async Task<IActionResult> ViewAdviser(ViewAdviserRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewAdviserRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewAdviser(converted_req)); ;

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewAdviserResponse>(serialized_result);


            return Ok(converted_result);
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("teachers/getteachersload")]
        public async Task<IActionResult> ViewTeachersLoad([FromBody] GetStudyLoadRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudyLoadResponse { schedules = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetTeachersLoadRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetTeachersLoad(converted_req));

            //convert DTO to response
            var response = result.schedules.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTeachersLoadResponse.Schedules>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetTeachersLoadResponse { schedules = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("student/assessmentshs")]
        public async Task<IActionResult> GetSHSAssessment([FromBody] GetSHSAssessmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetSHSAssessmentRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetSHSAssessment(converted_req));

            //convert DTO to response
            var response = result.exams.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSHSAssessmentResponse.Exam>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetSHSAssessmentResponse { exams = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("student/assessmentbe")]
        public async Task<IActionResult> GetBEAssessment([FromBody] GetBEAssessmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetBEAssessmentRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetBEssessment(converted_req));

            //convert DTO to response
            var response = result.exams.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetBEAssessmentResponse.Exam>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetBEAssessmentResponse { exams = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("student/assessmentcl")]
        public async Task<IActionResult> GetCLAssessment([FromBody] GetSHSAssessmentRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCLAssessmentRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetCLAssessment(converted_req));

            //convert DTO to response
            var response = result.exams.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCLAssessmentResponse.Exam>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetCLAssessmentResponse { exams = response });
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("student/assessmentreport")]
        public async Task<IActionResult> AssessmentReport(GetStudentBalancePerCategoryRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentBalancePerCategoryRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetStudentBalancePerCategory(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentBalancePerCategoryResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("student/permitlist")]
        public async Task<IActionResult> ViewPermitList(ViewPermitListRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewPermitListRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewPermitList(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewPermitListResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("student/payments")]
        public async Task<IActionResult> GetPayments(GetPaymentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetPaymentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetPayments(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPaymentResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("student/payments/view")]
        public async Task<IActionResult> GetStudentPaymentList(GetStudentPaymentListRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentPaymentListRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetStudentPaymentList(converted_req));

            var response = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentPaymentListResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetStudentPaymentListResponse { students = response, count = result.count });
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("student/payments/acknowledge")]
        public async Task<IActionResult> AcknowledgePayment(AcknowledgePaymentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AcknowledgePaymentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.AcknowledgePayment(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<AcknowledgePaymentResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("teacher/uploadgrades")]
        public async Task<IActionResult> UploadGrades(UploadGradesRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UploadGradesRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.UploadGrades(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadGradesResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("teacher/uploadcore")]
        public async Task<IActionResult> UploadCore(UploadCoreValuesRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UploadCoreValuesRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.UploadCoreValues(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadCoreValuesResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("teacher/viewgrades")]
        public async Task<IActionResult> ViewGrades(ViewGradesRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewGradesRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewGrades(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewGradesResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
         * Endpoint for viewing classlist
         * 
         */
        [HttpPost]
        [Route("teacher/deptlist")]
        public async Task<IActionResult> ViewTeachersPerDept(ViewTeachersPerDepartmentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewTeachersPerDepartmentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ViewTeachersPerDept(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewTeachersPerDepartmentResponse>(serialized_result);

            return Ok(converted_result);
        }


        [HttpPost]
        [Route("config/basicedmonth")]
        public async Task<IActionResult> GetBasicEdMonth(GetBasicEdMonthRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetBasicEdMonthRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetBasicEdMonth(converted_req));

            return Ok(new GetBasicEdMonthResponse { start_month = result.start_month, end_month = result.end_month});
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("config/activeterm")]
        public async Task<IActionResult> GetActiveTerm()
        {
            var result = await Task.FromResult(_enrollmentManagement.GetActiveTerm());

            return Ok(new ActiveTermResponse { active_terms = result.active_terms, active_term = "20212" });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("lms/users")]
        public async Task<IActionResult> GetLMSUser([FromBody] UserLMSReportRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UserLMSReportResponse { users = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UserLMSReportRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CreateLMSUserReport(converted_req));

            //convert DTO to response
            var response = result.users.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLMSReportResponse.User>(rSched);
                return cSched;
            }).ToList();

            return Ok(new UserLMSReportResponse { users = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("lms/courses")]
        public async Task<IActionResult> GetLMCourses([FromBody] CourseLMSReportRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new CourseLMSReportResponse { courses = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CourseLMSReportRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CreateLMSCourseReport(converted_req));

            //convert DTO to response
            var response = result.courses.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<CourseLMSReportResponse.course>(rSched);
                return cSched;
            }).OrderBy(x => x.category).ToList();

            return Ok(new CourseLMSReportResponse { courses = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("lms/enrolled")]
        public async Task<IActionResult> GetLMSEnrolled([FromBody] EnrolledLMSReportRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new EnrolledLMSReportResponse { enrolled = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.EnrolledLMSReportRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CreateLMSEnrolledReport(converted_req));

            //convert DTO to response
            var response = result.enrolled.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrolledLMSReportResponse.enroll>(rSched);
                return cSched;
            }).OrderBy(x => x.col3).ToList();

            return Ok(new EnrolledLMSReportResponse { enrolled = response });
        }

        /*
        * Endpoint for getting the studyload
        * 
        */
        [HttpPost]
        [Route("lms/teachers")]
        public async Task<IActionResult> GetLMSTeachers(CreateTeachersLoadReportRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CreateTeachersLoadReportRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CreateTeachersLoadReport(converted_req));

            //convert DTO to response
            var response = result.teachers.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<TeachersLMSReportResponse.teacher>(rSched);
                return cSched;
            }).OrderBy(x => x.username).ToList();

            return Ok(new TeachersLMSReportResponse { teachers = response });
        }


        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("tools/transfergrades")]
        public async Task<IActionResult> REmoveDuplicateOstsp(TransferGradeRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.TransferGradeRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.TransferGrade(converted_req));

            return Ok(new TransferGradeResponse { success = result.success });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("tools/cleanseostsp")]
        public async Task<IActionResult> REmoveDuplicateOstsp(RemoveDuplicateOstspRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RemoveDuplicateOstspRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.RemoveDuplicateOstsp(converted_req));

            return Ok(new RemoveDuplicateOtspResponse { success = result.success });
        }

        /*
      * Endpoint for uploading images
      * 
      */
        [HttpPost]
        [Route("tools/forceupdatestatus")]
        public async Task<IActionResult> ForceUpdateStatus(UpdateStudentStatusRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateStudentStatusRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.UpdateStudentStatus(converted_req));

            return Ok(new UpdateStudentStatusResponse { success = result.success });
        }

        /*
     * Endpoint for uploading images
     * 
     */
        [HttpPost]
        [Route("tools/manualenrollment")]
        public async Task<IActionResult> ManualEnrollment(ManualEnrollmentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ManualEnrollmentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ManualEnrollment(converted_req));

            return Ok(new UpdateStudentStatusResponse { success = result.success });
        }

        /*
      * Endpoint for uploading images
      * 
      */
        [HttpPost]
        [Route("tools/correcttotalunits")]
        public async Task<IActionResult> CorrectTotalUnits(CorrectTotalUnitsRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CorrectTotalUnitsRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.CorrectTotalUnits(converted_req));

            return Ok(new RemoveDuplicateOtspResponse { success = result.success });
        }


        /*
        * Endpoint for uploading images
        * 
        */
        [HttpPost]
        [Route("tools/updateInfo/view")]
        public async Task<IActionResult> UpdateInfoView(UpdateInfoRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateInfoRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.GetInfoUpdate(converted_req));

            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateInfoResponse>(serialized_result);

            return Ok(converted_result);
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("tools/updateInfo")]
        public async Task<IActionResult> UpdateInfo(UpdateInforRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateInforRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.UpdateInfor(converted_req));

            return Ok(new UpdateInforResponse { success = result.success });
        }

        [HttpPost]
        [Route("tools/setclosedsubject")]
        public async Task<IActionResult> SetClosedSubject(SetClosedSubjectRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SetClosedSubjectRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.SetClosed(converted_req));

            return Ok(new SetClosedSubjectResponse { success = result.success });
        }

        /*
       * Endpoint for uploading images
       * 
       */
        [HttpPost]
        [Route("tools/activateadjustment")]
        public async Task<IActionResult> ActivateAdjustment(ActivateAdjustmentRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ActivateAdjustmentRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.ActivateAdjustment(converted_req));

            return Ok(new ActivateAdjustmentResponse { success = result.success });
        }

        [HttpPost]
        [Route("tools/transfersection")]
        public async Task<IActionResult> TransferSection(TransferSectionRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.TransferSectionRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.TransferSection(converted_req));

            return Ok(new TransferSectionResponse { success = result.success });
        }

        [HttpPost]
        [Route("tools/correctlabandlec")]
        public async Task<IActionResult> CorrectLabAndLec(CorrectLecAndLabRequest request)
        { 
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CorrectLecAndLabRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.CorrectLabAndLec(converted_req));

            return Ok(new CorrectLabAndLecResponse { success = result.success });
        }

        [HttpPost]
        [Route("tools/sendnotificationdissolved")]
        public async Task<IActionResult> SendNotificationForDissolved(SendNotificationRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SendNotificationRequest>(serialized_req);

            var result = await Task.FromResult(_enrollmentManagement.SendNotificationDissolved(converted_req));

            return Ok(new CorrectLabAndLecResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/prospectus")]
        public async Task<IActionResult> GetCurriculum([FromBody] GetCurriculumRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCurriculumRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetCurriculum(converted_req));

            // convert DTO to response
            // convert DTO to response


            var response = result.subjects.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumResponse.Subjects>(rSched);
                return cSched;
            }).ToList();
            
            var remark = result.requisites.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumResponse.Requisites>(rSched);
                return cSched;
            }).ToList();

            var grades = result.grades.Select(x =>
            {
                var rGrade = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cGrade = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumResponse.Grades>(rGrade);
                return cGrade;
            }).ToList();

            var schedules = result.schedules.Select(x =>
            {
                var rSchedule = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumResponse.Schedules>(rSchedule);
                return cSchedule;
            }).ToList();
            

            return Ok(new GetCurriculumResponse { success = result.success,dept = result.dept, subjects = response, course_code = result.course_code, requisites = remark, grades = grades, schedules = schedules, units = result.units, curr_year = result.curr_year });
        }
        [HttpPost]
        [Route("student/prospectusbe")]
        public async Task<IActionResult> GetCurriculumBE([FromBody] GetCurriculumBERequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCurriculumBERequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetCurriculumBE(converted_req));

            // convert DTO to response
            // convert DTO to response


            var response = result.subjects.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumBEResponse.Subjects>(rSched);
                return cSched;
            }).ToList();

            var remark = result.requisites.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumBEResponse.Requisites>(rSched);
                return cSched;
            }).ToList();

            var grades = result.grades.Select(x =>
            {
                var rGrade = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cGrade = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumBEResponse.Grades>(rGrade);
                return cGrade;
            }).ToList();

            var schedules = result.schedules.Select(x =>
            {
                var rSchedule = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCurriculumBEResponse.Schedules>(rSchedule);
                return cSchedule;
            }).ToList();


            return Ok(new GetCurriculumBEResponse { success = result.success, dept = result.dept, subjects = response, course_code = result.course_code, requisites = remark, grades = grades, schedules = schedules, units = result.units, curr_year = result.curr_year });
        }
        [HttpPost]
        [Route("student/requestsubject")]
        public async Task<IActionResult> RequestSubject([FromBody] StudentSubjectRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.StudentSubjectRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.RequestSubject(converted_req));

            // convert DTO to response
            // convert DTO to response

            return Ok(new StudentSubjectResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/getrequestsubject")]
        public async Task<IActionResult> GetRequestSubject([FromBody] GetSubjectReqRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetSubjectReqRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetRequestSubject(converted_req));

            var requestSubjects = result.request.Select(x =>
            {
                var rSchedule = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSubjectReqResponse.RequestedSubject>(rSchedule);
                return cSchedule;
            }).ToList();
            // convert DTO to response

            return Ok(new GetSubjectReqResponse { request = requestSubjects });
        }


        [HttpPost]
        [Route("student/getstudentrequest")]
        public async Task<IActionResult> GetStudentRequest([FromBody] GetStudentReqRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentReqRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudentSubjectRequest(converted_req));

            var requestSubjects = result.request.Select(x =>
            {
                var rSchedule = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentReqResponse.RequestedSubject>(rSchedule);
                return cSchedule;
            }).ToList();
            var filteredSubjects = result.filtered.Select(x =>
            {
                var rSchedule = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentReqResponse.FilteredSubject>(rSchedule);
                return cSchedule;
            }).ToList();
            // convert DTO to response

            return Ok(new GetStudentReqResponse { request = requestSubjects, filtered = filteredSubjects });
        }

        [HttpPost]
        [Route("student/addstudentrequest")]
        public async Task<IActionResult> AddStudentRequest([FromBody] AddStudentReqRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AddStudentReqRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.AddSubjectRequest(converted_req));

            return Ok(new AddStudentReqResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/cancelstudentrequest")]
        public async Task<IActionResult> CancelSubjectRequest([FromBody] CancelSubjectReqRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CancelSubjectReqRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CancelSubjectRequest(converted_req));

            return Ok(new CancelEnrollmentResponse { success = result.success });
        }

        [HttpPost]
        [Route("allcurriculum")]
        public async Task<IActionResult> GetAllCurriclum()
        {

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetAllCurriculum());


            var getYears = result.year.Select(x =>
            {
                var rYears = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cYears = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllCurriculumResponse.SchoolYear>(rYears);
                return cYears;

            }).ToList();
            var getCourses = result.courses.Select(x =>
            {
                var rCourse = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cCourse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllCurriculumResponse.Courses>(rCourse);
                return cCourse;

            }).ToList();

            var getDepartments = result.departments.Select(x =>
            {
                var rCourse = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cCourse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllCurriculumResponse.Departments>(rCourse);
                return cCourse;

            }).ToList();
            return Ok(new GetAllCurriculumResponse { year = getYears, course = getCourses, departments = getDepartments, current_curriculum = result.current_curriculum });

        }

        [HttpPost]
        [Route("courselist")]
        public async Task<IActionResult> CourseList([FromBody] GetCourseInfoRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetCourseInfoRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetCourseInfo(converted_req));

            var getCourseList = result.courses.Select(x =>
            {
                var rCourse = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cCourse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCourseInfoResponse.Courses>(rCourse);
                return cCourse;

            }).ToList();

            return Ok(new GetCourseInfoResponse { courses = getCourseList });
        }

        [HttpPost]
        [Route("savecurriculum")]
        public async Task<IActionResult> SaveCurriculum([FromBody] AddCurriculumRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AddCurriculumRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.AddCurriculum(converted_req));


            return Ok(new AddCurriculumResponse { success = result.success });
        }

        [HttpPost]
        [Route("closecurriculum")]
        public async Task<IActionResult> CloseCurriculum([FromBody] CloseCurriculumRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CloseCurriculumRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.CloseCurriculum(converted_req));
            return Ok(new CloseCurriculumResponse { success = result.success });
        }

        [HttpPost]
        [Route("getsubjectinfo")]
        public async Task<IActionResult> GetSubjectInfo([FromBody] GetSubjectInfoRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetSubjectInfoRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetSubjectInfo(converted_req));

            var subjects = result.subjects.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSubjectInfoResponse.Subjects>(rSched);
                return cSched;
            }).ToList();
            var remarks = result.requisites.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSubjectInfoResponse.Requisites>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetSubjectInfoResponse { subjects = subjects, requisites = remarks });
        }
        [HttpPost]
        [Route("curriculum/removerequisite")]
        public async Task<IActionResult> RemovePrerequisite([FromBody] RemoveRequisiteRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RemoveRequisiteRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.RemovePrerequisite(converted_req));

            return Ok(new RemoveRequisiteResponse { success = result.success });
        }
        [HttpPost]
        [Route("curriculum/saverequisite")]
        public async Task<IActionResult> SavePrerequisite([FromBody] SaveRequisiteRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SaveRequisiteRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SavePrerequisite(converted_req));

            return Ok(new RemoveRequisiteResponse { success = result.success });
        }
        [HttpPost]
        [Route("getequivalence")]
        public async Task<IActionResult> GetEquivalence([FromBody] GetEquivalenceRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetEquivalenceRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetEquivalence(converted_req));

            var equivalences = result.equivalences.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetEquivalenceResponse.Equivalence>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetEquivalenceResponse { equivalences = equivalences });
        }

        [HttpPost]
        [Route("searchsubject")]
        public async Task<IActionResult> SearchSubjet([FromBody] SearchSubjectRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SearchSubjectRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SearchSubject(converted_req));

            var subjects = result.subjects.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchSubjectResponse.Subjects>(rSched);
                return cSched;
            }).ToList();

            return Ok(new SearchSubjectResponse { subjects = subjects });
        }

        [HttpPost]
        [Route("curriculum/addequivalence")]
        public async Task<IActionResult> AddEquivalence([FromBody] AddEquivalenceRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.AddEquivalenceRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.AddEquivalence(converted_req));


            return Ok(new AddEquivalenceResponse { success = result.success });
        }

        [HttpPost]
        [Route("curriculum/removeequivalence")]
        public async Task<IActionResult> RemoveEquivalence([FromBody] AddEquivalenceRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.RemoveEquivalenceRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.RemoveEquivalence(converted_req));


            return Ok(new RemoveEquivalenceResponse { success = result.success });
        }

        [HttpPost]
        [Route("curriculum/subjectequivalence")]
        public async Task<IActionResult> GetsubjectEquivalence([FromBody] GetSubjectEquivalenceRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetSubjectEquivalenceRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetSubjectEquivalence(converted_req));
            var subjects = result.subjects.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetSubjectEquivalenceResponse.Subjects>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetSubjectEquivalenceResponse { subjects = subjects });
        }

        [HttpPost]
        [Route("student/evaluate/all")]
        public async Task<IActionResult> ViewStudentEvaluate([FromBody] ViewStudentDeanEvaluationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ViewStudentPerStatusResponse { students = null });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ViewStudentDeanEvaluationRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.ViewStudentEvaluation(converted_req));

           
            var student = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewStudentDeanEvaluationResponse.Student>(rSched);
                return cSched;
            }).ToList();

            return Ok(new ViewStudentDeanEvaluationResponse { students = student, count = result.count });
        }


        [HttpPost]
        [Route("student/request/all")]
        public async Task<IActionResult> GetStudentReuqestList([FromBody] GetStudentListRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudentListResponse { students = null });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentListRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudentList(converted_req));


            var student = result.students.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentListResponse.Students>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetStudentListResponse { students = student,});
        }

        [HttpPost]
        [Route("student/request/updatestatus")]
        public async Task<IActionResult> UpdateRequestStatus([FromBody] UpdateRequestScheduleStatusRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UpdateRequestScheduleStatusResponse { success = 0 });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateRequestScheduleStatusRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.UpdateRequestStatus(converted_req));



            return Ok(new UpdateRequestScheduleStatusResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/getstudentgrades")]
        public async Task<IActionResult> GetStudentGrades([FromBody] GetStudentGradesRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudentGradesResponse { });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentGradesRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudentGrades(converted_req));

            var grade = result.grades.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentGradesResponse.Grades>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetStudentGradesResponse { grades = grade });
        }
        [HttpPost]
        [Route("student/getstudentgradesbe")]
        public async Task<IActionResult> GetStudentGradesBE([FromBody] GetStudentGradesBERequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudentGradesResponse { });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.GetStudentGradesBERequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.GetStudentGradesBE(converted_req));

            var grade = result.grades.Select(x =>
            {
                var rSched = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var cSched = Newtonsoft.Json.JsonConvert.DeserializeObject<GetStudentGradesBEResponse.Grades>(rSched);
                return cSched;
            }).ToList();

            return Ok(new GetStudentGradesBEResponse { grades = grade });
        }
        [HttpPost]
        [Route("student/setstudentgrades")]
        public async Task<IActionResult> SetGradeEvaluation ([FromBody] SetGradeEvaluationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new GetStudentGradesResponse { });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SetGradeEvaluationRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SetGradeEvaluation(converted_req));

            return Ok(new SetGradeEvaluationResponse { success = result.success });
        }

        [HttpPost]
        [Route("student/setstatusevaluation")]
        public async Task<IActionResult> SetStatusEvaluation([FromBody] SetStatusEvaluationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new SetStatusEvaluationResponse { success = 0 });
            }

            //Convert response object to DTO Objectscourses
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.SetStatusEvaluationRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_enrollmentManagement.SetStatusEvaluation(converted_req));

            return Ok(new SetGradeEvaluationResponse { success = result.success });
        }
    }
}
