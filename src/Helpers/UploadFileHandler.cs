using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRSApplication.Helpers
{
    public class UploadFileCommand : IRequest<string>
    {
        public string Genre { get; set; }
        public IFormFile file { get; set; }
    }
    public class UploadFileHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedFileExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".svg", ".jfif" };
        private const long _allowedFileSize = 1 * 1024 * 1024; // 1MB
        public UploadFileHandler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if (request.file == null)
            {
                throw new Exception("File you are trying to upload is null.");
            }
            if (request.file.Length > _allowedFileSize)
            {
                throw new Exception("File size exceeds 1MB");
            }
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", request.Genre);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(request.file.FileName);
            if (!_allowedFileExtensions.Contains(extension))
            {
                throw new Exception($"Not an allowed file format, must be in format{string.Join(',', _allowedFileExtensions)}");
            }
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fileNameWithPath = Path.Combine(path, fileName);

            using var stream = new FileStream(fileNameWithPath, FileMode.CreateNew);
            await request.file.CopyToAsync(stream, cancellationToken);
            return fileName;
        }
    }
}