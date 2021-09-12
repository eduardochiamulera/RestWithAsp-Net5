using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _personService;

        public BookController(ILogger<BookController> logger, IBookBusiness personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            
            if (person == null) return NotFound();

            return Ok(person);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (book == null) return BadRequest();

            return Ok(_personService.Create(book));
        }
        
        [HttpPut]
        public IActionResult Put([FromBody]Book book)
        {
            if (book == null) return BadRequest();

            return Ok(_personService.Update(book));
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);

            return NoContent();
        }

 
    }
}
