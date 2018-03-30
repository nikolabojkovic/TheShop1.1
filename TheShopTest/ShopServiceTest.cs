using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheShop.Data;
using TheShop.Interfaces;
using TheShop.Models;
using TheShop.Services;

namespace TheShopTest
{
    [TestClass]
    public class ShopServiceTest
    {
        private IShopService _shopService;
        private IDatabaseDriver _dbDriver;
        private ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbDriver = new DatabaseDriver();
            _logger = new Logger();
            _shopService = new ShopService(_dbDriver, _logger);
        }


        [TestMethod]
        public void TestOrderArticle_ShouldReturnAnArticleFromSupplier()
        {
            var expectedArticle = new Article
            {
                Id = 1,
                ArticlePrice = 459,
                NameOfArticle = "Article from supplier2",
            };


            var actualArtical = _shopService.OrderArticle(1, 459);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Could not order article")]
        public void TestOrderArticleThatNotExists_ShouldFailToReturnAnArticleFromSupplier()
        {
            _shopService.OrderArticle(1, 20);
        }

        [TestMethod]
        public void TestSellArticle_ShoulMarkAnArticleAsSold()
        {
            var expectedArticle = new Article
            {
                Id = 1,
                ArticlePrice = 458,
                NameOfArticle = "Article from supplier1",
                IsSold = true,
                BuyerUserId = 10
            };

            var expectedSoldDate = new DateTime(2018, 3, 30);

            var actualArtical = _shopService.OrderArticle(1, 458);
            _shopService.SellArticle(actualArtical, 10, expectedSoldDate);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
            Assert.AreEqual(expectedArticle.IsSold, actualArtical.IsSold);
            Assert.AreEqual(expectedArticle.BuyerUserId, actualArtical.BuyerUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Could not order article")]
        public void TestSellArticle_ShoulFailToSellAnArticle()
        {
            _shopService.SellArticle(null, 10, new DateTime(2018, 3, 30));
        }

        [TestMethod]
        public void TestGetArticleById_ShoulReturnArticle()
        {
            var expectedArticle = new Article
            {
                Id = 1,
                ArticlePrice = 458,
                NameOfArticle = "Article from supplier1",
                IsSold = true,
                BuyerUserId = 10
            };

            _shopService.SellArticle(expectedArticle, 10, new DateTime(2018, 3, 30));

            var actualArtical = _shopService.GetById(1);

            Assert.AreEqual(expectedArticle.Id, actualArtical.Id);
            Assert.AreEqual(expectedArticle.ArticlePrice, actualArtical.ArticlePrice);
            Assert.AreEqual(expectedArticle.NameOfArticle, actualArtical.NameOfArticle);
            Assert.AreEqual(expectedArticle.IsSold, actualArtical.IsSold);
            Assert.AreEqual(expectedArticle.BuyerUserId, actualArtical.BuyerUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Could not find article with ID: 12")]
        public void TestGetArticleById_ShoulFailToReturnArticle()
        {
            var expectedArticle = new Article
            {
                Id = 1,
                ArticlePrice = 458,
                NameOfArticle = "Article from supplier1",
                IsSold = true,
                BuyerUserId = 10
            };

            _shopService.SellArticle(expectedArticle, 10, new DateTime(2018, 3, 30));

            _shopService.GetById(12);
        }
    }
}
