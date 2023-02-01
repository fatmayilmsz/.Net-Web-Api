using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("todoitem")]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoContext _context;
        public ToDoItemController(ToDoContext todocontext) 
        {
            _context= todocontext;
        }

        [HttpGet("{id:int}")]
        public IActionResult Find(int id)
        {
            return Ok(_context.Persons.ToList());
        }
       
        [HttpPost]
        public IActionResult Post(Person person) 
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            _context.Persons.Add(new Person()
           {
               ID= person.ID,
               FirstName= person.FirstName,
               LastName= person.LastName,
           });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var existingPerson = _context.Persons.Where(p => p.ID == person.ID).FirstOrDefault<Person>();
            if(existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete] 
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid person id");
            }
            var personn = _context.Persons.Where(p => p.ID == id).FirstOrDefault();

            if (personn is null)
            {
                return BadRequest("Not a valid person id");
            }

            _context.Entry(personn).State = EntityState.Deleted;
            _context.SaveChanges();

            return Ok();
        }


    }
}
