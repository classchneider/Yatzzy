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

namespace Yatzy.UserControls
{
    /// <summary>
    /// Interaction logic for Dice.xaml
    /// </summary>
    public partial class Dice : UserControl
    {
        Random random = new Random();
        bool hold = false;
        public bool Hold
        {
            get { return hold; }
            set
            {
                if (value == false)
                {
                    hold = value;
                }
            }
        }
        bool rolling = false;
        public int DiceValue { get; private set; }

        public bool CanHold { get; set; }

        Image[] Dices;

        public Dice()
        {
            InitializeComponent();
            Dices = new Image[] { img_Dice1, img_Dice2, img_Dice3, img_Dice4, img_Dice5, img_Dice6 };
            DiceValue = random.Next(1, 7);
        }

        public async Task<int> RollDice()
        {
            if (hold)
            {
                return DiceValue;
            }
            rolling = true;
            int count = 20;
            int tmpDice = -1;
            for (int i = 0; i < count; i++)
            {
                tmpDice = random.Next(1, 7);
                ShowDice(tmpDice);
                await Task.Delay(150);
            }
            DiceValue = tmpDice;
            rolling = false;
            return DiceValue;
        }

        public void Reset()
        {
            ShowDice(DiceValue);
            Reframe();
        }

        private void ShowDice(int dice)
        {
            // Hide all dices
            foreach (Image i in Dices)
            {
                i.Visibility = Visibility.Hidden;
            }

            // Show the correct dice
            Dices[dice - 1].Visibility = Visibility.Visible;
        }

        private void Reframe()
        {
            if (hold)
            {
                grid_Dice.Background = Brushes.Blue;
            }
            else
            {
                grid_Dice.Background = Brushes.White;
            }
        }

        private void grid_Dice_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (rolling || !CanHold)
            {
                return;
            }
            hold = !hold;
            Reframe();
        }
    }
}
