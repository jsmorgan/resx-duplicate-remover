namespace ResxDuplicateRemover.Utils;

public static class ResourceCollector
{
    // this is not recursive intentionally right now.
    public static List<FileInfo> GetResourceFiles(string path)
    {
        var rootDirectory = new DirectoryInfo(path);
        
        var resourceFiles = new List<FileInfo>();

        resourceFiles.AddRange(rootDirectory.GetFiles("*.resx"));

        return resourceFiles;
    }
}