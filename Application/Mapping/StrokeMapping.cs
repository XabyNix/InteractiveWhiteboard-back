using Application.Dto;
using Core.Models;

namespace Application.Mapping;

public static class StrokeMapping
{
    public static StrokeDto ToDto(this StrokeDbo stroke)
        => new()
        {
            Color = stroke.Color,
            Size = stroke.Size,
            Points = stroke.Points.Select(p => p.ToDto())
        };

    public static PointDto ToDto(this PointDbo point)
        => new()
        {
            X = point.X,
            Y = point.Y,
            IsNewLine = point.IsNewLine,
        };
}