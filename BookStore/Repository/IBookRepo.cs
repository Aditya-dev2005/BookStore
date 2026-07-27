using BookStore.Models;

namespace BookStore.Repository
{
    public interface IBookRepo
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookById(int id);
        Task<List<Book>> SearchByName(string name);
        Task<List<Book>> SearchById(int id);
        Task<int> Insert(Book obj);
        Task<int> Update(Book obj);
        Task<int> Delete(int id);
    }
}
