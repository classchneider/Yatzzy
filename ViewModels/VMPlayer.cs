using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMPlayer : INotifyPropertyChanged
    {
        private Player Player { get; set; }

        public VMPlayer(Player player)
        {
            this.Player = player;
        }

        public string Name
        {
            get
            {
                return Player.Name;
            }
        }

        public void AddToPlayerScore(PlayerScore playerScore)
        {
            playerScore.Player = Player;
            //playerScore.Scoreboard = new Scoreboard();
        }

        //public void RemoveFromGame(Game game)
        //{
        //    game.Players.Remove(Player);
        //}

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
