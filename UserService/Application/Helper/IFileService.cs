using System;
using Microsoft.AspNetCore.Http;

namespace Application.Helper;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile request);
}
