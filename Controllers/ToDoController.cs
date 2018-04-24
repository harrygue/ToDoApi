using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// http://localhost:59126/api/todo
namespace ToDoApi.Controllers
{
    [Route("api/todo")]
    public class ToDoController : Controller
    {
        private readonly ToDoContext _context;
        public ToDoController(ToDoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new ToDoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/todo
        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            // return new string[] { "value1", "value2" };
            return _context.TodoItems.ToList();
        }

        // GET api/todo/5
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            // return "value";
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            if(item == null)
            {
                return BadRequest();
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ToDoItem item)
        {
            if(item == null || item.Id != id)
            {
                return BadRequest(); // produces a status 400 response
            }
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo == null)
            {
                return NotFound(); // produses a status 404 response
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/todo/
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if(todo == null)
            {
                return NotFound();
            }
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
