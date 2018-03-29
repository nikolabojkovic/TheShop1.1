using System;

namespace TheShop
{
    public interface IShopInterface
    {
        Article OrderArticle(int id, int maxExpectedPrice);

        void SellArticle(Article article, int buyerId, DateTime sellingTime);

        Article GetById(int id);
    }
}
