using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_api.Models;

namespace task_api.Controllers
{
    [Route("api/TasksItems")]
    [ApiController]
    public class TasksItemsController : ControllerBase
    {
        private readonly TasksContext _context;

        public TasksItemsController(TasksContext context)
        {
            _context = context;
        }

        // GET: api/TasksItems/completed
        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TasksItem>>> GetCompletedTasksItems()
        {
            if (_context.TasksItems == null)
            {
                return NotFound();
            }
            var completedTasks = await _context.TasksItems.Where(t => t.Completed == true).ToListAsync();
            return completedTasks;
        }

        // GET: api/TasksItems/incomplete
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<TasksItem>>> GetIncompleteTasksItems()
        {
            if (_context.TasksItems == null)
            {
                return NotFound();
            }
            var incompleteTasks = await _context.TasksItems.Where(t => t.Completed == false).ToListAsync();
            return incompleteTasks;
        }

        // GET: api/TasksItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksItem>>> GetTasksItems()
        {
          if (_context.TasksItems == null)
          {
              return NotFound();
          }
            return await _context.TasksItems.ToListAsync();
        }

        // GET: api/TasksItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksItem>> GetTasksItem(long id)
        {
          if (_context.TasksItems == null)
          {
              return NotFound();
          }
            var tasksItem = await _context.TasksItems.FindAsync(id);

            if (tasksItem == null)
            {
                return NotFound();
            }

            return tasksItem;
        }

        // PUT: api/TasksItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksItem(long id, TasksItem tasksItem)
        {
            if (id != tasksItem.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(tasksItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tasksItem);
        }

        // POST: api/TasksItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksItem>> PostTasksItem(TasksItem tasksItem)
        {
          if (_context.TasksItems == null)
          {
              return Problem("Entity set 'TasksContext.TasksItems'  is null.");
          }
            _context.TasksItems.Add(tasksItem);
            await _context.SaveChangesAsync();

//    return CreatedAtAction("GetTasksItem", new { id = tasksItem.Id }, tasksItem);
    return CreatedAtAction(nameof(GetTasksItem), new { id = tasksItem.TaskId }, tasksItem);
}

        // DELETE: api/TasksItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksItem(long id)
        {
            if (_context.TasksItems == null)
            {
                return NotFound();
            }
            var tasksItem = await _context.TasksItems.FindAsync(id);
            if (tasksItem == null)
            {
                return NotFound();
            }

            _context.TasksItems.Remove(tasksItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksItemExists(long id)
        {
            return (_context.TasksItems?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
