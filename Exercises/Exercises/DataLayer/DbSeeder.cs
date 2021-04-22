using Exercises.DataLayer.DbModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exercises.DataLayer
{
    public class DbSeeder
    {
        public static void Seed(ExercisesDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            if (context.products.Any() && context.shopperHistories.Any())
            {
                return;
            }

            var products = LoadJson<Product>(@".\DataLayer\SampleData\products.json");
            var shopperHistories = LoadJson<ShopperHistory>(@".\DataLayer\SampleData\shopperHistories.json");

            context.products.AddRange(products);
            context.SaveChanges();

            shopperHistories.ForEach(sh =>
            {
                sh.product = context.products.Where(p => p.name == sh.productName).FirstOrDefault();
                context.shopperHistories.Add(sh);
                context.SaveChanges();
            });
        }

        #region Loading Sample Data

        private static List<T> LoadJson<T>(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
                return items;
            }
        }
        #endregion
    }
}
