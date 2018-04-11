using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Data.Models;

namespace Core.Repositories
{
    public class DatabaseDriver : IDatabaseDriver
    {
        private List<Article> _articles = new List<Article>();

        public Article GetById(int id)
        {
             return _articles.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Article article)
        {
            _articles.Add(article);
        }
    }
}