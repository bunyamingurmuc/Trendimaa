using Microsoft.AspNetCore.Mvc;
using Trendeimaa.Entities;
using Trendimaa.API.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.DTO;

namespace Trendimaa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public readonly IImageService _service;
        public static IWebHostEnvironment _environment;
        public ImageController(IImageService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment= environment;
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetAllAsy()
        {
            var response = await _service.GetAllAsync();
            return this.ResponseStatusWithData(response);
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> GetByIdAsy(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return this.ResponseStatusWithData(response);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> CreateAsy(Image entity)
        {
            var response = await _service.CreateAsync(entity);
            return this.ResponseStatusWithData(response);

        }

        [HttpPut]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> UpdateAsy(Image entity)
        {
            var response = await _service.UpdateAsync(entity);
            return this.ResponseStatusWithData(response);
        }

        [HttpDelete]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> DeleteAsy(int id)
        {

            var response = await _service.RemoveAsync(id);
            return this.ResponseStatusWithData(response);
        }
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> ImageUpload([FromForm] CImageUploadDto objFile, int? productId)
        {

            if (objFile.File.Length > 0)
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                Random rnd = new Random();
                int randomNumber = rnd.Next(1, 40000);
                var imageName = "img" + randomNumber + ".png";
                var path = _environment.WebRootPath + "\\images\\" + imageName;
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    objFile.File.CopyTo(fileStream);
                    fileStream.Flush();
                }

                var image = new Image()
                {
                    Path = "http://localhost:5178/images/" + imageName,
                    ProductId = productId,

                };
                var response = await _service.CreateAsync(image);
                return this.ResponseStatusWithData(response);


            }

            return BadRequest();


        }
    }
}
