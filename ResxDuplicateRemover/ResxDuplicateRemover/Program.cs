using ResxDuplicateRemover.Models;
using static ResxDuplicateRemover.Utils.OutputHelper;
using static ResxDuplicateRemover.Utils.ResourceCollector;
using static ResxDuplicateRemover.Utils.ResourceDuplicateUtil;

var startTime = DateTime.Now;

if (Environment.GetCommandLineArgs().Length == 0)
    throw new ArgumentNullException($"Please pass an absolute path for the utility to check.");
    
var path = Environment.GetCommandLineArgs()[1];

StartLogging(path, startTime);

var files = GetResourceFiles(path);

LogCount(files.Count);

var outputResults = files.Select(RemoveUnnecessaryResources).ToList();

FinishLogging(path, outputResults, startTime);
