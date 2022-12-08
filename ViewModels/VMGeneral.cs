using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMYatzyGeneral
    {
        public ObservableCollection<VMPlayer> Players { get; set; } = new();
        public Model Model { get; set; } = new Model();

        public VMPlayer AddPlayer(string name)
        {
            Player player = new Player()
            {
                Name = name,
            };
            VMPlayer vmPlayer = new VMPlayer(player);
            Players.Add(vmPlayer);
            return vmPlayer;
        }

        public void DemoVersion()
        {

            Scoreboard result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 15,
                Fours = 12,
                Threes = 9,
                Twos = 6,
                Ones = 3,
                Chance = 17,
                FourSame = 16,
                GreatStraight = 0,
                House = 0,
                LittleStraight = 1 + 2 + 3 + 4 + 5,
                Pair = 10,
                ThreeSame = 12,
                TwoPairs = 18,
                Yatzy = 50,
            };
            Model.Scoreboards.Add(result);
            Player player = new Player()
            {
                Result = result,
                Name = "Claus",
            };
            Model.Players.Add(player);
            result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 20,
                Fours = 8,
                Threes = 6,
                Twos = 6,
                Ones = 3,
                Chance = 24,
                FourSame = 20,
                GreatStraight = 2 + 3 + 4 + 5 + 6,
                House = 3 * 5 + 2 * 6,
                LittleStraight = 0,
                Pair = 12,
                ThreeSame = 15,
                TwoPairs = 22,
                Yatzy = 0,
            };
            Model.Scoreboards.Add(result);
            player = new Player()
            {
                Result = result,
                Name = "Grisling",
            };
            Model.Players.Add(player);
            result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 20,
                Fours = 12,
                Threes = 12,
                Twos = 6,
                Ones = 3,
                Chance = 15,
                FourSame = 16,
                GreatStraight = 0,
                House = 3 * 5 + 2 * 4,
                LittleStraight = 0,
                Pair = 10,
                ThreeSame = 18,
                TwoPairs = 0,
                Yatzy = 0,
            };
            Model.Scoreboards.Add(result);
            player = new Player()
            {
                Result = result,
                Name = "Ninka",
            };
            Model.Players.Add(player);
        }
    }

}
