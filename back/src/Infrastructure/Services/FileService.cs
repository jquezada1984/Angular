using Application.Services;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly string _baseImagePath;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    private readonly int _maxFileSizeInMB = 5;

    public FileService()
    {
        _baseImagePath = Path.Combine(Directory.GetCurrentDirectory(), "img");
    }

    public async Task<string> SaveImageAsync(Stream imageStream, string fileName, string folderName)
    {
        if (imageStream == null || imageStream.Length == 0)
            throw new ArgumentException("El stream de imagen es requerido");

        if (!IsValidImage(fileName, "image/*", imageStream.Length))
            throw new ArgumentException("El archivo no es una imagen válida");

        // Crear directorio si no existe
        var folderPath = Path.Combine(_baseImagePath, folderName);
        Directory.CreateDirectory(folderPath);

        // Generar nombre único para la imagen
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(folderPath, uniqueFileName);

        // Guardar archivo
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        // Retornar URL relativa
        return $"/img/{folderName}/{uniqueFileName}";
    }

    public void DeleteImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
            return;

        // Convertir URL a ruta física
        var relativePath = imagePath.TrimStart('/');
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public bool IsValidImage(string fileName, string contentType, long fileSize)
    {
        if (string.IsNullOrEmpty(fileName) || fileSize == 0)
            return false;

        // Verificar extensión
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (!_allowedExtensions.Contains(extension))
            return false;

        // Verificar tamaño (5MB máximo)
        if (fileSize > _maxFileSizeInMB * 1024 * 1024)
            return false;

        // Verificar tipo MIME
        if (!contentType.StartsWith("image/"))
            return false;

        return true;
    }
} 