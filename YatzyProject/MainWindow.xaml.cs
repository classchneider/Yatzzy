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
using YatzyRepository;

namespace Yatzzy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model Model { get; set; } = new Model();
        public MainWindow()
        {
            InitializeComponent();
            DemoVersion();
            DataContext = this;
        }

        public void DemoVersion()
        {

            Scoreboard result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 15,
                Fours = 12,
                Threes = 9,
                Twos = 6,
                Ones = 3,
            };
            Model.Scoreboards.Add(result);
            Player player = new Player()
            {
                Result = result,
                Name = "Claus",
            };
            Model.Players.Add(player);
            result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 20,
                Fours = 8,
                Threes = 6,
                Twos = 6,
                Ones = 3,
            };
            Model.Scoreboards.Add(result);
            player = new Player()
            {
                Result = result,
                Name = "Grisling",
            };
            Model.Players.Add(player);
            result = new Scoreboard()
            {
                Sixes = 18,
                Fives = 20,
                Fours = 12,
                Threes = 12,
                Twos = 6,
                Ones = 3,
            };
            Model.Scoreboards.Add(result);
            player = new Player()
            {
                Result = result,
                Name = "Ninka",
            };
            Model.Players.Add(player);
        }
    }
}
