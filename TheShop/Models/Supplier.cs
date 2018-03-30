using System.Collections.Generic;
using System.Linq;

namespace TheShop.Models
{
    public class Supplier
    {
        public List<Article> Articles { get; private set; } = new List<Article>();

        private Supplier()
        {
        }

        public bool IsArticleInInventory(int id)
        {
            if (Articles.Any(x => x.Id == id && !x.IsSold))
                return true;

            return false;
        }

        public Article GetArticle(int id)
        {
            return Articles.Find(x => x.Id == id);
        }

        public static Supplier Create(List<Article> articles)
        {
            var suplier = new Supplier();
            suplier.Articles.AddRange(articles);
            return suplier;
        }
    }
}