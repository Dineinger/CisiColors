using Cisi.CisiColors.DBModels;
using Cisi.CisiColors.Infrastructure.Models;
using Dotgem.Colors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace Cisi.CisiColors.Infrastructure.DB;

public sealed partial class ColorDataAccess : IColorDataAccess
{
    private readonly IConfiguration _config;
    private readonly ILogger<ColorDataAccess> _logger;
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _colors_collectionName;
    private readonly string _colorCollections_collectionName;

    private readonly IMongoCollection<ColorModel> _colorCollection;
    private readonly IMongoCollection<ColorCollectionModel> _colorCollectionsCollection;

    public ColorDataAccess(IConfiguration config, ILogger<ColorDataAccess> logger)
    {
        _config = config;
        _logger = logger;
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

    [GeneratedRegex(@"[A-z ]+")]
    private partial Regex ColorCollectionName();

    [Obsolete("not id but name")]
    public async Task<ColorCollectionAndStatus> GetColorCollectionAsync(string? name)
    {
        if (name is null)
        {
            return ColorCollectionAndStatus.NotFound();
        }

        if (ColorCollectionName().IsMatch(name) == false)
        {
            return ColorCollectionAndStatus.NotFound();
        }

        var items = await _colorCollectionsCollection.FindAsync(x => x.Title == name);
        var list = items.ToList();

        if (list.Count == 0)
        {
            return ColorCollectionAndStatus.NotFound();
        }
        return CreateColorCollectionAndStatus(list[0]);
    }

    private IReadOnlyList<ColorCollectionAndStatus> CreateListOfColorcollectionAndStatus(List<ColorCollectionModel> list)
    {
        List<ColorCollectionAndStatus> result = new();
        foreach (var collectionFromDB in list)
        {
            result.Add(CreateColorCollectionAndStatus(collectionFromDB));
        }
        return result;
    }

    private ColorCollectionAndStatus CreateColorCollectionAndStatus(ColorCollectionModel value)
    {
        List<ColorDefinition> colors = CreateColorDefinitions(value.Colors);

        var colorCollection = ColorsCollection.From(
            value.Title ?? "unknown collection",
            colors);

        return ColorCollectionAndStatus.FoundAndLoaded(colorCollection);
    }

    private List<ColorDefinition> CreateColorDefinitions(List<ColorModel>? colorsFromDB)
    {
        List<ColorDefinition> colors = new();
        if (colorsFromDB is not null)
        {
            foreach (var colorInfo in colorsFromDB)
            {
                var color = Color.FromARGB(colorInfo.R, colorInfo.G, colorInfo.B);

                string? newId = null;
                if (colorInfo.Id is null)
                {
                    newId = ObjectId.GenerateNewId().ToString();
                    _logger.LogError("Color {color} has no id when it was loaded from the database. New id: {id}", color.ToString(), newId);
                }
                string? newName = null;
                if (colorInfo.Name is null)
                {
                    newName = Guid.NewGuid().ToString();
                    _logger.LogWarning("Color {color} has no name when it was loaded from the database. New name: {name}", color.ToString(), newName);
                }

                colors.Add(new ColorDefinition(
                    colorInfo.Id ?? newId ?? throw new InvalidOperationException(),
                    colorInfo.Name ?? newName ?? throw new InvalidOperationException(),
                    color));
            }
        }

        return colors;
    }

    private IMongoCollection<T> CreateCollectionReference<T>(string collectionName)
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        return db.GetCollection<T>(collectionName);
    }
}
