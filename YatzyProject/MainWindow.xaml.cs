using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels;
using Yatzy.UserControls;
using YatzyRepository;
using static System.Formats.Asn1.AsnWriter;
using static ViewModels.VMYatzyGeneral;

namespace Yatzy
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UCScoreBoard scoreboard;
        UPlayTurn turnPlay;
        USetupGame setupGame;

        public VMYatzyGeneral ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = DataContext as VMYatzyGeneral;
            ViewModel.UIStateChange = ViewChangeState;
            ViewModel.UIConfirm = ConfirmAction;
            ViewModel.ValueChangedHandler += ValueSet;

            scoreboard = new UCScoreBoard(ViewModel);
            setupGame = new USetupGame(ViewModel, g_Left);
            turnPlay = new UPlayTurn(ViewChangeState);

            g_Right.Children.Add(scoreboard);
            GotoSetup();
        }

        private void ValueSet(object? sender, PropertyChangedEventArgs e)
        {
            int[] sortedScores = PlayerScore.CountScores(turnPlay.Results);
            scoreboard.MarkScoreChoice(ViewModel.GenerateSuggestion(sortedScores, e.PropertyName), sender as VMPlayer);
        }

        public void GotoSetup()
        {
            g_Left.Children.Clear();
            g_Left.Children.Add(setupGame);
        }

        public void NewGame()
        {
            g_Left.Children.Clear();
            g_Left.Children.Add(turnPlay);
            Reset();
        }

        private void Reset()
        {
            turnPlay.SortPlayers();
            turnPlay.ResetDices(!ViewModel.IsGameOver);
            scoreboard.ResetScoreboard();
        }

        public void SelectScore(string ColumnName)
        {
            ViewModel.SelectScore(scoreboard.SelectedHeaderPath, turnPlay.Results, ColumnName);
        }

        public void MakeSuggestions()
        {
            scoreboard.MarkScoreSuggestions(ViewModel.GenerateSuggestions(turnPlay.Results));
        }

        public bool ConfirmAction(string message, string title)
        {
            MessageBoxResult res = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            return (res == MessageBoxResult.OK);
        }

        public async void AskPlayerAction()
        {
            HoldInfo[]? hold = ViewModel.Holds(turnPlay.Results, turnPlay.RollCount);

            if (hold != null)
            {
                await Task.Delay(1000);
                turnPlay.SetHold(hold, false);

                await Task.Delay(2000);
                if (turnPlay.RollCount == 3)
                {
                    ViewModel.SelectScore(turnPlay.Results, turnPlay.RollCount);
                }
                else
                {
                    turnPlay.ActivateRoll();
                    turnPlay.EnableButtons(false);
                }
            }
            else
            {
                turnPlay.EnableButtons();
            }
        }

        public void ViewChangeState(GameStates state)
        {
            try
            {
                switch (state)
                {
                    case GameStates.RunGame:
                        NewGame();
                        break;
                    case GameStates.SelectScore:
                        SelectScore(scoreboard.SelectedCellName);
                        break;
                    case GameStates.Rolling:
                        scoreboard.ResetSuggestions();
                        break;
                    case GameStates.AfterRoll:
                        MakeSuggestions();
                        AskPlayerAction();
                        break;
                    case GameStates.NewTurn:
                        Reset();
                        break;
                    case GameStates.EndGame:
                        Reset();
                        scoreboard.SelectPlayer(ViewModel.LeadingPlayer);
                        break;
                    case GameStates.SetupGame:
                        GotoSetup();
                        break;
                }
            }
            catch (CellAlreadyUsedException ex)
            {
                MessageBox.Show($"{scoreboard.SelectedCellName} er allerede brugt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Score kan ikke registreres i {scoreboard.SelectedCellName}");
            }
        }

        private void Menu_New_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GameSetup();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_Suggestions_Click(object sender, RoutedEventArgs e)
        {
            MakeSuggestions();
        }

        private void Menu_Holds_Click(object sender, RoutedEventArgs e)
        {
            AskPlayerAction();
        }

        private void Menu_Select_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentPlayerScore.SelectScore(ViewModel.GenerateSuggestions(turnPlay.Results), turnPlay.Results, turnPlay.RollCount);
        }
    }
}
