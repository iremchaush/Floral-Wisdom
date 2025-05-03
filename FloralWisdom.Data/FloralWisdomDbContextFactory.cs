using FloralWisdom.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;

namespace FloralWisdom.Data.Context;

public class FloralWisdomDbContextFactory : IDesignTimeDbContextFactory<FloralWisdomDbContext>
{
    public FloralWisdomDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FloralWisdomDbContext>();

        optionsBuilder.UseSqlServer("Server=DESKTOP-00QPLV4\\SQLEXPRESS02;Database=FloralWisdom;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");


		return new FloralWisdomDbContext(optionsBuilder.Options);
    }
}
