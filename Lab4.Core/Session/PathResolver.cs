namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Session;

public static class PathResolver
{
    public static bool IsAbsolute(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        return path.StartsWith('/');
    }

    public static string Resolve(string basePath, string relativePath)
    {
        if (IsAbsolute(relativePath))
            return Normalize(relativePath);

        if (string.IsNullOrEmpty(basePath))
            throw new ArgumentException("Base path cannot be null or empty", nameof(basePath));

        List<string> baseComponents = SplitPath(basePath);
        List<string> relativeComponents = SplitPath(relativePath);

        var resultComponents = new List<string>();

        foreach (string component in baseComponents)
        {
            if (component == ".")
                continue;

            if (component == "..")
            {
                if (resultComponents.Count > 0)
                    resultComponents.RemoveAt(resultComponents.Count - 1);
                continue;
            }

            resultComponents.Add(component);
        }

        foreach (string component in relativeComponents)
        {
            if (component == ".")
                continue;

            if (component == "..")
            {
                if (resultComponents.Count > 0)
                    resultComponents.RemoveAt(resultComponents.Count - 1);
                continue;
            }

            resultComponents.Add(component);
        }

        return "/" + string.Join("/", resultComponents);
    }

    public static string Normalize(string path)
    {
        if (string.IsNullOrEmpty(path))
            return "/";

        path = path.Replace('\\', '/');
        path = path.Trim('/');

        List<string> components = SplitPath(path);
        var normalizedComponents = new List<string>();

        foreach (string component in components)
        {
            if (component == ".")
                continue;

            if (component == "..")
            {
                if (normalizedComponents.Count > 0)
                    normalizedComponents.RemoveAt(normalizedComponents.Count - 1);
                continue;
            }

            normalizedComponents.Add(component);
        }

        string result = string.Join("/", normalizedComponents);
        return IsAbsolute(path) ? "/" + result : result;
    }

    private static List<string> SplitPath(string path)
    {
        var components = new List<string>();

        if (string.IsNullOrEmpty(path))
            return components;

        path = path.Replace('\\', '/');

        foreach (string component in path.Split('/', StringSplitOptions.RemoveEmptyEntries))
        {
            components.Add(component);
        }

        return components;
    }
}