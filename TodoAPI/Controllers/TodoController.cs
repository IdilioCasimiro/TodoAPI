using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;
            if (context.Items.Count() == 0)
            {
                context.Items.Add(new TodoItem() { Name = "Item 1", Done = true });
                context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> Get()
        {
            return await context.Items.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetItem")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"O recurso solicitado para o ID: {id} não foi encontrado!");
            }
            return Ok(item);
        }

        public async Task<IActionResult> Post([FromBody] TodoItem item)
        {
            if (ModelState.IsValid)
            {
                await context.Items.AddAsync(item);
                await context.SaveChangesAsync();
                return CreatedAtRoute("GetItem", new { id = item.ID }, item);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodoItem item)
        {
            var todo = await context.Items.FindAsync(id);
            if (todo == null)
                return NotFound($"O recurso solicitado para o ID: {id} não foi encontrado!");

            todo.Name = item.Name;
            todo.Done = item.Done;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item == null)
                return NotFound($"O recurso solicitado para o ID {id} não foi encontrado!");

            context.Items.Remove(item);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}