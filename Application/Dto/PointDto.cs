namespace Application.Dto;


public record PointDto
{
    public required float X { get; init; } 
    public required float Y { get; init; }
    public required bool IsNewLine { get; init; }
}
