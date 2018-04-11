using System;
using Autofac;
using Core.AutoFac;
using Infrastructure;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
            Program.Run();
		    Program.Exit();
		}

	    private static void Run()
	    {
	        try
	        {
                Shop.Create()
	                .SellArticleForBestPrice(1, 459, 10, new DateTime(2018, 3, 30))
	                .ShowArticle(1)
	                .ShowArticle(12);
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine(ex);
	        }
        }

	    private static void Exit()
	    {
	        Console.WriteLine("Press eny key to exit.");
	        Console.ReadKey();
        }
	}
}