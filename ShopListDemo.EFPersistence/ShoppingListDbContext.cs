using Microsoft.EntityFrameworkCore;
using ShopListDemo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListDemo.EFPersistence
{
    public class ShoppingListDbContext: DbContext
    {
        public DbSet<ShoppingAppUser> AppUsers { get; set; }
        /*public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        */
        public ShoppingListDbContext(DbContextOptions options):base(options)
        {               
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ShoppingAppUser>()
                        .ToContainer("ShoppingUsers")
                        .OwnsMany(u => u.ShoppingLists,
                            list =>
                            {
                                list.WithOwner()
                                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
                                
                                list.OwnsMany(item => item.Items)
                                    .WithOwner()
                                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);

                            }
                        );

        }



    }
}
