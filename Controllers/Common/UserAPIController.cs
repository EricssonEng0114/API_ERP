using API_ERP.Interfaces;
using API_ERP.Models;
using API_ERP.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ERP.Controllers.Common
{
    [Route("[controller]")]
    //[ApiController]
    public class UserAPIController : Controller
    {
        private IUserService _userService;

        public UserAPIController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("checkAccessToken"), Authorize]
        public bool checkUserStillAuthorized()
        {
            return true;
        }

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] WebAPILoginKey data)
        {
            //DateTime a = DateTimeZone.malaysia;
            AuthoriseViewModel authoriseViewModel = null;
            string errorMessageReturn = "Username or password is incorrect.";
            try
            {
                if (data != null)
                    //authoriseViewModel = _userService.Authenticate(userParam.ApiAppName, userParam.ApiKey);
                    authoriseViewModel = await _userService.Authenticate_Async(data).ConfigureAwait(false);

                if (authoriseViewModel == null)
                    return BadRequest(new { message = errorMessageReturn });
                else// if (authoriseViewModel != null)
                {
                    if (string.IsNullOrEmpty(authoriseViewModel.access_token))
                        return BadRequest(new { message = errorMessageReturn });
                }

                return Ok(authoriseViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Hi, You Have an Error Message: " + ex.Message });
            }
        }
    }
}
