Call this utility and pass an absolute path as a parameter in the CLI.
It will locate resx files non-recursively, identify duplicate nodes within
those files and remove the duplicate nodes.

If it detects that the node contents are different it will throw an error.
