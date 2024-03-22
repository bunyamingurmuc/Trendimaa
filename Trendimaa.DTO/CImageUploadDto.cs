using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendimaa.DTO
{
    public class CImageUploadDto
    {
        public IFormFile File { get; set; }
    }
}
