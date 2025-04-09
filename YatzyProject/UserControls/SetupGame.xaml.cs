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

namespace Yatzy.UserControls
{
    /// <summary>
    /// Interaction logic for TurnPlay.xaml
    /// </summary>
    public partial class USetupGame : UserControl
    {
        VMYatzyGeneral viewModel;

        public USetupGame(VMYatzyGeneral vMYatzy, Grid grid)
        {
            InitializeComponent();
            viewModel = vMYatzy;
        }       

        private void btn_AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreatePlayer(tb_Name.Text);
            tb_Name.Text = "";
        }

        private void btn_AddAIPlayer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreateAIRollPlayer(tb_Name.Text);
            tb_Name.Text = "";
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            AutomatedSelectionUpdate ++;
            viewModel.PlayGame(tb_Game.Text);
            AutomatedSelectionUpdate--;
        }

        private void btn_CreateGame_Click(object sender, RoutedEventArgs e)
        {
            VMGame game = viewModel.CreateGame(tb_Game.Text);
            lb_Games.SelectedItem = game;
        }

        private void btn_NewGame_Click(object sender, RoutedEventArgs e)
        {
            VMGame? game = viewModel.NewGame();
            lb_Games.SelectedItem = game;
            lb_FinishedGames.SelectedItem = null;
        }

        private void lb_Games_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                viewModel.SelectGame(e.AddedItems[0] as VMGame);
            }
            UpdateAllPlayersSelection();
        }

        int AutomatedSelectionUpdate { get; set; } = 0;

        private void UpdateAllPlayersSelection()
        {
            AutomatedSelectionUpdate++;
            lb_AllPlayers.UnselectAll();
            foreach (VMPlayerScore playerScore in viewModel.CurrentGame.PlayerScores)
            {
                lb_AllPlayers.SelectedItems.Add(playerScore.VMPlayer);
            }
            AutomatedSelectionUpdate--;
        }

        private void lb_AllPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AutomatedSelectionUpdate > 0)
            {
                return;
            }

            if (e.AddedItems?.Count > 0)
            {
                foreach (VMPlayer player in e.AddedItems)
                {
                    if (!viewModel.AddPlayer(player))
                    {
                        lb_AllPlayers.SelectedItems.Remove(player);
                    }
                }
            }
            if (e.RemovedItems?.Count > 0)
            {
                foreach (VMPlayer player in e.RemovedItems)
                {
                    viewModel.RemovePlayer(player);
                }
            }
        }

        private async void tb_Game_GotFocus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(10);
            tb_Game.SelectAll();
        }

        private void tb_Game_GotFocus(object sender, MouseButtonEventArgs e)
        {
            tb_Game.SelectAll();
        }

        private void tb_Game_GotFocus(object sender, ManipulationStartingEventArgs e)
        {
            tb_Game.SelectAll();
        }

    }
}
