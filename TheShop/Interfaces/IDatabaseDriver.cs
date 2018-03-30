using TheShop.Models;

namespace TheShop.Interfaces
{
    public interface IDatabaseDriver
    {
        Article GetById(int id);

        void Save(Article article);
    }
}