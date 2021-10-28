using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestWithASPNETUdemy.Business;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;
using System.IO;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [Produces("application/json")]
        [Authorize("Bearer")]
        public async Task<IActionResult> UploadOneFile([FromForm]IFormFile file)
        {
            var detail = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [Produces("application/json")]
        [Authorize("Bearer")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            var details = await _fileBusiness.SaveFilesToDisk(files);

            return new OkObjectResult(details);
        }
        
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [Produces("application/octet-stream")]
        [Authorize("Bearer")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            var buffer = _fileBusiness.GetFile(fileName);
            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }
    }
}
