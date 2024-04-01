using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMPlayerScore
    {
        public VMPlayerScore(VMPlayer vmPlayer, VMScoreboard vmScoreboard, PlayerScore playerScore)
        {
            VMPlayer = vmPlayer;
            VMScoreboard = vmScoreboard;
            PlayerScore = playerScore;
        }
        private PlayerScore PlayerScore { get; set; }

        public VMPlayer VMPlayer { get; set; }
        public VMScoreboard VMScoreboard { get; set; }

        public HoldInfo[]? Holds(int[] Results, int diceCount)
        {
            return VMPlayer.Holds(Results, VMScoreboard, diceCount);
        }

        public PropertyInfo SelectScore(List<(string property, int value)> suggestions, int[] Results, int diceCount)
        {
            return VMPlayer.SelectScore(suggestions, Results, VMScoreboard, diceCount);
        }

        // Remove current PlayerScore from game (~delete this playerscore)
        public void RemoveFromGame(Game game)
        {
            game.PlayerScores.Remove(PlayerScore);
        }

        public override string ToString()
        {
            return $"{VMPlayer.Name}: {VMScoreboard.Sum2}";
        }
    }
}
