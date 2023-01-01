using System;
using System.Collections.Generic;
using System.Linq;
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

        // Remove current PlayerScore from game (~delete this playerscore)
        public void RemoveFromGame(Game game)
        {
            game.PlayerScores.Remove(PlayerScore);
        }
    }
}
