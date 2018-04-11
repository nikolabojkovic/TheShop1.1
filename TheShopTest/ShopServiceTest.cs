using System;
using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Data.Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TheShopTest
{
    [TestFixture]
    public class ShopServiceTest
    {
        private IShopService _shopService;
        private IDatabaseDriver _dbDriver;
        private ILogger _logger;

        [SetUp]
        public void TestInitialize()
        {
            _dbDriver = new DatabaseDriver();
            _logger = new Logger();
            _shopService = new ShopService(_dbDriver, _logger);
        }


        [Test]
        public void TestOrderArticle_ShouldReturnAnArticleFromSupplier()
        {
            var expectedArticle = Article.Create(1, "Article from supplier2", 459);

            var actualArtical = _shopService.OrderArticle(1, 459);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
        }

        [Test]
        public void TestOrderArticleThatNotExists_ShouldFailToReturnAnArticleFromSupplier()
        {
            var ex = Assert.ThrowsException<Exception>(() => _shopService.OrderArticle(1, 20));
            Assert.AreEqual(ex.Message, "Could not order article. Article not found");
        }

        [Test]
        public void TestSellArticle_ShoulMarkAnArticleAsSold()
        {
            // inherit from base model and create test model where you can inistialize data
            // in constructore or some method where initialization will be explicit and descriptive
            var expectedArticle = Article.Create(1, "Article from supplier1", 458);
            expectedArticle.Sell(10, DateTime.Now); 

            var expectedSoldDate = new DateTime(2018, 3, 30);
            var actualArtical = _shopService.OrderArticle(1, 458);
            _shopService.SellArticle(actualArtical, 10, expectedSoldDate);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
            Assert.AreEqual(expectedArticle.IsSold, actualArtical.IsSold);
            Assert.AreEqual(expectedArticle.BuyerUserId, actualArtical.BuyerUserId);
        }

        [Test]
        public void TestSellArticle_ShoulFailToSellAnArticle()
        {
            var ex = Assert.ThrowsException<Exception>(() => _shopService.SellArticle(null, 10, new DateTime(2018, 3, 30)));
            Assert.AreEqual(ex.Message, "Could not sell article. Article not found.");
        }

        [Test]
        public void TestGetArticleById_ShoulReturnArticle()
        {
            var expectedArticle = Article.Create(1, "Article from supplier1", 458);
            expectedArticle.Sell(10, DateTime.Now);

            _shopService.SellArticle(expectedArticle, 10, new DateTime(2018, 3, 30));

            var actualArtical = _shopService.GetById(1);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
            Assert.AreEqual(expectedArticle.IsSold, actualArtical.IsSold);
            Assert.AreEqual(expectedArticle.BuyerUserId, actualArtical.BuyerUserId);
        }

        [Test]
        public void TestGetArticleById_ShoulFailToReturnArticle()
        {
            var ex = Assert.ThrowsException<Exception>(() => _shopService.GetById(12));
            Assert.AreEqual(ex.Message, "Could not find article with ID: 12");
        }
    }
}
