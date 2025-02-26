using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTOnline2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleServices.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _roleServices.GetRoleById(id);
            if (role == null)
                return NotFound(new { message = "Không tìm thấy chức vụ" });

            return Ok(role);
        }

        [HttpPost]
        public IActionResult AddRole([FromBody] RoleRequest request)
        {
            if (_roleServices.AddRole(request))
                return Ok(new { message = "Thêm chức vụ thành công" });

            return BadRequest(new { message = "Thêm chức vụ không thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleRequest request)
        {
            if (_roleServices.UpdateRole(id, request))
                return Ok(new { message = "Cập nhật chức vụ thành công" });

            return NotFound(new { message = "Không tìm thấy chức vụ" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            if (_roleServices.DeleteRole(id))
                return Ok(new { message = "Xóa chức vụ thành công" });

            return NotFound(new { message = "Không tìm thấy chức vụ" });
        }
    }
}
