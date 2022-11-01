using Cisi.CisiColors.DBModels;
using Cisi.CisiColors.Infrastructure.Models;
using Dotgem.Colors;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cisi.CisiColors.Infrastructure.DB;

internal static class IConfigurationExtentions
{
    public static string GetDatabase(this IConfiguration config, string dbName)
    {
        return config.GetSection("Databases").GetSection(dbName).Value;
    }

    public static string GetDBCollection(this IConfiguration config, string collectionName)
    {
        return config.GetSection("Databases").GetSection("Collections").GetSection(collectionName).Value;
    }
}

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

    public async Task<List<ColorCollectionAndStatus>> GetAllColorCollectionsAsync()
    {
        var allItems = await _colorCollectionsCollection.FindAsync(_ => true);

        return CreateListOfColorcollectionAndStatus(allItems.ToList());
    }

    public List<ColorCollectionAndStatus> GetAllColorCollections()
    {
        var allItems = _colorCollectionsCollection.Find(_ => true);

        return CreateListOfColorcollectionAndStatus(allItems.ToList());
    }

    private static List<ColorCollectionAndStatus> CreateListOfColorcollectionAndStatus(List<ColorCollectionModel> list)
    {
        return list
            .Select(c =>
                (c.Title, c.Colors?.Select(color => new ColorDefinition(color.Name, Color.FromARGB(color.R, color.G, color.B))))
            )
            .Select(c => ColorsCollection.From(c.Item1 ?? "unknown", c.Item2?.ToList() ?? new()))
            .Select(c => ColorCollectionAndStatus.FoundAndLoaded(c)).ToList();
    }

    private IMongoCollection<T> CreateCollectionReference<T>(string collectionName)
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collectionName);
    }
}
