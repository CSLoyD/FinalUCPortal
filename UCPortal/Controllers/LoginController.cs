using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UCPortal.Authenticator;
using UCPortal.BusinessLogic.Login;
using UCPortal.RequestResponse.Request;
using UCPortal.RequestResponse.Response;

namespace UCPortal.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private ILoginManagement _loginManagement;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public LoginController(ILoginManagement loginManagement, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _loginManagement = loginManagement;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        /*W
         * Endpoint for login user
         * 
         */
        [AllowAnonymous]
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new LoginResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.LoginRequest>(serialized_req);

            //await result from function CheckIfUserExist
            var result = await Task.FromResult(_loginManagement.CheckIfUserExist(converted_req));

            //Convert DTO to response result
            var serialized_result = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var converted_result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(serialized_result);

            string token = String.Empty;

            if (result.id_number != null)
            {
                token = _jwtAuthenticationManager.Authenticate(result.id_number, result.user_type);
            }

            //create login response
            var loginResponse = new LoginResponse();

            loginResponse.success = result.success;
            loginResponse.is_verified = result.is_verified;

            //result has info
            if (result.student_info != null)
            {
                loginResponse.student_info = converted_result.student_info;
                loginResponse.user_type = result.user_type;
                loginResponse.email = result.email;
                loginResponse.id_number = result.id_number;
                loginResponse.classification = result.classification;
            }
            loginResponse.curr_year = result.curr_year;
            loginResponse.allowed_units = result.allowed_units;
            loginResponse.token = token;
            loginResponse.profile = result.profile;
            loginResponse.has_load = result.has_load;
            loginResponse.current_exam = result.current_exam;
            loginResponse.active_terms = result.active_terms;
            loginResponse.has_active_term = result.has_active_term;

            //return login reponse
            return Ok(loginResponse);
        }

        /*
         * Endpoint for getting all notifications per student
         * 
         */

        [HttpPost]
        [Route("notification")]
        public async Task<IActionResult> GetNotification([FromBody] NotificationRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new NotificationResponse { notifications = null });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.NotificationRequest>(serialized_req);

            //await result from function CheckIfUserExist
            var result = await Task.FromResult(_loginManagement.GetNotifications(converted_req));

            //Convert DTO Objects list to response
            var response = result.notifications.Select(x =>
            {
                var rsRes = Newtonsoft.Json.JsonConvert.SerializeObject(x);
                var rRes = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationResponse.Notification>(rsRes);
                return rRes;
            }).ToList();

            //return notification reponse
            return Ok(new NotificationResponse { notifications = response });
        }

        /*
        * Endpoint for checking if user is verified
        * 
        */

        [HttpPost]
        [Route("userverified")]
        public async Task<IActionResult> CheckIfEmailIsValidated([FromBody] VerifyEmailRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new VerifyEmailResponse { is_verified = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.VerifyEmailRequest>(serialized_req);

            //await result from function CheckifUserIsVerified
            var result = await Task.FromResult(_loginManagement.CheckifUserIsVerified(converted_req));

            //return notification reponse
            return Ok(new VerifyEmailResponse { is_verified = result.is_verified });
        }

        /*
        * Endpoint for Resetting password
        * 
        */
        [HttpPost]
        [Route("user/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ResetPasswordRequest>(serialized_req);

            //await result from function ResetPassword
            var result = await Task.FromResult(_loginManagement.ResetPassword(converted_req));

            //return reponse
            return Ok(new ResetPasswordResponse { success = result.success });
        }

        /*
        * Endpoint for Changing password
        * 
        */
        [HttpPost]
        [Route("user/changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ChangePasswordRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.ChangePassword(converted_req));

            //return  reponse
            return Ok(new ChangePasswordResponse { success = result.success });
        }

        /*
        * Endpoint for Checking email
        * 
        */
        [HttpPost]
        [Route("student/email")]
        public async Task<IActionResult> CheckEmailRequest([FromBody] EmailCheckRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new EmailCheckResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.EmailCheckRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.CheckEmail(converted_req));

            return Ok(new EmailCheckResponse { success = result.success });
        }


        /*
        * Endpoint for validating email
        * 
        */
        [HttpPost]
        [Route("student/email/verify")]
        public async Task<IActionResult> ValidateEmail([FromBody] ValidateEmailRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ValidateEmailResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ValidateEmailRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.ValidateEmail(converted_req));

            return Ok(new ValidateEmailResponse { success = result.success });
        }


        /*
        * Endpoint for reading notification
        * 
        */
        [HttpPost]
        [Route("student/notification")]
        public async Task<IActionResult> ReadNotification([FromBody] ReadNotifRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new ReadNotifResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.ReadNotifRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.ReadNotification(converted_req));

            return Ok(new ReadNotifResponse { success = result.success });
        }

        /*
        * Endpoint for reading notification
        * 
        */
        [HttpPost]
        [Route("user/create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateAccountRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new CreateAccountResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.CreateAccountRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.CreateAccount(converted_req));

            return Ok(new CreateAccountResponse { success = result.success });
        }

        /*
        * Endpoint for Updating email
        * 
        */
        [HttpPost]
        [Route("student/email/update")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequest request)
        {
            //Check if required fields are present
            if (!ModelState.IsValid)
            {
                return Ok(new UpdateEmailResponse { success = 0 });
            }

            //Convert response object to DTO Objects
            var serialized_req = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var converted_req = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Request.UpdateEmailRequest>(serialized_req);

            //await result from function ChangePassword
            var result = await Task.FromResult(_loginManagement.UpdateEmail(converted_req));

            return Ok(new UpdateEmailResponse { success = result.success });
        }

    }
}
