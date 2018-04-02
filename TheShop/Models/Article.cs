using System;

namespace TheShop.Models
{
    public class Article
    {
        public int Id { get; private set; }

        public string NameOfArticle { get; private set; }

        public int ArticlePrice { get; private set; }

        public bool IsSold { get; private set; }

        public DateTime SoldDate { get; private set; }

        public int BuyerUserId { get; private set; }

        public void Sell(int buyerId, DateTime sellingTime)
        {
            IsSold = true;
            SoldDate = sellingTime;
            BuyerUserId = buyerId;
        }

        public static Article Create(int id, string nameOfArticle, int articlePrice)
        {
            return new Article
            {
                Id = id,
                NameOfArticle = nameOfArticle,
                ArticlePrice = articlePrice
            };
        }
    }
}