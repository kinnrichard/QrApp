using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QrAppWebApi.Models;

namespace QrAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly DataContext _context;

        public AttendancesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public IEnumerable<Attendance> GetAttendances()
        {
            return _context.Attendances;
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await _context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // GET: api/Attendances/employee/5
        [HttpGet("employee/{empid}/{dateToday}/timein/")]
        public async Task<IActionResult> GetAttendanceTimeIn([FromRoute] string empid, string dateToday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == empid && a.AttendanceDate == dateToday );

            if (attendance.TimeIn != null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

      
        // GET: api/Attendances/employee/5
        [HttpGet("employee/{empid}/{dateToday}/timeout/")]
        public async Task<IActionResult> GetAttendanceTimeOut([FromRoute] string empid, string dateToday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == empid && a.AttendanceDate == dateToday);

            if (attendance.TimeOut != null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // PUT: api/Attendances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance([FromRoute] int id, [FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendances
        [HttpPost]
        public async Task<IActionResult> PostAttendance([FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return Ok(attendance);
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}