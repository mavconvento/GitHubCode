using BussinessLayer.Contracts;
using BussinessLayer.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;
using System.IO;
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
        private readonly IHostEnvironment _hostingEnvironment;

        public UploadFileController(IExceptionService exceptionService, IFileDownloadUpload fileDownloadUpload, IImageService image, IHostEnvironment hostingEnvironment)
        {

            _exceptionService = exceptionService ?? throw new ArgumentNullException(nameof(exceptionService));
            _fileUpload = fileDownloadUpload ?? throw new ArgumentNullException(nameof(fileDownloadUpload));
            _imageService = image ?? throw new ArgumentNullException(nameof(image));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        [HttpPost("[action]")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage([FromForm] DomainObject.FileUpload file)
        {
            try
            {
                int width = 400;
                int height = 400;

                Image image = Image.FromStream(file.File.OpenReadStream(), true, true);
                var newImage = new Bitmap(width, height);

                string path = Path.Combine(_hostingEnvironment.ContentRootPath);

                using (var a = Graphics.FromImage(newImage))
                {
                    a.DrawImage(image, 0, 0, width, height);
                    newImage.Save(path);
                }

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
