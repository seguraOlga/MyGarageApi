using Microsoft.AspNetCore.Mvc;

namespace MyGarageApi.Models
{
    public class UploadImageRequest
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
