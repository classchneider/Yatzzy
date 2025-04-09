using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMPlayerHuman : VMPlayer
    {
        public VMPlayerHuman(Player player) : base(player)
        {
        }
        public override HoldInfo[]? Holds(int[] Results, VMScoreboard Scoreboard, int diceCount)
        {
            return null;
        }
        public override PropertyInfo? SelectScore(List<(string property, int value)> suggestions, int[] Results, VMScoreboard Scoreboard, int diceCount)
        {
            return null;
        }
        public override RollActions Action
        {
            get
            {
                return RollActions.AskUser;
            }
        }

    }

}
