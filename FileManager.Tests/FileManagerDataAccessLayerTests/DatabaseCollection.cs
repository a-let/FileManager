using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}