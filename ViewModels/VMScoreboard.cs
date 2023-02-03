using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMScoreboard : INotifyPropertyChanged
    {
        public VMScoreboard(Scoreboard scoreboard)
        {
            Scoreboard = scoreboard;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private Scoreboard Scoreboard { get; set; }

        public int? Ones
        {
            get => Scoreboard.Ones;
            set => Scoreboard.Ones = value;
        }
        public int? Twos
        {
            get => Scoreboard.Twos;
            set => Scoreboard.Twos = value;
        }
        public int? Threes
        {
            get => Scoreboard.Threes;
            set => Scoreboard.Threes = value;
        }
        public int? Fours
        {
            get => Scoreboard.Fours;
            set => Scoreboard.Fours = value;
        }
        public int? Fives
        {
            get => Scoreboard.Fives;
            set => Scoreboard.Fives = value;
        }
        public int? Sixes
        {
            get => Scoreboard.Sixes;
            set => Scoreboard.Sixes = value;
        }
        public int BonusStatus
        {
            get
            {
                int par = 0;
                int score = 0;
                if (Ones != null)
                {
                    par += 3 * 1;
                    score += Ones.Value;
                }
                if (Twos != null)
                {
                    par += 3 * 2;
                    score += Twos.Value;
                }
                if (Threes != null)
                {
                    par += 3 * 3;
                    score += Threes.Value;
                }
                if (Fours != null)
                {
                    par += 3 * 4;
                    score += Fours.Value;
                }
                if (Fives != null)
                {
                    par += 3 * 5;
                    score += Fives.Value;
                }
                if (Sixes != null)
                {
                    par += 3 * 6;
                    score += Sixes.Value;
                }
                return score - par;
            }
        }

        static string FormatFinancialNumber(int number)
        {
            string sign = number < 0 ? "-" : "+"; // decide the sign
            return sign + Math.Abs(number).ToString().PadLeft(7); // Make the number part 7 characters long
        }

        public string SumEvalueate
        {
            get
            {
                return $"({string.Format("{0,1:#;#;0}{0:+;-;+}", BonusStatus)}) {Sum}";
            }
        }

        public int Sum
        {
            get
            {
                return (Sixes != null ? Sixes.Value : 0) +
                    (Fives != null ? Fives.Value : 0) +
                    (Fours != null ? Fours.Value : 0) +
                    (Threes != null ? Threes.Value : 0) +
                    (Twos != null ? Twos.Value : 0) +
                    (Ones != null ? Ones.Value : 0);
            }
        }
        public int Bonus
        {
            get
            {
                return (Sum >= 3 * (1 + 2 + 3 + 4 + 5 + 6)) ? 50 : 0;
            }
        }

        public int? Pair
        {
            get => Scoreboard.Pair;
            set => Scoreboard.Pair = value;
        }
        public int? TwoPairs
        {
            get => Scoreboard.TwoPairs;
            set => Scoreboard.TwoPairs = value;
        }

        public int? ThreeSame
        {
            get => Scoreboard.ThreeSame;
            set => Scoreboard.ThreeSame = value;
        }
        public int? FourSame
        {
            get => Scoreboard.FourSame;
            set => Scoreboard.FourSame = value;
        }
        public int? LittleStraight
        {
            get => Scoreboard.LittleStraight;
            set => Scoreboard.LittleStraight = value;
        }
        public int? GreatStraight
        {
            get => Scoreboard.GreatStraight;
            set => Scoreboard.GreatStraight = value;
        }
        public int? House
        {
            get => Scoreboard.House;
            set => Scoreboard.House = value;
        }
        public int? Chance
        {
            get => Scoreboard.Chance;
            set => Scoreboard.Chance = value;
        }
        public int? Yatzy
        {
            get => Scoreboard.Yatzy;
            set => Scoreboard.Yatzy = value;
        }

        public int Sum2
        {
            get
            {
                return Sum +
                    Bonus +
                    (Pair != null ? Pair.Value : 0) +
                    (TwoPairs != null ? TwoPairs.Value : 0) +
                    (ThreeSame != null ? ThreeSame.Value : 0) +
                    (FourSame != null ? FourSame.Value : 0) +
                    (LittleStraight != null ? LittleStraight.Value : 0) +
                    (GreatStraight != null ? GreatStraight.Value : 0) +
                    (House != null ? House.Value : 0) +
                    (Chance != null ? Chance.Value : 0) +
                    (Yatzy != null ? Yatzy.Value : 0);
            }
        }

        public bool IsFull
        {
            get
            {
                return
                    Ones.HasValue &&
                    Twos.HasValue &&
                    Threes.HasValue &&
                    Fours.HasValue &&
                    Fives.HasValue &&
                    Sixes.HasValue &&
                    Pair.HasValue &&
                    TwoPairs.HasValue &&
                    ThreeSame.HasValue &&
                    FourSame.HasValue &&
                    LittleStraight.HasValue &&
                    GreatStraight.HasValue &&
                    House.HasValue &&
                    Chance.HasValue &&
                    Yatzy.HasValue;
            }
        }
    }
}
