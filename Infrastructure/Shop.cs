using System;
using Autofac;
using Core.AutoFac;
using Core.Interfaces;
using Core.Services;
using Data.Models;

namespace Infrastructure
{
    public class Shop
    {
        private IShopService _shopService;
        private Article _orderedArticle;

        private Shop()
        {
        }

        public static Shop Create()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DepandencyInjectionModule());
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                Shop shopInstance = new Shop();
                shopInstance._shopService = new ShopService(scope.Resolve<IDatabaseDriver>(), scope.Resolve<ILogger>());
                shopInstance._orderedArticle = null;

                return shopInstance;
            }
        }

        public Shop OrderArticle(int id, int maxExpectedPrice)
        {
            _orderedArticle = _shopService.OrderArticle(id, maxExpectedPrice);
            return this;
        }
        public Shop SellArticle(int buyerId, DateTime sellingDate)
        {
            _shopService.SellArticle(_orderedArticle, buyerId, sellingDate);
            return this;
        }

        public Shop SellArticleForBestPrice(int id, int maxExpectedPrice, int buyerId, DateTime sellingDate)
        {
            OrderArticle(id, maxExpectedPrice);
            SellArticle(buyerId, sellingDate);
            return this;
        }

        public Shop ShowArticle(int id)
        {
            Console.WriteLine("Found article with ID: " + _shopService.GetById(id).Id);
            return this;
        }
    }
}