using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace YatzyRepository
{
    public class Model : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Scoreboard> Scoreboards { get; set; }
        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerScore> PlayerScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EF_Yatzy; Trusted_Connection = True; ");

            //Players.Load();
            //Scoreboards.Load();
            //Games.Load();
            //PlayerScores.Load();
        }

        //public ObservableCollection<Player> PlayerList
        //{
        //    get
        //    {
        //        if (Players.Count() == 0)
        //        {
        //            Players.Load();
        //        }
        //        return Players.Local.ToObservableCollection();
        //    }
        //}
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Scoreboard
    {
        public int Id { get; set; }
        public int? Ones { get; set; }
        public int? Twos { get; set; }
        public int? Threes { get; set; }
        public int? Fours { get; set; }
        public int? Fives { get; set; }
        public int? Sixes { get; set; }

        public int? Pair { get; set; }
        public int? TwoPairs { get; set; }
        public int? ThreeSame { get; set; }
        public int? FourSame { get; set; }
        public int? LittleStraight { get; set; }
        public int? GreatStraight { get; set; }
        public int? House { get; set; }
        public int? Chance { get; set; }
        public int? Yatzy { get; set; }
    }

    public class Game
    {
        //public Game()
        //{
        //    NextPlayserScoreIndex = 0;
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NextPlayerScoreIndex { get; set; } = 0;
        public List<PlayerScore> PlayerScores { get; set; } = new List<PlayerScore>();
    }

    // Bridge "table"
    // Implements many-to-many relation between Player and Scoreboard
    public class PlayerScore
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Scoreboard Scoreboard { get; set; }
    }
}