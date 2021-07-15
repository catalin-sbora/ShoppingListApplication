using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.EFPersistence
{
    public class EFPersistenceCosmosContextFactory : IDesignTimeDbContextFactory<ShoppingListDbContext>
    {
        private IConfiguration configuration;
        public EFPersistenceCosmosContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public ShoppingListDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingListDbContext>();
            var connectionKey = configuration["ConnectionKey"];
            var database = configuration["Database"];
            optionsBuilder.UseCosmos(connectionKey, database);
            return new ShoppingListDbContext(optionsBuilder.Options);
        }
    }
}
