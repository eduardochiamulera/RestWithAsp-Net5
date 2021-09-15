using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

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
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            
            if (person == null) return NotFound();

            return Ok(person);
        }
        
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody]BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_personService.Create(book));
        }
        
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody]BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_personService.Update(book));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);

            return NoContent();
        }

 
    }
}
