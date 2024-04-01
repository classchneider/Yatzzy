using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzyRepository
{
    public class Scoreboard
    {
        public int Id { get; set; }
        public int? Ones { get; set; }
        public int? Twos { get; set; }
        public int? Threes { get; set; }
        public int? Fours { get; set; }
        public int? Fives { get; set; }
        public int? Sixes { get; set; }

        public int? Pair { get; set; }
        public int? TwoPairs { get; set; }
        public int? ThreeSame { get; set; }
        public int? FourSame { get; set; }
        public int? LittleStraight { get; set; }
        public int? GreatStraight { get; set; }
        public int? House { get; set; }
        public int? Chance { get; set; }
        public int? Yatzy { get; set; }

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

    }

}
