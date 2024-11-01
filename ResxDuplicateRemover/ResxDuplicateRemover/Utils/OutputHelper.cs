using System.Globalization;
using ResxDuplicateRemover.Models;

namespace ResxDuplicateRemover.Utils;

public static class OutputHelper
{
    public static void StartLogging(string path, DateTime time)
    {
        Console.WriteLine($"Resource lookup started at: {time}");
        Console.WriteLine($"Looking for resx files in: \"{path}\"");
    }
    
    public static void LogCount(int count)
    {
        Console.WriteLine($"Found {count} resx files to review.");
    }
    
    public static void StartFile(string fileName)
    {
        Console.WriteLine($"Started processing \"{fileName}\".");
    }
    
    public static void DuplicateFound(string fileName, string nodeName)
    {
        Console.WriteLine($"Duplicate resource \"{nodeName}\" found and removed from \"{fileName}\".");
    }
    
    public static void SummarizeFile(CheckResult output)
    {
        Console.WriteLine($"Finished processing \"{output.FileName}\", {output.Duplicates} removed.");
    }
    
    public static void NoFileChanges(CheckResult output)
    {
        Console.WriteLine($"Finished processing \"{output.FileName}\", no duplicates were found.");
    }

    
    
    public static void FinishLogging(string path, List<CheckResult> results, DateTime startTime)
    {
        var totalFiles = results.Count;
        var totalEntries = results.Sum(r => r.Duplicates);
        var processTime = DateTime.Now - startTime;
        
        Console.WriteLine($"Resource lookup for: {path} took {processTime} and found {totalEntries} duplicates across {totalFiles} files");
    }
}