using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackgroundTaskWithHangfire.Data;
using BackgroundTaskWithHangfire.Models;
using BackgroundTaskWithHangfire.Job;
using BackgroundTaskWithHangfire.Services;

namespace BackgroundTaskWithHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly BackgroundTaskWithHangfireContext _context;
        private readonly InformationMongoService _informationMongoService;

        public InformationController(BackgroundTaskWithHangfireContext context, InformationMongoService informationMongoService)
        {
            _context = context;
            _informationMongoService = informationMongoService;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Information>>> GetInformation()
        {
            var info = await _context.Information.ToListAsync();

            //firing recurring job
            var rj = new Recurring_Job(_context, _informationMongoService);

            return await _context.Information.ToListAsync();
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }

            return information;
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, Information information)
        {
            if (id != information.id)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
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

        // POST: api/Information
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(Information information)
        {
            _context.Information.Add(information);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { id = information.id }, information);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Information>> DeleteInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return information;
        }

        private bool InformationExists(int id)
        {
            return _context.Information.Any(e => e.id == id);
        }
    }
}
