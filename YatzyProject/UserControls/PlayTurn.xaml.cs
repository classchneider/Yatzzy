﻿using System;
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
        int RollCount;
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
        }

        public void SortPlayers()
        {
            TurnCounter.SortPlayers();
        }

        private void EnableButtons(bool enable = true)
        {
            btn_Roll.IsEnabled = RollCount < 3 && enable;
            btn_Select.IsEnabled = RollCount > 0 && enable;
        }

        private async void Roll()
        {
            // Abort if already rolling or after 3 rolls.
            if (RollCount > 2 || rolling)
            {
                return;
            }
            rolling = true;
            RollCount++;
            Task d1 = Dice1.RollDice();
            Task d2 = Dice2.RollDice();
            Task d3 = Dice3.RollDice();
            Task d4 = Dice4.RollDice();
            Task d5 = Dice5.RollDice();
            await Task.WhenAll(new Task[] { d1, d2, d3, d4, d5 });
            foreach (Dice d in Dices)
            {
                d.CanHold = true;
            }
            EnableButtons();
            rolling = false;
        }

        private async void btn_Roll_Click(object sender, RoutedEventArgs e)
        {
            Roll();
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            StateChange(GameStates.SelectScore);
        }
    }
}
