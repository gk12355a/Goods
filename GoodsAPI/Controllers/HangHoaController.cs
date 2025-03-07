using GoodsAPI.Data;
using GoodsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HangHoaController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Lấy tất cả hàng hóa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> GetAll()
        {
            return await _context.HangHoas.ToListAsync();
        }

        // ✅ Lấy hàng hóa theo mã (truyền qua route)
        [HttpGet("{id}")]
        public async Task<ActionResult<HangHoa>> GetById(string id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null) return NotFound();
            return hangHoa;
        }

        // ✅ Thêm hàng hóa mới
        [HttpPost]
        public async Task<ActionResult<HangHoa>> Create(HangHoa hangHoa)
        {
            _context.HangHoas.Add(hangHoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = hangHoa.MaHangHoa }, hangHoa);
        }

        // ✅ Cập nhật hàng hóa
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, HangHoa hangHoa)
        {
            if (id != hangHoa.MaHangHoa) return BadRequest("Mã hàng hóa không khớp");

            _context.Entry(hangHoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Xóa hàng hóa
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null) return NotFound();

            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Cập nhật ghi chú của hàng hóa
        [HttpPatch("{id}/ghichu")]
        public async Task<IActionResult> UpdateGhiChu(string id, [FromBody] UpdateGhiChuRequest request)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null) return NotFound();

            hangHoa.GhiChu = request.GhiChu;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // ✅ Model hỗ trợ cập nhật ghi chú
    public class UpdateGhiChuRequest
    {
        public string GhiChu { get; set; }
    }
}
