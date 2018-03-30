using System;
using TheShop.Data;
using TheShop.Services;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            var dbDriver = new DatabaseDriver();
		    var logger = new Logger();
            var shopService = new ShopService(dbDriver, logger);
            
			try
			{
			    const int articleId = 1;
			    const int maxExpectedPrice = 20;
			    const int buyerId = 10;

                var orderdArticle = shopService.OrderArticle(articleId, maxExpectedPrice);
                shopService.SellArticle(orderdArticle, buyerId, DateTime.Now);

                Console.WriteLine("Found article with ID: " + shopService.GetById(1).Id);
			    Console.WriteLine("Found article with ID: " + shopService.GetById(12).Id);
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.ReadKey();
		}
	}
}