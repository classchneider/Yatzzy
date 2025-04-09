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
using YatzyRepository;
using static ViewModels.VMYatzyGeneral;

namespace Yatzy.UserControls
{
    /// <summary>
    /// Interaction logic for PlayTurn.xaml
    /// </summary>
    public partial class UPlayTurn : UserControl
    {
        Dice Dice1 = new Dice();
        Dice Dice2 = new Dice();
        Dice Dice3 = new Dice();
        Dice Dice4 = new Dice();
        Dice Dice5 = new Dice();

        private Dice[] Dices { get; set; }
        private CurrentPlayer CurrentPlayer { get; set; }
        private UTurnCounter TurnCounter { get; set; }
        public int RollCount { private set; get; }
        bool rolling;

        public int[] Results
        {
            get
            {
                return new int[] { Dice1.DiceValue, Dice2.DiceValue, Dice3.DiceValue, Dice4.DiceValue, Dice5.DiceValue}; 
            }
        }

        public StateChangeMethod StateChange { get; set; }

        public UPlayTurn(StateChangeMethod stateChange)
        {
            InitializeComponent();
            Dices = new Dice[] { Dice1, Dice2, Dice3, Dice4, Dice5 };

            TurnCounter = new UTurnCounter();


            CurrentPlayer = new CurrentPlayer();
            TopStack.Children.Insert(0, CurrentPlayer);

            // Insert TurnCounter at top
            TopStack.Children.Insert(0, TurnCounter);


            foreach (Dice d in Dices)
            {
                d.Width = 80;
                TopPanel.Children.Add(d);
            }

            StateChange = stateChange;
            RollCount = 0;
            rolling = false;
        }

        public void SetHold(HoldInfo[] holds, bool ButtonsAvailable = true)
        {
            if (holds == null)
            {
                return;
            }

            EnableButtons(ButtonsAvailable);

            for (int i=0; i< holds.Length; i++)
            {
                Dices[i].Hold = holds[i].Hold;
                Dices[i].CanHold = holds[i].CanHold;
            }
        }

        public void ResetDices(bool enable = true)
        {
            foreach (Dice d in Dices)
            {
                d.CanHold = false;
                d.Hold = false;
                d.Reset();
            }
            RollCount = 0;
            EnableButtons(enable);
            UpdateRollsText();
        }

        public void SortPlayers()
        {
            TurnCounter.SortPlayers();
        }

        public void EnableButtons(bool enable = true)
        {
            btn_Roll.IsEnabled = RollCount < 3 && enable;
            btn_Select.IsEnabled = RollCount > 0 && enable;
        }

        private void UpdateRollsText()
        {
            tb_RollCount.Text = RollCount.ToString();
        }

        public void Rollback()
        {
            RollCount = 3;
        }

        private async void Roll()
        {
            EnableButtons(false);
            

            // Abort if already rolling or after 3 rolls.
            if (RollCount > 2 || rolling)
            {
                return;
            }
            rolling = true;
            RollCount++;
            Task[] tasks = new Task[Dices.Length];
            for (int i=0; i< Dices.Length; i++)
            {
                tasks[i] = Dices[i].RollDice();
            }
            await Task.WhenAll(tasks);

            foreach (Dice d in Dices)
            {
                d.CanHold = true;
            }
            //EnableButtons();
            rolling = false;
            StateChange(GameStates.AfterRoll);

            UpdateRollsText();
        }

        public async void ActivateRoll()
        {
            Roll();
        }

        private async void btn_Roll_Click(object sender, RoutedEventArgs e)
        {
            Roll();
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            StateChange(GameStates.SelectScore);
        }

        private void ShowScorePossibilities()
        {

        }
    }
}
