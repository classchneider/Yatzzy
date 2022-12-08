using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace YatzyRepository
{
    public class Model : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Scoreboard> Scoreboards { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EF_Yatzy; Trusted_Connection = True; ");
        }

        public ObservableCollection<Player> PlayerList
        {
            get
            {
                if (Players.Count() == 0)
                {
                    Players.Load();
                }
                return Players.Local.ToObservableCollection();
            }
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Scoreboard Result { get; set; }
    }

    public class Scoreboard
    {
        public int Id { get; set; }
        public int? Sixes { get; set; }
        public int? Fives { get; set; }
        public int? Fours { get; set; }
        public int? Threes { get; set; }
        public int? Twos { get; set; }
        public int? Ones { get; set; }
        public int Sum
        {
            get
            {
                return Sixes!=null?Sixes.Value:0 + 
                    Fives != null ? Fives.Value : 0 + 
                    Fours != null ? Fours.Value : 0 + 
                    Threes != null ? Threes.Value : 0 + 
                    Twos != null ? Twos.Value : 0 + 
                    Ones != null ? Ones.Value : 0;
            }
        }
        public int Bonus
        {
            get
            {
                return (Sum > 3 * (1 + 2 + 3 + 4 + 5 + 6)) ? 50 : 0;
            }
        }

        public int? Pair { get; set; }
        public int? TwoPairs { get; set; }
        public int? ThreeSame { get; set; }
        public int? FourSame { get; set; }
        public int? LittleStraight { get; set; }
        public int? GreatStraight { get; set; }
        public int? House { get; set; }
        public int? Chance { get; set; }
        public int? Yatzy { get; set; }
        public int Sum2
        {
            get
            {
                return Sum + 
                    Bonus + 
                    Pair != null?Pair.Value:0 + 
                    TwoPairs != null? TwoPairs.Value:0 + 
                    ThreeSame != null? ThreeSame.Value:0 + 
                    FourSame != null?FourSame.Value:0 + 
                    LittleStraight != null?LittleStraight.Value:0 + 
                    GreatStraight != null?GreatStraight.Value:0 + 
                    House != null? House.Value : 0 + 
                    Chance != null? Chance.Value : 0 + 
                    Yatzy != null? Yatzy.Value : 0;
            }
        }
    }
}