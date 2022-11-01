namespace Cisi.CisiColors.Infrastructure.DB;

internal record struct PreColorCollection(string? Id, string? Title, string? Description, IEnumerable<ColorDefinition>? Colors)
{

}