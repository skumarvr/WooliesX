using AutoMapper;
using Exercises.BusinessLayer.Services;
using Exercises.BusinessLayer.ViewModels;
using Exercises.DataLayer;
using Exercises.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exercises.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private DbContextOptions<ExercisesDbContext> ContextOptions { get; }

        private IConfigurationProvider MapperConfiguration;

        private ExercisesDbContext DbContext;

        private ProductRepository productRepository;

        public ProductServiceTests()
        {
            ContextOptions = MockDbContext.GetContextOptions();
            MockDbContext.Seed(ContextOptions);
            MapperConfiguration = new MapperConfiguration(mc => {
                mc.AddProfile(new DataLayer.Profiles.ProductProfile());
                mc.AddProfile(new BusinessLayer.Profiles.ProductProfile());
            }).CreateMapper().ConfigurationProvider;
            DbContext = new ExercisesDbContext(ContextOptions);
            productRepository = new ProductRepository(DbContext, MapperConfiguration);
        }

        [TestMethod]
        public async Task Test_GetProductsSortedByNameAsc()
        {
            var productService = new ProductService(productRepository, MapperConfiguration);

            var result = await productService.GetProducts(BusinessLayer.Entities.SortOptions.Ascending);
            Assert.IsInstanceOfType(result, typeof(List<ProductsResponse>));
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
            var productService = new ProductService(productRepository, MapperConfiguration);

            var result = await productService.GetProducts(BusinessLayer.Entities.SortOptions.Descending);
            Assert.IsInstanceOfType(result, typeof(List<ProductsResponse>));
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
            var productService = new ProductService(productRepository, MapperConfiguration);

            var result = await productService.GetProducts(BusinessLayer.Entities.SortOptions.Low);
            Assert.IsInstanceOfType(result, typeof(List<ProductsResponse>));
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
            var productService = new ProductService(productRepository, MapperConfiguration);

            var result = await productService.GetProducts(BusinessLayer.Entities.SortOptions.High);
            Assert.IsInstanceOfType(result, typeof(List<ProductsResponse>));
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
            var productService = new ProductService(productRepository, MapperConfiguration);

            var result = await productService.GetProducts(BusinessLayer.Entities.SortOptions.Recommended);
            Assert.IsInstanceOfType(result, typeof(List<ProductsResponse>));
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
