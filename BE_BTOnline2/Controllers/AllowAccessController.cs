using BE_BTOnline2.Models.Requests;
using BE_BTOnline2.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTOnline2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AllowAccessController : ControllerBase
    {
        private readonly IAllowAccessServices _allowAccessServices;

        public AllowAccessController(IAllowAccessServices allowAccessServices)
        {
            _allowAccessServices = allowAccessServices;
        }

        [HttpGet]
        public IActionResult GetAllAllowAccess()
        {
            var allowAccessList = _allowAccessServices.GetAllAllowAccess();
            return Ok(allowAccessList);
        }

        [HttpGet("{id}")]
        public IActionResult GetAllowAccessById(int id)
        {
            var allowAccess = _allowAccessServices.GetAllowAccessById(id);
            if (allowAccess == null)
                return NotFound(new { message = "Không tìm thấy quyền truy cập" });

            return Ok(allowAccess);
        }

        [HttpPost]
        public IActionResult AddAllowAccess([FromBody] AllowAccessRequest request)
        {
            if (_allowAccessServices.AddAllowAccess(request))
                return Ok(new { message = "Thêm quyền truy cập thành công" });

            return BadRequest(new { message = "Thêm quyền truy cập không thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAllowAccess(int id, [FromBody] AllowAccessRequest request)
        {
            if (_allowAccessServices.UpdateAllowAccess(id, request))
                return Ok(new { message = "Cập nhật quyền truy cập thành công" });

            return NotFound(new { message = "Không tìm thấy quyền truy cập" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAllowAccess(int id)
        {
            if (_allowAccessServices.DeleteAllowAccess(id))
                return Ok(new { message = "Xóa quyền truy cập thành công" });

            return NotFound(new { message = "Không tìm thấy quyền truy cập" });
        }
    }
}
