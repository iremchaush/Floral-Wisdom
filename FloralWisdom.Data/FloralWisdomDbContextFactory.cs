using FloralWisdom.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;

namespace FloralWisdom.Data.Context;

public class ArtGalleryDbContextFactory : IDesignTimeDbContextFactory<FloralWisdomDbContext>
{
    public FloralWisdomDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FloralWisdomDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=FloralWisdom;User Id=sa;Password=#AniBonbon128;TrustServerCertificate=True;");


		return new FloralWisdomDbContext(optionsBuilder.Options);
    }
}
