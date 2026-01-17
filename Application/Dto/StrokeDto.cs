namespace Application.Dto;

public record StrokeDto
{
    public required string Color { get; init; }
    public required ushort Size { get; init; }
    public IEnumerable<PointDto> Points { get; init; } = [];

}
