using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTOnline2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user == null)
                return NotFound(new { message = "Không tìm thấy người dùng" });

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserRequest request)
        {
            if (_userServices.AddUser(request))
                return Ok(new { message = "Thêm người dùng thành công" });

            return BadRequest(new { message = "Thêm người dùng không thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserRequest request)
        {
            if (_userServices.UpdateUser(id, request))
                return Ok(new { message = "Cập nhật người dùng thành công" });

            return NotFound(new { message = "Không tìm thấy người dùng" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_userServices.DeleteUser(id))
                return Ok(new { message = "Xóa người dùng thành công" });

            return NotFound(new { message = "Không tìm thấy người dùng" });
        }
    }
}
