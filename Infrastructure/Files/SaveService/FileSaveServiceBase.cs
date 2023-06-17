using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files.SaveService;

public abstract class FileServiceBase : IFileService<string>
{
    private readonly string _webRootDirectoryPath;
    private readonly FileServiceTools _fileServiceTools;

    protected FileServiceBase(IWebHostEnvironment webHostEnvironment, FileServiceTools fileServiceTools)
    {
        _webRootDirectoryPath = webHostEnvironment.WebRootPath;
        _fileServiceTools = fileServiceTools;
    }
    
    protected abstract string SaveFolder { get; }

    public async Task<string> SaveAsync(IFormFile formFile)
    {
        if(formFile is null)
            throw new NullReferenceException("Нет целевого файла на сохранение");
        if (string.IsNullOrEmpty(SaveFolder) == true)
            throw new NullReferenceException("Название папки для сохранения не указана");
        
        string fileName = _fileServiceTools.GenerateFileName(formFile.FileName);
        string saveFolder = _fileServiceTools.GetSaveDirectoryPath(SaveFolder);
        string savePath = _fileServiceTools.GenerateSavePath(saveFolder, fileName);
        
        await using Stream stream = new FileStream(savePath, FileMode.Create);
        await formFile.CopyToAsync(stream);

        return Path.Combine(SaveFolder, fileName);
    }

    public bool Delete(string fileName)
    {
        var filePath = Path.Combine(_webRootDirectoryPath, fileName); 
        if (File.Exists(filePath) == false)
            return false;

        File.Delete(filePath);
        return true;
    }
}