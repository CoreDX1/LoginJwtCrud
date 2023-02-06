using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dto.Request;
using POS.Application.Interface;

namespace POS.src.POS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class Usercontroller : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public Usercontroller(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        [Route("Select")]
        public async Task<IActionResult> ListSelectUser()
        {
            var response = await _userApplication.ListSelectUser();
            return Ok(response);
        }

        [HttpGet]
        [Route("SelectUserById")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [HttpPost]
        [Route("GenerateToken")]
        public async Task<IActionResult> GenerateToke([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return Ok(response);
        }

        [HttpPut]
        [Route("EditUser/{userId:int}")]
        public async Task<IActionResult> EditUser(int userId, UserRequestDto requestDto)
        {
            var response = await _userApplication.EditUser(userId, requestDto);
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteUser/{userId:int}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var response = await _userApplication.RemoveUser(userId);
            return Ok(response);
        }
    }
}
