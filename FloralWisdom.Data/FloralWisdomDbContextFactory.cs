using FloralWisdom.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;

namespace FloralWisdom.Data.Context;

public class ArtGalleryDbContextFactory : IDesignTimeDbContextFactory<FloralWisdomDbContext>
{
    public FloralWisdomDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FloralWisdomDbContext>();

        optionsBuilder.UseSqlServer("Server=DESKTOP-4PGVMQP\\SQLEXPRESS;Database=ArtGalleryDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new FloralWisdomDbContext(optionsBuilder.Options);
    }
}
