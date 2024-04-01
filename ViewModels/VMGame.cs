using Microsoft.EntityFrameworkCore;
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
        public VMGame(Game game, YatzyModel model)
        {
            this.game = game;
            Model = model;
        }
        private Game game;

        public YatzyModel Model { private get; set; }

        private ObservableCollection<VMPlayerScore> playerScores = new ObservableCollection<VMPlayerScore>();
        public ReadOnlyObservableCollection<VMPlayerScore> PlayerScores { get; private set; }
        public ReadOnlyObservableCollection<VMPlayerScore> PlayerScoresCopy { get; private set; }


        public string Name
        {
            get
            {
                return game.Name;
            }
        }

        public int NextPlayerScoreIndex
        {
            get => game.NextPlayerScoreIndex;
            set
            {
                game.NextPlayerScoreIndex = value;
            }
        }

        public VMPlayerScore PlayerScore
        {
            get
            {
                if (PlayerScores != null && PlayerScores.Count > NextPlayerScoreIndex && NextPlayerScoreIndex >= 0)
                {
                    return PlayerScores[NextPlayerScoreIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public VMGame CloneEmpty()
        {
            Game newGame = new Game()
            {
                Name = this.Name,
            };

            VMGame newVMGame = new VMGame(newGame, Model);
            foreach (VMPlayerScore score in PlayerScores)
            {
                newVMGame.AddPlayer(score.VMPlayer);
            }
            Model.Games.Add(newGame);
            Model.SaveChanges();

            return newVMGame;
        }

        public bool CurrentPlayerIsGameOver
        {
            get
            {
                if (PlayerScore != null && PlayerScore.VMScoreboard != null)
                {
                    return PlayerScore.VMScoreboard.IsFull;
                }
                else
                {
                    return false;
                }
            }
        }

        public VMPlayerScore? LeadingPlayer
        {
            get
            {
                VMPlayerScore candidate = null;
                foreach (VMPlayerScore score in PlayerScores)
                {
                    if (candidate == null || score.VMScoreboard.Sum2 > candidate.VMScoreboard.Sum2)
                    {
                        candidate = score;
                    }
                }
                return candidate;
            }
        }

        public void LoadPlayerScores()
        {
            playerScores.Clear();
            foreach (PlayerScore playerScore in game.PlayerScores)
            {
                VMPlayer vmPlayer = VMPlayer.CreateVMPlayer(playerScore.Player);
                VMScoreboard vmScoreboard = new VMScoreboard(playerScore.Scoreboard);
                VMPlayerScore vmPlayerScore = new VMPlayerScore(vmPlayer, vmScoreboard, playerScore);
                playerScores.Add(vmPlayerScore);
            }
            PlayerScores = new ReadOnlyObservableCollection<VMPlayerScore>(playerScores);
            PlayerScoresCopy = new ReadOnlyObservableCollection<VMPlayerScore>(playerScores);

            RaisePropertyChanged(nameof(PlayerScores));
            RaisePropertyChanged(nameof(PlayerScoresCopy));
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
