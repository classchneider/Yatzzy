using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace YatzyRepository
{
    public struct HoldInfo
    {
        public bool Hold;
        public bool CanHold;
    }

    public class DiceCandidate : IComparable
    {
        public string Property { get; set; }
        public int Missing { get; set; }
        public int Needed { get; set; }
        public int Score { get; set; }
        public double CombinedScore { get; set; }
        public int HoldDice { get; set; }

        public int CompareTo(object? obj)
        {
            int combinedCmp = CombinedScore.CompareTo(((DiceCandidate)obj).CombinedScore);
            if (combinedCmp == 0)
            {
                return -Missing.CompareTo(((DiceCandidate)obj).Missing);
            }
            else
            {
                return combinedCmp;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Property)}: {Property} | {nameof(Missing)}: {Missing} | {nameof(HoldDice)}: {HoldDice} | {nameof(Score)}: {Score} | {nameof(CombinedScore)}: {CombinedScore}";
        }
    }

    public abstract class Player 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HumanPlayer : Player
    {
    }

    public class AIRollPlayer : Player
    {

    }

}
