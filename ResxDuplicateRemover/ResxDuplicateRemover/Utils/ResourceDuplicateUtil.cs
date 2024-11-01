using System.Data;
using System.Xml;
using ResxDuplicateRemover.Models;
using static ResxDuplicateRemover.Utils.OutputHelper;

namespace ResxDuplicateRemover.Utils;

public static class ResourceDuplicateUtil
{
    public static CheckResult RemoveUnnecessaryResources(FileSystemInfo fileInfo)
    {
        var output = new CheckResult(){FileName = fileInfo.FullName};
        StartFile(output.FileName);
        try
        {
            // Load the resx file
            var rootDoc = new XmlDocument();
            rootDoc.Load(fileInfo.FullName);

            // this list us used to store nodes as they get found
            var resourceList = new SortedList<string, XmlNode>();

            var nodes = rootDoc.SelectNodes("//data[@name]");
            if (nodes != null)
            {
                foreach (XmlNode resource in nodes)
                {
                    var nodeName = resource.Attributes?["name"]?.Value;
                    if (nodeName == null) continue;
                    
                    // Look for any matching "named" nodes already found
                    var matchedResource = resourceList.FirstOrDefault(x => x.Key.Equals(nodeName, StringComparison.CurrentCultureIgnoreCase));

                    // Not found yet, add to the list
                    if (matchedResource.Value == null)
                    {
                        resourceList.Add(nodeName, resource);
                    }
                    else
                    {
                        // if the values aren't truly a duplicate, let the user know to resolve manually
                        // and abort
                        if (matchedResource.Value.InnerText != resource.InnerText)
                            throw new DataException("Node data didn't match. Manually check these.");
                                
                        output.Duplicates++;
                        DuplicateFound(output.FileName, nodeName);
                        resource.ParentNode?.RemoveChild(resource);
                    }
                }
            }
            
            // If there were changes save the file
            if (output.Duplicates > 0)
            {
                rootDoc.Save(fileInfo.FullName);
                SummarizeFile(output);
            }
            else
                NoFileChanges(output);
        }
        catch (Exception e)
        {
            output.Error = e.Message;
            Console.WriteLine(e.ToString());
        }

        return output;
    }
}