using Microsoft.EntityFrameworkCore;
using BookStore.DAL;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly BookStoreDbContext _context;

        public BookRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<List<Book>> SearchByName(string name)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Book>> SearchById(int id)
        {
            return await _context.Books
                .Where(b => b.BookId == id)
                .ToListAsync();
        }

        public async Task<int> Insert(Book obj)
        {
            await _context.Books.AddAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Book obj)
        {
            _context.Books.Update(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return 0;
            }
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }
    }
}
