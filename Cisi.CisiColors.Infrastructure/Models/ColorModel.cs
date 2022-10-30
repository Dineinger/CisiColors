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

    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    [BsonConstructor]
    public ColorModel(string id, byte r, byte g, byte b)
    {
        Id = id;
        R = r;
        G = g;
        B = b;
    }

    public ColorModel(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}
