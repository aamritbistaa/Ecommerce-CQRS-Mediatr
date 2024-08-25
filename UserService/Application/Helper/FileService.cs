using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Application.Helper;

public class FileService : IFileService
{
    private readonly string[] _allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".jfif" };
    private const long _allowedFileSize = 1 * 1024 * 1024; // 1MB

    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadFileAsync(IFormFile request)
    {
        if (request == null)
        {
            throw new Exception("File you are trying to upload is null.");
        }
        if (request.Length > _allowedFileSize)
        {
            throw new Exception("File size exceeds 1MB");
        }
        var contentPath = _environment.ContentRootPath;
        var path = Path.Combine(contentPath, "Resources");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var extension = Path.GetExtension(request.FileName);
        if (!_allowedFileExtensions.Contains(extension))
        {
            throw new Exception($"Not an allowed file format, must be in format{string.Join(',', _allowedFileExtensions)}");
        }
        var fileName = $"{Guid.NewGuid().ToString()}{extension}";
        var fileNameWithPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fileNameWithPath, FileMode.CreateNew);
        await request.CopyToAsync(stream);
        return fileName;
    }
}
