using Cisi.CisiColors.DBModels;
using Cisi.CisiColors.Infrastructure.Models;
using Dotgem.Colors;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cisi.CisiColors.Infrastructure.DB;

public sealed class ColorDataAccess : IColorDataAccess
{
    private readonly IConfiguration _config;

    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _colors_collectionName;
    private readonly string _colorCollections_collectionName;

    private readonly IMongoCollection<ColorModel> _colorCollection;
    private readonly IMongoCollection<ColorCollectionModel> _colorCollectionsCollection;

    public ColorDataAccess(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("MongoDBDefault");
        _databaseName = _config.GetDatabase("Colors");
        _colors_collectionName = _config.GetDBCollection("Colors");
        _colorCollections_collectionName = _config.GetDBCollection("ColorCollections");

        _colorCollection = CreateCollectionReference<ColorModel>(_colors_collectionName);
        _colorCollectionsCollection = CreateCollectionReference<ColorCollectionModel>(_colorCollections_collectionName);
    }

    public async Task<List<ColorModel>> GetAllColorsAsync()
    {
        var allItems = await _colorCollection.FindAsync(_ => true);
        return allItems.ToList();
    }

    public List<ColorModel> GetAllColors()
    {
        var allItems = _colorCollection.Find(_ => true);
        return allItems.ToList();
    }

    public async Task<IReadOnlyList<ColorCollectionAndStatus>> GetAllColorCollectionsAsync()
    {
        var allItems = await _colorCollectionsCollection.FindAsync(_ => true);

        return CreateListOfColorcollectionAndStatus(allItems.ToList());
    }

    public IReadOnlyList<ColorCollectionAndStatus> GetAllColorCollections()
    {
        var allItems = _colorCollectionsCollection.Find(_ => true);

        return CreateListOfColorcollectionAndStatus(allItems.ToList());
    }

    private static IReadOnlyList<ColorCollectionAndStatus> CreateListOfColorcollectionAndStatus(List<ColorCollectionModel> list)
    {
        var preColorCollections = list.Select(c => new PreColorCollection(c.Id, c.Title, c.Description,
            c.Colors?.Select(color => new ColorDefinition(
                color.Id ?? ObjectId.GenerateNewId().ToString(),
                color.Name ?? "unknown",
                Color.FromARGB(color.R, color.G, color.B)))
        ));
        var colorCollections = preColorCollections.Select(c => ColorsCollection.From(c.Title ?? "unknown", c.Colors?.ToList() ?? new()));
        var colorCollectionAndStatus = colorCollections.Select(c => ColorCollectionAndStatus.FoundAndLoaded(c));
        return colorCollectionAndStatus.ToList();
    }

    private IMongoCollection<T> CreateCollectionReference<T>(string collectionName)
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collectionName);
    }
}
