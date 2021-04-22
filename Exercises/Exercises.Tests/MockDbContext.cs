using Exercises.DataLayer;
using Exercises.DataLayer.DbModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Exercises.Tests
{
    public class MockDbContext
    {
        public static DbContextOptions<ExercisesDbContext> GetContextOptions()
        {
            return new DbContextOptionsBuilder<ExercisesDbContext>()
                            .UseInMemoryDatabase("TestDatabase")
                            .Options;
        }
        
        public static void Seed(DbContextOptions<ExercisesDbContext> ContextOptions)
        {
            using (var context = new ExercisesDbContext(ContextOptions))
            {
                context.Database.EnsureCreated();
                if (context.products.Any() && context.shopperHistories.Any())
                {
                    return;
                }

                var products = LoadJson<Product>(@".\SampleData\products.json");
                var shopperHistories = LoadJson<ShopperHistory>(@".\SampleData\shopperHistories.json");

                context.products.AddRange(products);
                context.SaveChanges();

                shopperHistories.ForEach(sh =>
                {
                    sh.product = context.products.Where(p => p.name == sh.productName).FirstOrDefault();
                    context.shopperHistories.Add(sh);
                    context.SaveChanges();
                });
            }
        }

        private static List<T> LoadJson<T>(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
                return items;
            }
        }
    }
}
