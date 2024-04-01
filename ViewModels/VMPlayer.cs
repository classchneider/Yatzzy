using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;
using YatzyRepository.Migrations;

namespace ViewModels
{
    public enum RollActions { Roll, Register, AskUser }

    public abstract class VMPlayer : INotifyPropertyChanged
    {
        private Player Player { get; set; }

        public VMPlayer(Player player)
        {
            this.Player = player;
        }

        public abstract PropertyInfo SelectScore(List<(string property, int value)> suggestions, int[] Results, VMScoreboard Scoreboard, int diceCount);

        public abstract HoldInfo[]? Holds(int[] Results, List<(string property, int value)> suggestions, VMScoreboard Scoreboard, int diceCount);

        public abstract RollActions Action { get; }
        public static VMPlayer CreateVMPlayer(Player player)
        {
            if (player is HumanPlayer)
            {
                return new VMPlayerHuman(player);
            }
            else if (player is AIRollPlayer)
            {
                return new VMPlayerComputer(player);
            }
            else
            {
                throw new ArgumentException($"Player must be {nameof(HumanPlayer)} or {nameof(AIRollPlayer)}", "CreateVMPlayer Error");
            }
        }

        public virtual string Name
        {
            get
            {
                return Player.Name;
            }
        }

        public void AddToPlayerScore(PlayerScore playerScore)
        {
            playerScore.Player = Player;
        }

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
