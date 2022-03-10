using LabNOSQL.Models;
using LabNOSQL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabNOSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITasksCollection db = new TasksCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await db.GetAllTasks());
        }

        [HttpGet("today​")]
        public async Task<IActionResult> GetAllTodayTasks()
        {
            return Ok(await db.GetTodayTask());
        }

        [HttpGet("{​id}​")]
        public async Task<IActionResult> GetTask(string id)
        {
            return Ok(await db.GetOneTask(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tasks task)
        {
            if (task == null)
                return BadRequest();
            if (task.Name == string.Empty)
                ModelState.AddModelError("Name", "la tarea debe contener un nombre");
            await db.CreateTask(task);
            return Created("Created", true);
        }

        [HttpPut("{​id}​")]
        public async Task<IActionResult> UpdateProduct([FromBody] Tasks task, string id)
        {
            if (task == null)
                return BadRequest();
            if (task.Name == string.Empty)
                ModelState.AddModelError("Name", "la tarea debe contener un nombre");
            task.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateTask(task);
            return Created("Actulizado", true);
        }

        [HttpDelete("{​id}​")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            await db.DeleteTask(id);
            return NoContent();
        }

        [HttpPost("complete")]
        public IActionResult CompleteTask(string id)
        {
            return Ok(db.CompleteTask(id));
        }
    }
}
