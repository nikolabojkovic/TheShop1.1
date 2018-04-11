using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Data.Models;

namespace Core.Services
{
	public class ShopService : IShopService
    { 
        private readonly IDatabaseDriver _databaseDriver;
		private readonly ILogger _logger;

        private List<Supplier> _supliers = new List<Supplier>();
		
		public ShopService(
		        IDatabaseDriver databaseDriver,
		        ILogger logger
            )
		{
		    _databaseDriver = databaseDriver;
		    _logger = logger;

            CreateSupliers();
		}

	    public Article OrderArticle(int id, int maxExpectedPrice)
	    {
	        Supplier supplier = _supliers.LastOrDefault(x => x.IsArticleInInventory(id) && x.GetArticle(id).ArticlePrice <= maxExpectedPrice);

            if (supplier == null)
                throw new Exception("Could not order article. Article not found");

            return supplier.GetArticle(id);
        }

	    public void SellArticle(Article article, int buyerId, DateTime sellingTime)
	    {
	        if (article == null)
	            throw new Exception("Could not sell article. Article not found.");

	        _logger.Debug("Trying to sell article with id=" + article.Id);

            article.Sell(buyerId, sellingTime);

	        _databaseDriver.Save(article);
	        _logger.Info("Article with id=" + article.Id + " is sold on price " + article.ArticlePrice + ".");
        }

        public Article GetById(int id)
		{
			var article = _databaseDriver.GetById(id);

            if (article == null)
                throw new Exception("Could not find article with ID: " + id);

		    return article;
		}

	    private void CreateSupliers()
	    {
	        _supliers = new List<Supplier> {
	            Supplier.Create(new List<Article>
	            {
	                Article.Create(
	                    1,
	                    "Article from supplier1",
	                    458
	                )
	            }),

	            Supplier.Create(new List<Article>
	            {
	                Article.Create(
	                    1,
	                    "Article from supplier2",
	                     459
	                )
	            }),
	            Supplier.Create(new List<Article>
	            {
	                Article.Create(
	                    1,
	                    "Article from supplier3",
	                    460
	                )
	            })
	        };
	    }
    }

	//in memory implementation
}
