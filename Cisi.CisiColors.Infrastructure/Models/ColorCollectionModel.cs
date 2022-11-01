using Cisi.CisiColors.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Cisi.CisiColors.DBModels;

public class ColorCollectionModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [DisallowNull]
    public string? Id { get; set; }

    [DisallowNull]
    public string? Title { get; set; }

    [DisallowNull]
    public string? Description { get; set; }

    [DisallowNull]
    public List<ColorModel>? Colors { get; set; }
}
