using Microsoft.EntityFrameworkCore;

namespace BanSach.Data;

public class BookStoreContext : DbContext
{
    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
    {
    }

    public DbSet<BanSach.Models.Book> Books { get; set; }
}