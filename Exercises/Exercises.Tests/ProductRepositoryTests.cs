using AutoMapper;
using Exercises.DataLayer;
using Exercises.DataLayer.Profiles;
using Exercises.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercises.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private DbContextOptions<ExercisesDbContext> ContextOptions { get; }

        private IConfigurationProvider MapperConfiguration;

        private ExercisesDbContext DbContext;

        public ProductRepositoryTests()
        {
            ContextOptions = MockDbContext.GetContextOptions();
            MockDbContext.Seed(ContextOptions);
            MapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new ProductProfile())).CreateMapper().ConfigurationProvider;
            DbContext = new ExercisesDbContext(ContextOptions);
        }

        [TestMethod]
        public async Task Test_GetProductsSortedByNameAsc()
        {
            var productRepository = new ProductRepository(DbContext, MapperConfiguration);

            var result = await productRepository.GetProductsSortedByNameAsc();

            Assert.IsInstanceOfType(result, typeof(List<Exercises.BusinessLayer.Entities.Product>));
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].name, "Test Product A");
            Assert.AreEqual(result[1].name, "Test Product B");
            Assert.AreEqual(result[2].name, "Test Product C");
            Assert.AreEqual(result[3].name, "Test Product D");
            Assert.AreEqual(result[4].name, "Test Product F");
        }

        [TestMethod]
        public async Task Test_GetProductsSortedByNameDesc()
        {
            var productRepository = new ProductRepository(DbContext, MapperConfiguration);

            var result = await productRepository.GetProductsSortedByNameDesc();
            Assert.IsInstanceOfType(result, typeof(List<Exercises.BusinessLayer.Entities.Product>));
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].name, "Test Product F");
            Assert.AreEqual(result[1].name, "Test Product D");
            Assert.AreEqual(result[2].name, "Test Product C");
            Assert.AreEqual(result[3].name, "Test Product B");
            Assert.AreEqual(result[4].name, "Test Product A");
        }

        [TestMethod]
        public async Task Test_GetProductsSortedByPriceAsc()
        {
            var productRepository = new ProductRepository(DbContext, MapperConfiguration);

            var result = await productRepository.GetProductsSortedByPriceAsc();
            Assert.IsInstanceOfType(result, typeof(List<Exercises.BusinessLayer.Entities.Product>));
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].price, 5);
            Assert.AreEqual(result[1].price, 10.99);
            Assert.AreEqual(result[2].price, 99.99);
            Assert.AreEqual(result[3].price, 101.99);
            Assert.AreEqual(result[4].price, 999999999999);
        }

        [TestMethod]
        public async Task Test_GetProductsSortedByPriceDesc()
        {
            var productRepository = new ProductRepository(DbContext, MapperConfiguration);

            var result = await productRepository.GetProductsSortedByPriceDesc();
            Assert.IsInstanceOfType(result, typeof(List<Exercises.BusinessLayer.Entities.Product>));
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].price, 999999999999);
            Assert.AreEqual(result[1].price, 101.99);
            Assert.AreEqual(result[2].price, 99.99);
            Assert.AreEqual(result[3].price, 10.99);
            Assert.AreEqual(result[4].price, 5);
        }

        [TestMethod]
        public async Task Test_GetRecommendedProducts()
        {
            var productRepository = new ProductRepository(DbContext, MapperConfiguration);

            var result = await productRepository.GetRecommendedProducts();
            Assert.IsInstanceOfType(result, typeof(List<Exercises.BusinessLayer.Entities.Product>));
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].name, "Test Product A");
            Assert.AreEqual(result[0].quantity, 6);
            Assert.AreEqual(result[1].name, "Test Product B");
            Assert.AreEqual(result[1].quantity, 5);
            Assert.AreEqual(result[2].name, "Test Product F");
            Assert.AreEqual(result[2].quantity, 4);
            Assert.AreEqual(result[3].name, "Test Product C");
            Assert.AreEqual(result[3].quantity, 3);
            Assert.AreEqual(result[4].name, "Test Product D");
            Assert.AreEqual(result[4].quantity, 0);
        }
    }
}


