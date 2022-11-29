using Microsoft.EntityFrameworkCore;

namespace YatzyRepository
{
    public class Model : DbContext
    {
        public DbSet<Player> Player { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EF_Yatzy; Trusted_Connection = True; ");
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public int sixes { get; set; }
        public int fives { get; set; }
        public int fours { get; set; }
        public int threes { get; set; }
        public int twos { get; set; }
        public int ones { get; set; }
    }
}