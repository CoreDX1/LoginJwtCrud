using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dto.Request;
using POS.Application.Interface;

namespace POS.src.POS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Usercontroller : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public Usercontroller(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        // GET: api/User/Select
        [HttpGet]
        [Route("Select")]
        public async Task<IActionResult> ListSelectUser()
        {
            var response = await _userApplication.ListSelectUser();
            return Ok(response);
        }

        // GET: api/User/UserById/4
        [Authorize]
        [HttpGet]
        [Route("UserById")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }

        // POST: api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        // POST: api/User/GenerateToken
        [HttpPost]
        [Route("GenerateToken")]
        public async Task<IActionResult> GenerateToke([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return Ok(response);
        }

        // PUT: api/User/EditUser/2
        [HttpPut]
        [Route("EditUser/{userId:int}")]
        public async Task<IActionResult> EditUser(int userId, UserRequestDto requestDto)
        {
            var response = await _userApplication.EditUser(userId, requestDto);
            return Ok(response);
        }

        // DELETE: api/User/DeleteUser/2
        [HttpDelete]
        [Route("DeleteUser/{userId:int}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var response = await _userApplication.RemoveUser(userId);
            return Ok(response);
        }
    }
}
