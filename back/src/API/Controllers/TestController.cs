using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("files")]
    public IActionResult GetFiles()
    {
        var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "img");
        var files = new List<object>();
        
        if (Directory.Exists(imgPath))
        {
            var usuarioPath = Path.Combine(imgPath, "usuario");
            if (Directory.Exists(usuarioPath))
            {
                var imageFiles = Directory.GetFiles(usuarioPath, "*.png");
                foreach (var file in imageFiles)
                {
                    var fileName = Path.GetFileName(file);
                    var fileInfo = new FileInfo(file);
                    files.Add(new
                    {
                        fileName,
                        fullPath = file,
                        size = fileInfo.Length,
                        url = $"/img/usuario/{fileName}"
                    });
                }
            }
        }
        
        return Ok(new
        {
            imgPath,
            exists = Directory.Exists(imgPath),
            files
        });
    }

    [HttpGet("image/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "img", "usuario", fileName);
        
        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound($"Archivo no encontrado: {fileName}");
        }
        
        var fileInfo = new FileInfo(imagePath);
        return Ok(new
        {
            fileName,
            fullPath = imagePath,
            size = fileInfo.Length,
            exists = true,
            url = $"/img/usuario/{fileName}"
        });
    }
} 