namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Metadata;

public class ParameterMetadata
{
    public string Name { get; }

    public bool IsRequired { get; }

    public ParameterMetadata(string name, bool isRequired)
    {
        Name = name;
        IsRequired = isRequired;
    }
}