using System;
using System.Collections.Generic;
using System.Linq;
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

            scoreboard = new UCScoreBoard(ViewModel);
            setupGame = new USetupGame(ViewModel, g_Left);
            turnPlay = new UPlayTurn(ViewChangeState);

            g_Right.Children.Add(scoreboard);
            GotoSetup();
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
            turnPlay.ResetDices(!ViewModel.IsGameOver);
            turnPlay.SortPlayers();
            scoreboard.ResetScoreboard();
        }

        public void SelectScore(string ColumnName)
        {
            ViewModel.SelectScore(scoreboard.SelectedHeaderPath, turnPlay.Results, ColumnName);
        }

        public bool ConfirmAction(string message, string title)
        {
            MessageBoxResult res = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            return (res == MessageBoxResult.OK);
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
    }
}
