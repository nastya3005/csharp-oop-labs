namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

public class FlagMetadata
{
    public string Name { get; }

    public bool IsRequired { get; }

    public bool HasValue { get; }

    public object? DefaultValue { get; }

    public FlagMetadata(string name, bool isRequired, bool hasValue, object? defaultValue = null)
    {
        Name = name;
        IsRequired = isRequired;
        HasValue = hasValue;
        DefaultValue = defaultValue;
    }
}