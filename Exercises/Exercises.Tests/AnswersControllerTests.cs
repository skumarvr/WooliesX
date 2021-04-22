using AutoMapper;
using Exercises.BusinessLayer.ViewModels;
using Exercises.Controllers;
using Exercises.DataLayer;
using Exercises.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit.Sdk;

namespace Exercises.Tests
{
    [TestClass]
    public class AnswersControllerTests
    {
        private DbContextOptions<ExercisesDbContext> ContextOptions { get; }

        private IConfigurationProvider MapperConfiguration;

        private ExercisesDbContext DbContext;

        private ProductRepository productRepository;

        public AnswersControllerTests()
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
        public void TestGetUserToken()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetUserToken();

            Assert.IsInstanceOfType(result, typeof(UserTokenResponse));
            Assert.AreEqual(result.name, "Sajith Kumar");
            Assert.AreEqual(result.token, "320dccaf-a669-458e-a45f-1375d7f95628");
        }

        [TestMethod]
        public void TestGetProducts_sortOption_Low()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetProducts(BusinessLayer.Entities.SortOptions.Low).Result;

            Assert.IsInstanceOfType(result, typeof(ProductsResponse[]));
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0].price, 5);
            Assert.AreEqual(result[1].price, 10.99);
            Assert.AreEqual(result[2].price, 99.99);
            Assert.AreEqual(result[3].price, 101.99);
            Assert.AreEqual(result[4].price, 999999999999);
        }

        [TestMethod]
        public void TestGetProducts_sortOption_High()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetProducts(BusinessLayer.Entities.SortOptions.High).Result;

            Assert.IsInstanceOfType(result, typeof(ProductsResponse[]));
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0].price, 999999999999);
            Assert.AreEqual(result[1].price, 101.99);
            Assert.AreEqual(result[2].price, 99.99);
            Assert.AreEqual(result[3].price, 10.99);
            Assert.AreEqual(result[4].price, 5);
        }

        [TestMethod]
        public void TestGetProducts_sortOption_Ascending()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetProducts(BusinessLayer.Entities.SortOptions.Ascending).Result;

            Assert.IsInstanceOfType(result, typeof(ProductsResponse[]));
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0].name, "Test Product A");
            Assert.AreEqual(result[1].name, "Test Product B");
            Assert.AreEqual(result[2].name, "Test Product C");
            Assert.AreEqual(result[3].name, "Test Product D");
            Assert.AreEqual(result[4].name, "Test Product F");
        }

        [TestMethod]
        public void TestGetProducts_sortOption_Descending()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetProducts(BusinessLayer.Entities.SortOptions.Descending).Result;

            Assert.IsInstanceOfType(result, typeof(ProductsResponse[]));
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0].name, "Test Product F");
            Assert.AreEqual(result[1].name, "Test Product D");
            Assert.AreEqual(result[2].name, "Test Product C");
            Assert.AreEqual(result[3].name, "Test Product B");
            Assert.AreEqual(result[4].name, "Test Product A");
        }

        [TestMethod]
        public void TestGetProducts_sortOption_Recommended()
        {
            var mockLogger = new Mock<ILogger<AnswersController>>();
            var controller = new AnswersController(mockLogger.Object, productRepository, MapperConfiguration);

            var result = controller.GetProducts(BusinessLayer.Entities.SortOptions.Recommended).Result;

            Assert.IsInstanceOfType(result, typeof(ProductsResponse[]));
            Assert.AreEqual(result.Length, 5);
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
