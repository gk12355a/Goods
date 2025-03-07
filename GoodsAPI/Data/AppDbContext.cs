using Microsoft.EntityFrameworkCore;
using GoodsAPI.Models;

namespace GoodsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HangHoa> HangHoas  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangHoa>().HasKey(hh => hh.MaHangHoa);
        }
    }
}
