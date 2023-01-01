using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMGame : INotifyPropertyChanged
    {
        public VMGame(Game game)
        {
            this.game = game;
            PlayerScores = new ReadOnlyObservableCollection<VMPlayerScore>(playerScores);
        }
        private Game game;
        
        private ObservableCollection<VMPlayerScore> playerScores = new ObservableCollection<VMPlayerScore>();
        public ReadOnlyObservableCollection<VMPlayerScore> PlayerScores {get; private set; }


        public string Name
        {
            get
            {
                return game.Name;
            }
        }

        public void AddPlayer(VMPlayer vmPlayer)
        {
            // Scoreboard for player in game
            Scoreboard scoreboard = new Scoreboard();
            VMScoreboard vmScoreboard = new VMScoreboard(scoreboard);

            // Create PlayerScore
            PlayerScore playerScore = new PlayerScore()
            {
                Scoreboard = scoreboard,
            };

            // Add player to PlayerScore
            vmPlayer.AddToPlayerScore(playerScore);

            // Create VMPlayerScore
            VMPlayerScore vmScore = new VMPlayerScore(vmPlayer, vmScoreboard, playerScore);

            // Add to ViewModel
            playerScores.Add(vmScore);

            // Add to model
            game.PlayerScores.Add(playerScore);

            RaisePropertyChanged(nameof(PlayerScores));
        }

        public void RemovePlayer(VMPlayer vmPlayer)
        {
            // We need to find vmPlayer in PlayerScores
            VMPlayerScore playerScore = playerScores.First(x => x.VMPlayer == vmPlayer);

            // Remove from viewModel
            playerScores.Remove(playerScore);

            // Remove from model
            playerScore.RemoveFromGame(game);

            RaisePropertyChanged(nameof(PlayerScores));
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
