using e_store_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_store_be.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
