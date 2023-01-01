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
            //DataContext = this;
            scoreboard = new UCScoreBoard(ViewModel);
            setupGame = new USetupGame(ViewModel, g_Left);
            turnPlay = new UPlayTurn(ViewChangeState);
            g_Right.Children.Add(scoreboard);
            g_Left.Children.Add(setupGame);
            ViewModel.UIStateChange = ViewChangeState;
            ViewModel.UIConfirm = ConfirmAction;
        }

        public void NewGame()
        {
            g_Left.Children.Clear();
            g_Left.Children.Add(turnPlay);
            Reset();
        }

        private void Reset()
        {
            turnPlay.ResetDices();
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
                    case GameStates.NewGame:
                        NewGame();
                        break;
                    case GameStates.SelectScore:
                        SelectScore(scoreboard.SelectedCellName);
                        break;
                    case GameStates.NewTurn:
                        Reset();
                        break;
                }
            }
            catch (CellAlreadyUsedException ex)
            {
                string column = scoreboard.SelectedCellName;
                MessageBox.Show($"{column} already used");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot Register score");
            }
        }
    }
}
