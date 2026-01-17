namespace Core.Models;

public record StrokeDbo(
    string Color, 
    ushort Size, 
    ICollection<PointDbo> Points);