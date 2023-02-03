using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using YatzyRepository;

namespace ViewModels
{
    public class VMYatzyGeneral : INotifyPropertyChanged
    {
        public VMYatzyGeneral()
        {
            LoadPlayers();
            LoadGames();
            _Players = new ReadOnlyObservableCollection<VMPlayer>(players);
            _VMGames = new ReadOnlyObservableCollection<VMGame>(games);
        }

        public enum GameStates { SetupGame, RunGame, SelectScore, NewTurn, EndGame };

        GameStates gameState;
        public GameStates GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                gameState = value;
            }
        }

        /// 
        /// All players
        /// 
        private ObservableCollection<VMPlayer> players { get; set; } = new();

        private void LoadPlayers()
        {
            foreach (Player p in Model.Players)
            {
                VMPlayer player = new VMPlayer(p);
                players.Add(player);
            }
        }

        private ReadOnlyObservableCollection<VMPlayer> _Players;

        public ReadOnlyObservableCollection<VMPlayer> Players
        {
            get
            {
                return _Players;
            }
        }

        /// 
        /// All games
        /// 
        private ObservableCollection<VMGame> games { get; set; } = new();
        private ReadOnlyObservableCollection<VMGame> _VMGames;

        private void LoadGames()
        {
            if (Model.Games.Local.Count() == 0)
            {
                Model.Games.Load();
            }

            if (Model.PlayerScores.Local.Count() == 0)
            {
                Model.PlayerScores.Load();
            }

            if (Model.Scoreboards.Local.Count() == 0)
            {
                Model.Scoreboards.Load();
            }

            foreach (Game g in Model.Games.Local)
            {
                VMGame game = new VMGame(g, Model);
                game.LoadPlayerScores();
                games.Add(game);
            }
        }

        public VMGame NewGame()
        {
            VMGame game;
            if (CurrentGame != null)
            {
                game = CloneGame();
            }
            else
            {
                Game g = new Game();
                game = new VMGame(g, Model);
            }
            return game;
        }

        public ReadOnlyObservableCollection<VMGame> Games
        {
            get
            {
                return _VMGames;
            }
        }

        /// 
        /// Finished games
        /// 
        public IEnumerable<VMGame> FinishedGames
        {
            get
            {
                return Games.Where(g => g.CurrentPlayerIsGameOver);
            }
        }

        /// 
        /// Unfinished games
        /// 
        public IEnumerable<VMGame> ActiveGames
        {
            get
            {
                return Games.Where(g => !g.CurrentPlayerIsGameOver);
            }
        }

        public VMGame? CurrentGame { get; private set; }

        public VMPlayerScore? CurrentPlayerScore
        {
            get
            {
                if (CurrentGame?.NextPlayerScoreIndex >= 0 && CurrentGame?.NextPlayerScoreIndex < CurrentGame?.PlayerScores.Count)
                {
                    return CurrentGame?.PlayerScores[CurrentGame.NextPlayerScoreIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public VMPlayer? CurrentPlayer => CurrentPlayerScore?.VMPlayer;

        private Model Model { get; set; } = new Model();

        // delegates
        public delegate void StateChangeMethod(GameStates state);
        public delegate bool ConfirmMethod(string question, string title = "Bekræft handling");

        public StateChangeMethod? UIStateChange { private get; set; }
        public ConfirmMethod? UIConfirm { private get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void GameSetup()
        {
            StateChange(GameStates.SetupGame);
        }

        public VMGame CloneGame()
        {
            if (CurrentGame == null)
            {
                throw new ArgumentNullException("No game selected");
            }
            VMGame game = CurrentGame.CloneEmpty();
            games.Add(game);
            RaisePropertyChanged(nameof(ActiveGames));
            return game;
        }

        private void NextPlayer()
        {
            if (CurrentGame != null)
            {
                CurrentGame.NextPlayerScoreIndex = (CurrentGame.NextPlayerScoreIndex + 1) % CurrentGame.PlayerScores.Count;
                Model.SaveChanges();
                RaisePropertyChanged(nameof(CurrentPlayer));
                RaisePropertyChanged(nameof(CurrentPlayerScore));
            }
        }

        public bool IsGameOver
        {
            get
            {
                if (CurrentGame == null || CurrentGame.CurrentPlayerIsGameOver)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void StateChange(GameStates state)
        {

            GameState = state;


            switch (GameState)
            {
                case GameStates.RunGame:
                    break;
                case GameStates.NewTurn:
                    NextPlayer();
                    if (IsGameOver)
                    {
                        GameState = GameStates.EndGame;
                    }
                    break;
                case GameStates.SetupGame:
                    // No action, all is handled in View
                    break;
            }

            // Let UI reflect state change
            UIStateChange?.Invoke(GameState);
        }

        public VMPlayerScore? LeadingPlayer
        {
            get
            {
                return CurrentGame != null ? CurrentGame.LeadingPlayer : null;
            }
        }

        public VMPlayer CreatePlayer(string name)
        {
            Player player = new Player()
            {
                Name = name,
            };
            VMPlayer vmPlayer = new VMPlayer(player);

            // Add to ViewModel
            players.Add(vmPlayer);
            // Add to model
            Model.Players.Add(player);
            Model.SaveChanges();

            // Add to game
            AddPlayer(vmPlayer);
            RaisePropertyChanged(nameof(Players));

            return vmPlayer;
        }

        public bool AddPlayer(VMPlayer player)
        {
            if (CurrentGame == null)
            {
                return false;
            }

            CurrentGame.AddPlayer(player);
            Model.SaveChanges();
            RaisePropertyChanged(nameof(CurrentGame));
            return true;
        }

        public void RemovePlayer(VMPlayer player)
        {
            if (CurrentGame != null)
            {
                CurrentGame.RemovePlayer(player);
                Model.SaveChanges();
                RaisePropertyChanged(nameof(CurrentGame));
            }
        }

        public void PlayGame(string gameName)
        {
            StateChange(GameStates.RunGame);
        }

        public VMGame CreateGame(string name)
        {
            Game game = new Game()
            {
                Name = name,
            };
            VMGame vmGame = new VMGame(game, Model);
            games.Add(vmGame);
            Model.Games.Add(game);
            Model.SaveChanges();
            RaisePropertyChanged(nameof(Games));
            RaisePropertyChanged(nameof(ActiveGames));
            
            return vmGame;
        }

        public void SelectGame(VMGame game)
        {
            CurrentGame = game;
            game.LoadPlayerScores();

            RaisePropertyChanged(nameof(CurrentGame));
        }

        public void SelectScore(string path, int[] scores, string columnName)
        {
            string[] splitted = path.Split('.');
            object? source = GetObject(CurrentPlayerScore, splitted);

            // Now we have the object holding the property
            // Now set the property to the new value
            Type? t = source?.GetType();
            PropertyInfo? propInfo = t?.GetProperty(splitted[splitted.Length - 1]);

            if (propInfo == null || propInfo.GetValue(source, null) != null)
            {
                if (propInfo != null)
                {
                    throw new Exception($"Kategorien {columnName} er allerede brugt");
                }
                else
                {
                    throw new Exception("Vælg en kategori");
                }
            }

            int score = CalculateScore(propInfo.Name, scores, columnName);
            if (UIConfirm == null || UIConfirm($"Gem {score} points i {columnName}?"))
            {
                propInfo.SetValue(source, score, null);
                Model.SaveChanges();

                // Notify property change
                (source as VMScoreboard)?.RaisePropertyChanged(propInfo.Name);
                (source as VMScoreboard)?.RaisePropertyChanged(nameof(VMScoreboard.Sum));
                (source as VMScoreboard)?.RaisePropertyChanged(nameof(VMScoreboard.Bonus));
                (source as VMScoreboard)?.RaisePropertyChanged(nameof(VMScoreboard.SumEvalueate));
                (source as VMScoreboard)?.RaisePropertyChanged(nameof(VMScoreboard.Sum2));

                // Now change state
                StateChange(GameStates.NewTurn);
            }
        }


        private int CalculateScore(string property, int[] scores, string columnName)
        {
            if (property == nameof(CurrentPlayerScore.VMScoreboard.Ones))
            {
                return CalculateNumbers(1, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Twos))
            {
                return CalculateNumbers(2, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Threes))
            {
                return CalculateNumbers(3, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Fours))
            {
                return CalculateNumbers(4, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Fives))
            {
                return CalculateNumbers(5, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Sixes))
            {
                return CalculateNumbers(6, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Pair))
            {
                return CalculateSame(2, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.TwoPairs))
            {
                return CalculateTwoPairs(scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.ThreeSame))
            {
                return CalculateSame(3, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.FourSame))
            {
                return CalculateSame(4, scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.LittleStraight))
            {
                return CalculateLittleStraight(scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.GreatStraight))
            {
                return CalculateGreatStraight(scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.House))
            {
                return CalculateHouse(scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Chance))
            {
                return CalculateChance(scores);
            }
            else if (property == nameof(CurrentPlayerScore.VMScoreboard.Yatzy))
            {
                return CalculateYatzy(scores);
            }

            throw new ArgumentException($"Ukendt kolonne '{columnName}'");
        }

        private int[] CountScores(int[] scores)
        {
            int[] candidates = new int[] { 0, 0, 0, 0, 0, 0 };
            foreach (int roll in scores)
            {
                candidates[roll - 1]++;
            }
            return candidates;
        }

        private int CalculateSame(int count, int[] scores)
        {
            int[] candidates = CountScores(scores);
            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                if (candidates[i] >= count)
                {
                    return (i + 1) * count;
                }
            }
            return 0;
        }

        private int CalculateLittleStraight(int[] scores)
        {
            int[] candidates = CountScores(scores);
            for (int i = 0; i < 5; i++)
            {
                if (candidates[i] != 1)
                {
                    return 0;
                }
            }
            return scores.Sum();
        }

        private int CalculateGreatStraight(int[] scores)
        {
            int[] candidates = CountScores(scores);
            for (int i = 1; i < 6; i++)
            {
                if (candidates[i] != 1)
                {
                    return 0;
                }
            }
            return scores.Sum();
        }

        private int CalculateChance(int[] scores)
        {
            int score = 0;
            foreach (int s in scores)
            {
                score += s;
            }
            return score;
        }

        private int CalculateYatzy(int[] scores)
        {
            int candidate = -1;
            foreach (int s in scores)
            {
                if (candidate < 0)
                {
                    candidate = s;
                }
                else if (candidate != s)
                {
                    return 0;
                }
            }
            return 50;
        }

        private int CalculateHouse(int[] scores)
        {
            int[] candidates = CountScores(scores);
            int score = 0;
            bool three = false;
            bool two = false;
            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                if (candidates[i] == 3)
                {
                    score += (i + 1) * 3;
                    three = true;
                }
                else if (candidates[i] == 2)
                {
                    score += (i + 1) * 2;
                    two = true;
                }
            }
            if (two && three)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }

        private int CalculateTwoPairs(int[] scores)
        {
            int[] candidates = CountScores(scores);
            int score = 0;
            int pairCount = 0;
            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                if (candidates[i] >= 2)
                {
                    score += (i + 1) * 2;
                    pairCount++;
                }
                if (pairCount == 2)
                {
                    break;
                }
            }
            if (pairCount == 2)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }

        // Calculate points for 1'es,2'es, ... 6'es
        private int CalculateNumbers(int number, int[] scores)
        {
            int score = 0;
            foreach (int s in scores)
            {
                if (s == number)
                {
                    score += s;
                }
            }
            return score;
        }

        private object? GetObject(object? instance, string[] path)
        {
            if (instance == null)
            {
                return null;
            }

            Type t = instance.GetType();
            for (int i = 0; i < path.Length - 1; i++)
            {
                string prop = path[i];
                PropertyInfo? propInfo = t.GetProperty(prop);
                if (propInfo != null)
                {
                    instance = propInfo.GetValue(instance, null);
                    t = propInfo.PropertyType;
                }
            }
            return instance;
        }

        public void DemoVersion()
        {
            players.Clear();
            VMPlayer player;
            player = CreatePlayer("Peter Plys");
            player = CreatePlayer("Grisling");
            player = CreatePlayer("Ninka");
            Model.SaveChanges();
        }
    }

    public class CellAlreadyUsedException : Exception
    {
        public CellAlreadyUsedException(string message) : base(message) { }
    }
}
