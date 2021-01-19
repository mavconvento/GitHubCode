using BussinessLayer.Contracts;
using BussinessLayer.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IExceptionService _exceptionService;
        private readonly IFileDownloadUpload _fileUpload;
        private readonly IImageService _imageService;

        public UploadFileController(IExceptionService exceptionService, IFileDownloadUpload fileDownloadUpload,IImageService image)
        {
            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));
            _fileUpload= fileDownloadUpload ?? throw new ArgumentNullException(nameof(fileDownloadUpload));
            _imageService = image ?? throw new ArgumentNullException(nameof(image));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadImage([FromForm] DomainObject.FileUpload file)
        {
            try
            {
                return Ok(await _fileUpload.UploadImage(file));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImage([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _imageService.GetImage(id));
            }
            catch (Exception ex)
            {
                //ExceptionLog exception = await _exceptionService.UpsertException(ex, "GetUserRoles", "", "");
                return StatusCode((int)HttpStatusCode.InternalServerError, new CustomMesageError(ex.Message).Message);
            }
        }

    }
}
