using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BE_BTOnline2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly IInternServices _internServices;

        public InternController(IInternServices internServices)
        {
            _internServices = internServices;
        }

        // Lấy tất cả Interns theo quyền hạn
        [HttpGet]
        public IActionResult GetAllInterns()
        {
            // Lấy RoleId từ Claim trong JWT Token
            var roleIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            if (string.IsNullOrEmpty(roleIdClaim))
                return Unauthorized(new { message = "Không có quyền truy cập" });

            int roleId = int.Parse(roleIdClaim);

            var interns = _internServices.GetAllInterns(roleId);

            return Ok(interns);
        }

        // Lấy thông tin chi tiết Intern theo Id
        [HttpGet("{id}")]
        public IActionResult GetInternById(int id)
        {
            var roleIdClaim = User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            if (string.IsNullOrEmpty(roleIdClaim))
                return Unauthorized(new { message = "Không có quyền truy cập" });

            int roleId = int.Parse(roleIdClaim);

            var intern = _internServices.GetInternById(id, roleId);
            if (intern == null)
                return NotFound(new { message = "Không tìm thấy thông tin thực tập sinh" });

            return Ok(intern);
        }

        // Thêm mới Intern
        [HttpPost]
        public IActionResult AddIntern([FromBody] InternRequest request)
        {
            if (_internServices.AddIntern(request))
                return Ok(new { message = "Thêm thực tập sinh thành công" });

            return BadRequest(new { message = "Thêm thực tập sinh không thành công" });
        }

        // Cập nhật thông tin Intern
        [HttpPut("{id}")]
        public IActionResult UpdateIntern(int id, [FromBody] InternRequest request)
        {
            if (_internServices.UpdateIntern(id, request))
                return Ok(new { message = "Cập nhật thông tin thực tập sinh thành công" });

            return NotFound(new { message = "Không tìm thấy thông tin thực tập sinh" });
        }

        // Xóa Intern
        [HttpDelete("{id}")]
        public IActionResult DeleteIntern(int id)
        {
            if (_internServices.DeleteIntern(id))
                return Ok(new { message = "Xóa thực tập sinh thành công" });

            return NotFound(new { message = "Không tìm thấy thông tin thực tập sinh" });
        }
    }
}
