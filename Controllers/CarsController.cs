using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarApi5.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CarApi5.Controllers
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDBContext _context;

        public CarsController(CarDBContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetCars(long id)
        {
            var cars = await _context.Cars.FindAsync(id);

            if (cars == null)
            {
                return NotFound();
            }

            return cars;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCars(long id, Cars cars)
        {
            if (id != cars.Id)
            {
                return BadRequest();
            }

            _context.Entry(cars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarsExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cars>> PostCars(Cars cars)
        {
            _context.Cars.Add(cars);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCars", new { id = cars.Id }, cars);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cars>> DeleteCars(long id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();

            return cars;
        }

        private bool CarsExists(long id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
