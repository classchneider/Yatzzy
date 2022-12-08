using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMPlayer
    {
        Player player;

        public VMPlayer(Player player)
        {
            this.player = player;
        }

        public string Name 
        {
            get { return player.Name; }
        }
        public Scoreboard Result
        {
            get { return player.Result; }
        }
    }
}
