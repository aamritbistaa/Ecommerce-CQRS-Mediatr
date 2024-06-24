using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CQRSApplication.Helpers
{
    public class DeleteFileCommand : IRequest<string?>
    {
        public string Genre { get; set; }
        public string fileNameWithExtension { get; set; }
    }
    public class DeleteFileHandler : IRequestHandler<DeleteFileCommand, string?>
    {
        private readonly IWebHostEnvironment _environment;
        public DeleteFileHandler(IWebHostEnvironment enviroment)
        {
            _environment = enviroment;
        }
        public async Task<string?> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.fileNameWithExtension))
            {
                throw new Exception("File name is empty");
            }
            var contentPath = _environment.ContentRootPath;
            var fileNameWithPath = Path.Combine(contentPath, "Uploads", request.Genre, request.fileNameWithExtension);
            if (!File.Exists(fileNameWithPath))
            {
                throw new Exception($"Unable to find the file {request.fileNameWithExtension}");
            }
            File.Delete(fileNameWithPath);
            return null;
        }
    }
}