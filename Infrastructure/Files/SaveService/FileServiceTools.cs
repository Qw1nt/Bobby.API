using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Files.SaveService;

public class FileServiceTools
{
    private readonly string _webRootDirectoryPath;

    public FileServiceTools(IWebHostEnvironment webHostEnvironment)
    {
        _webRootDirectoryPath = webHostEnvironment.WebRootPath;
    }

    public string GetSaveDirectoryPath(string saveFolder)
    {
        string savePath = Path.Combine(_webRootDirectoryPath, saveFolder);

        if (Directory.Exists(savePath) == false)
            Directory.CreateDirectory(savePath);

        return savePath;
    }

    public string GenerateFileName(string sourceFileName)
    {
        string sourceExtension = Path.GetExtension(sourceFileName);
        string randomFileName = Path.GetRandomFileName();
        
        string fileName = Path.ChangeExtension(randomFileName, sourceExtension);
        
        return fileName;
    }

    public string GenerateSavePath(string fullPathToSaveDirectory, string fileName)
    {
        string savePath = Path.Combine(fullPathToSaveDirectory, fileName);
        return savePath;
    }

    public string GetSavePath(string fileNameAndFolder)
    {
        return Path.Combine(_webRootDirectoryPath, fileNameAndFolder);
    }
}