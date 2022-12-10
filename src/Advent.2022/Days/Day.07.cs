using Advent.Domain;

namespace Advent._2022;

class FileObject
{
    public string Name { get; private set; } = string.Empty;
    public string Directory { get; private set; } = string.Empty;

    public string Path => Directory + "/" + Name;

    public long Size { get; private set; }

    public FileObject(string dir, string line)
    {
        Directory = dir;

        var vals = line.Split(' ');
        Size = long.Parse(vals[0]);
        Name = vals[1];
    }
}

class Command
{
    public const string Prefix = "$";

    public string Type { get; private set; } = string.Empty;
    public string Arg { get; private set; } = string.Empty;

    public Command(string line)
    {
        var vals = line.Split(' ');
        Type = vals[1];
        if (vals.Length > 2) Arg = vals[2];
    }
}

public class FileSystemBrowser
{
    const char DELIM = '/';

    private readonly ICollection<string> _input;

    private readonly ICollection<FileObject> _files = new List<FileObject>();

    private readonly IDictionary<string, long> _dirs = new Dictionary<string, long>();

    private readonly Stack<string> Path = new();

    public long UsedSpace => _dirs["/"];

    public FileSystemBrowser(IEnumerable<string> inputs)
    {
        _input = inputs.ToList();

        ParseFilesFromInputs();
        ParseDirectoryMap();
    }

    void ParseFilesFromInputs()
    {
        foreach (int i in Enumerable.Range(0, _input.Count))
        {
            string line = _input.ElementAt(i);

            if (line.StartsWith(Command.Prefix))
            {
                HandleCommand(line);
                continue;
            }

            if (line.StartsWith("dir")) continue;

            var file = new FileObject(GetPath(Path), line);
            _files.Add(file);
        }
    }

    void HandleCommand(string line)
    {
        var cmd = new Command(line);
        if (cmd.Type == "cd")
        {
            if (cmd.Arg == "..") Path.Pop();
            else Path.Push(cmd.Arg);
        }
    }

    void ParseDirectoryMap()
    {
        foreach (var file in _files)
        {
            if (!_dirs.ContainsKey(file.Directory)) _dirs[file.Directory] = 0;

            if (file.Directory == "/")
            {
                AddToDir(file.Directory, file.Size);
                continue;
            }

            var parts = file.Directory.Split(DELIM);
            string path = string.Empty;

            foreach (var part in parts)
            {
                path += path.EndsWith(DELIM) ? part : $"{DELIM}{part}";

                AddToDir(path, file.Size);
            }
        }
    }

    void AddToDir(string path, long size)
    {
        if (!_dirs.ContainsKey(path)) _dirs[path] = 0;

        _dirs[path] += size;
    }

    public long CalculatePartOne(int maxSize = 100000)
    {
        var below = _dirs.Where(kvp => kvp.Value <= maxSize).ToList();
        long total = below.Select(x => x.Value).Sum();
        return total;
    }

    public long CalculatePartTwo(long totalSpace = 70000000, long targetSpace = 30000000)
    {
        long unused = totalSpace - UsedSpace;

        var candidates = _dirs.Where(kvp => unused + kvp.Value > targetSpace).ToList();
        if (candidates.Count == 0) throw new Exception($"No valid candidate directories found!");

        var smallest = candidates.OrderBy(x => x.Value).First();

        return smallest.Value;
    }

    static string GetPath(Stack<string> currPath)
    {
        return string.Join(DELIM, currPath.Reverse()).Replace("//", "/");
    }
}