namespace ResxDuplicateRemover.Models;

public class CheckResult
{
    public required string FileName { get; set; }
    public int Duplicates { get; set; }
    public string? Error { get; set; } // right now just catch the first error and abort 
}