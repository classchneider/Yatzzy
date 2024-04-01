using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using static System.Formats.Asn1.AsnWriter;

namespace YatzyRepository
{
    public class YatzyModel : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<AIRollPlayer> AIRollPlayers { get; set; }
        public DbSet<HumanPlayer> HumanPlayers { get; set; }

        public DbSet<Scoreboard> Scoreboards { get; set; }
        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerScore> PlayerScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EF_Yatzy; Trusted_Connection = True; ");
        }

    }

}