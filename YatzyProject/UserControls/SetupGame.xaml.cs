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

        private void DemoButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DemoVersion();
        }

        private void btn_AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPlayer(tb_Name.Text);
        }
    }
}
