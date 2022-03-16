using JobManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobManager.API.Controllers
{
    //https://jobmanagerdevapi.azurewebsites.net/A00004242/Jobs
    //https://jobmanagerdevapi.azurewebsites.net/A00004242/Jobs/9
    [Route("{studentnumber:regex(^A00[[0-9]]{{6}}$)}/[controller]")]
    [ApiController]
   
    public class JobsController : ControllerBase
    {
        private readonly JobContext _context; 

        public JobsController(JobContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(string studentnumber)
        {
            var jobs = await _context.Jobs.ToListAsync();
            return jobs; 
        }


        [HttpGet("(id)")]
        public async Task<ActionResult<Job>> GetJob(string studentnumber, int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound(); 
            }
            return job; 
        }

        [HttpPut("(id)")]
        public async Task<IActionResult> PutJob(string studentnumber, int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }
            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateCurrencyException)
            {
                if (!JobExists(id))
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

        //POST: api/Jobs
        [HttpPost]

        public async Task<ActionResult<Job>> PostJob(JobManager job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        //Delete: api/jobs/5

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(string studentnumber, int id)
        {
            var job = await _context.Jobs.FindAsync(id); 
            if (job == null)
            {
                return NotFound(); 
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id); 
        }


     //   private List<Job> GetDefaultJobs()
       // {
         //   var jobs = new List<Job>()
           // {
            // new Job { Id = 1, Name = "Job A API", Description = "This is job a." },
            // new Job { Id = 2, Name = "Job B API", Description = "This is job b." },
            // new Job { Id = 3, Name = "Job C API", Description = "This is job c." },
            // new Job { Id = 4, Name = "Job D API", Description = "This is job d." }
          //  throw new NotImplementedException();
   //     };
     //       return jobs;
       // }
    }
}
