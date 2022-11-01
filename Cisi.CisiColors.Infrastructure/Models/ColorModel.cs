using Dotgem.Colors;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.Infrastructure.Models;

public class ColorModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DisallowNull]
    public string? Id { get; set; }

    [DisallowNull]
    public string? Name { get; set; }

    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    [BsonConstructor]
    public ColorModel(string id, string name, byte r, byte g, byte b)
    {
        Id = id;
        Name = name;
        R = r;
        G = g;
        B = b;
    }

    [Obsolete]
    public ColorModel(byte r, byte g, byte b)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = "unknown";
        R = r;
        G = g;
        B = b;
    }

    public ColorModel(string name, byte r, byte g, byte b)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        R = r;
        G = g;
        B = b;
    }

    public ColorModel(string name, Color color)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        R = color.R;
        G = color.G;
        B = color.B;
    }
}
