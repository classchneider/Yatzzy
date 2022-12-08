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

namespace Yatzy
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UCScoreBoard scoreboard;
        USetupGame turnPlay;

        public VMYatzyGeneral ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = DataContext as VMYatzyGeneral;
            //DataContext = this;
            scoreboard = new UCScoreBoard(g_Right);
            turnPlay = new USetupGame(ViewModel, g_Left);
            g_Right.Children.Add(scoreboard);
            g_Left.Children.Add(turnPlay);
        }

    }
}
