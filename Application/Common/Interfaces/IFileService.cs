using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces;

public interface IFileService
{
}

public interface IFileService<T> : IFileService
{
    Task<T> SaveAsync(IFormFile formFile);

    bool Delete(string fileName);
}