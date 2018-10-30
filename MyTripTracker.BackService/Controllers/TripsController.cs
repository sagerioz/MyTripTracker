using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTripTracker.BackService.Data;
using MyTripTracker.BackService.Models;


namespace MyTripTracker.BackService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : Controller
    {
        TripContext _dbcontext;

        public TripsController(TripContext dbcontext)
        // stash a copy of this repository in a private field so we can interact with it:
        { 
            _dbcontext = dbcontext;

            // this instructs the app to not use resources tracking changes on any of the results returned from
            // HTTP requests. We just want a copy to go to the browser with no data built up around it.
            // "Short-Lived Queries" 

            _dbcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // private Repository _repository;

        //GET api/Trips
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            // ToList() is actually a link method which causes
            // the query to the db to materialize the Trips object. The Trips
            // object is the query itself.
            //
            // in a bigger application, you would want to
            // create different classes that contain the data access logic rather
            // than using EF context code directly in the controller as we are here.
            // that way you can encapsulate the logic elsewhere.
            // IEnumerable will execute query and return the results.


            var trips= await _dbcontext.Trips.AsNoTracking().ToListAsync();
            return Ok(trips);
        }

        // GET api/Trips/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return _dbcontext.Trips.Find(id);

        }

        // POST api/Trips
        [HttpPost]
        public IActionResult Post([FromBody]Trip value)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbcontext.Trips.Add(value);
            _dbcontext.SaveChanges();

            return Ok();
        }

        // PUT api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Trip value)
        {
            // .Any returns a Boolean. .Find() returns the whole object. Lighter on the db query
            if (!_dbcontext.Trips.Any(t => t.Id == id) )
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // this Update() method uploads and updates ALL the values in the object, even if there is
            // only one change in one column
            // what about nulls? 

            _dbcontext.Trips.Update(value);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/Trips/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var myTrip = _dbcontext.Trips.Find(id);
            if (myTrip == null)
            {
                return NotFound();
            }
            // DELETE FROM Trips WHERE id=?
            _dbcontext.Remove(myTrip);
            _dbcontext.SaveChanges();
            return NoContent();
        }
    }
}
