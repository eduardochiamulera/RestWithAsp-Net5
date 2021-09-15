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
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness business)
        {
            _loginBusiness = business;
        }

        [HttpPost]
        [Route("token")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO TokenVO)
        {
            if (TokenVO == null) return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(TokenVO);

            if (token == null) return BadRequest("Invalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;

            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Invalid client request");

            return NoContent();
        }
    }
}
