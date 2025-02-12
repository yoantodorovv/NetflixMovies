using Movies.Common.Enumerations;

namespace Movies.Dtos.Duration;

public class DurationDto
{
    public DurationDto()
    {}
    public DurationDto(DurationType durationType, int value)
    {
        DurationType = durationType;
        Value = value;
    }
    
    public DurationType DurationType { get; set; }

    public int Value { get; set; }
}