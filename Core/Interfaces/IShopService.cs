using System;
using Data.Models;

namespace Core.Interfaces
{
    public interface IShopService
    {
        Article OrderArticle(int id, int maxExpectedPrice);

        void SellArticle(Article article, int buyerId, DateTime sellingTime);

        Article GetById(int id);
    }
}
