namespace Application.Services;

public interface IFileService
{
    Task<string> SaveImageAsync(Stream imageStream, string fileName, string folderName);
    void DeleteImage(string imagePath);
    bool IsValidImage(string fileName, string contentType, long fileSize);
} 