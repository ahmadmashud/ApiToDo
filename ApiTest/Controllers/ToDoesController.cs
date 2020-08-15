using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTest.DbEntities;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesController : ControllerBase
    {
        private readonly CRUDContext _context;

        public ToDoesController(CRUDContext context)
        {
            _context = context;
        }


        //Get All Todo’s
        // GET: api/ToDoes
        [HttpGet]
        public IEnumerable<ToDo> GetToDo()
        {
            return _context.ToDo;
        }

        //Get Specific Todo
        // GET: api/ToDoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.ToDo.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        //update ToDo
        // PUT: api/ToDoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
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

        //create new ToDo
        // POST: api/ToDoes
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        //Delete ToDo
        // DELETE: api/ToDoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDo.Remove(toDo);
            await _context.SaveChangesAsync();

            return Ok(toDo);
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }

        //Get Incoming ToDo (for today/next day/current week)
        //  api/ToDoes/GetIncomingToDo/(today/nextDay/currentWeek)
        [HttpGet("GetIncomingToDo/{desc}")]
        public async Task<IActionResult> GetIncomingToDo([FromRoute] string desc)
        {
            DateTime start = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

            DateTime end = start.AddDays(7);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = _context.ToDo.AsQueryable();

            if (desc.Contains("today")) {
                toDo = toDo.Where(x => x.CreatedAt.Date == DateTime.Now.Date);
            } else if (desc.Contains("nextDay")) {
                toDo = toDo.Where(x => x.CreatedAt.Date > DateTime.Now.Date);
            }
            else {
                toDo = toDo.Where(x => x.CreatedAt.Date >= start && x.CreatedAt.Date < end );
            }

            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        //Set Todo percent complete
        // api/ToDoes/SetToDoPercent/1/100
        [HttpGet("SetToDoPercent/{id}/{percent}")]
        public async Task<IActionResult> SetToDoPercent([FromRoute] int id, [FromRoute] int percent)
        {
       
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var data = _context.ToDo.SingleOrDefault(x => x.Id == id);
            data.CompletedPresentage = percent;

            if (data.CompletedPresentage == 100) {
                data.IsCompleted = true;
            }

            return Ok(data);
        }

        //Mark Todo as Done
        // api/ToDoes/SetCompleted/1
        [HttpGet("SetCompleted/{id}")]
        public async Task<IActionResult> SetCompleted([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = _context.ToDo.SingleOrDefault(x => x.Id == id);
            data.IsCompleted = true;

            return Ok(data);
        }
    }


}