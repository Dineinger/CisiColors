using Cisi.CisiColors.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Cisi.CisiColors.Infrastructure.DB;

public class ColorDataAccess : IColorDataAccess
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _collectionName;

    private readonly IMongoCollection<ColorModel> _colorCollection;

    public ColorDataAccess(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("MongoDBDefault");
        _databaseName = _config.GetSection("Databases").GetSection("Colors").Value;
        _collectionName = _config.GetSection("Databases").GetSection("Collections").GetSection("Colors").Value;

        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);
        _colorCollection = db.GetCollection<ColorModel>(_collectionName);
    }

    public async Task<List<ColorModel>> GetAllColorsAsync()
    {
        var allItems = await _colorCollection.FindAsync(_ => true);

        _colorCollection.InsertOne(new ColorModel(0xff, 0x00, 0x64));

        return allItems.ToList();
    }

    public List<ColorModel> GetAllColors()
    {
        var allItems = _colorCollection.Find(_ => true);

        return allItems.ToList();
    }
}
