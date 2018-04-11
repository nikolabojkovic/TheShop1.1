using Data.Models;

namespace Core.Interfaces
{
    public interface IDatabaseDriver
    {
        Article GetById(int id);

        void Save(Article article);
    }
}