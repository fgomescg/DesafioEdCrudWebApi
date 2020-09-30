using Entities;
using Entities.Models;

namespace DesafioEdCRUD.Integration.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(RepositoryContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Book.AddRange(
                new Book() { Title = "Teste Title 1", Company = "Test Company1", PublishYear = "1999", Edition = 1, Value = 1.99m },
                new Book() { Title = "Teste Title 2", Company = "Test Company2", PublishYear = "2000", Edition = 1, Value = 2.99m },
                new Book() { Title = "Teste Title 3", Company = "Test Company3", PublishYear = "2001", Edition = 1, Value = 3.99m },
                new Book() { Title = "Teste Title 4", Company = "Test Company4", PublishYear = "2002", Edition = 1, Value = 4.99m }
            );

            context.SaveChanges();
        }
    }
}
