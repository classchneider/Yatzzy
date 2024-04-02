using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatzyRepository;

namespace ViewModels
{
    public class VMPlayerComputer : VMPlayer
    {
        public VMPlayerComputer(Player player) : base(player)
        {
        }
        public override string Name
        {
            get
            {
                return $"{base.Name} - bot";
            }
        }

        public override RollActions Action
        {
            get
            {
                return RollActions.Roll;
            }
        }


        ///////////////
        /// 
        /// AI Code
        /// 
        ///////////////
        HoldInfo[] HoldAll()
        {
            return new HoldInfo[]
            {
                    new HoldInfo { Hold = true, CanHold = false },
                    new HoldInfo { Hold = true, CanHold = false },
                    new HoldInfo { Hold = true, CanHold = false },
                    new HoldInfo { Hold = true, CanHold = false },
                    new HoldInfo { Hold = true, CanHold = false }
            };
        }

        HoldInfo[] HoldNothing()
        {
            return new HoldInfo[]
            {
                    new HoldInfo { Hold = false, CanHold = false },
                    new HoldInfo { Hold = false, CanHold = false },
                    new HoldInfo { Hold = false, CanHold = false },
                    new HoldInfo { Hold = false, CanHold = false },
                    new HoldInfo { Hold = false, CanHold = false }
            };
        }

        HoldInfo[] HoldGreaterOrEqual(int[] Results, int value)
        {
            HoldInfo[] holdInfos = new HoldInfo[5];
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] >= value)
                {
                    holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                }
            }

            return holdInfos;
        }

        HoldInfo[] HoldTwoPairs(int[] Results)
        {
            HoldInfo[] holdInfos = new HoldInfo[5];
            for (int i = 0; i < Results.Length; i++)
            {
                for (int j = i + 1; j < Results.Length; j++)
                {
                    if (Results[i] == Results[j] && !holdInfos[i].Hold)
                    {
                        // Hold pair
                        holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                        holdInfos[j] = new HoldInfo() { CanHold = false, Hold = true };

                    }
                }
            }
            return holdInfos;
        }

        HoldInfo[] HoldHouse(int[] Results, int UpToThree, int UpToTwo)
        {
            HoldInfo[] holdInfos = new HoldInfo[5];
            int UpToThreeCount = 0;
            int UpToTwoCount = 0;

            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] == UpToThree && UpToThreeCount < 3)
                {
                    holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                    UpToThreeCount++;
                }
                if (Results[i] == UpToTwo && UpToTwoCount < 2)
                {
                    holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                    UpToTwoCount++;
                }
            }

            return holdInfos;
        }

        HoldInfo[] HoldNumbers(int[] Results, int number)
        {
            HoldInfo[] holdInfos = new HoldInfo[5];

            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] == number)
                {
                    holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                }
                else
                {
                    holdInfos[i] = new HoldInfo() { CanHold = false, Hold = false };
                }
            }

            return holdInfos;
        }

        public int StraightCount(int[] sortedScores, int startValue)
        {
            int count = 0;
            for (int i = startValue; i < startValue + 4; i++)
            {
                if (sortedScores[i] > 0)
                {
                    count++;
                }
            }
            return count;
        }

        // Points for the score Good score
        static int AIScorePoints((string property, int value) suggestion, int BonusStatus, int diceCount)
        {
            int ScoreDiff;
            string propName = suggestion.property;
            int score = suggestion.value;

            ScoreDiff = score - Rules.GoodScore(propName);
            int ThrowsLeft = 3 - diceCount;
            int AddBonusStatus = Math.Min(BonusStatus, 0);

            if (propName == nameof(Scoreboard.Ones))
            {
                // Possible but not very good
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft + score - AddBonusStatus + 1;
            }
            if (propName == nameof(Scoreboard.Twos))
            {
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft * 2 + score - AddBonusStatus + 2;
            }
            if (propName == nameof(Scoreboard.Threes))
            {
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft * 3 + score - AddBonusStatus + 3;
            }
            if (propName == nameof(Scoreboard.Fours))
            {
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft * 4 + score - AddBonusStatus + 4;
            }
            if (propName == nameof(Scoreboard.Fives))
            {
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft * 5 + score - AddBonusStatus + 5;
            }
            if (propName == nameof(Scoreboard.Sixes))
            {
                // Negative bonus status makes it more important to try
                return ScoreDiff + ThrowsLeft * 6 + score - AddBonusStatus + 6;
            }

            // Diff is negative- add some positivity
            if (propName == nameof(Scoreboard.Pair))
            {
                // Use pair as last resort
                return ScoreDiff;
            }
            if (propName == nameof(Scoreboard.TwoPairs))
            {
                return ScoreDiff + score;
            }
            if (propName == nameof(Scoreboard.ThreeSame))
            {
                return ScoreDiff + score;
            }
            if (propName == nameof(Scoreboard.FourSame))
            {
                return ScoreDiff + score;
            }
            if (propName == nameof(Scoreboard.LittleStraight))
            {
                // Straights have the problem that it is risky to go for
                return ScoreDiff + score * 2 / 3;
            }
            if (propName == nameof(Scoreboard.GreatStraight))
            {
                // Straights have the problem that it is risky to go for
                return ScoreDiff + score * 2 / 3;
            }
            if (propName == nameof(Scoreboard.House))
            {
                return ScoreDiff + score;
            }
            if (propName == nameof(Scoreboard.Chance))
            {
                // Very last option
                return ScoreDiff;
            }
            if (propName == nameof(Scoreboard.Yatzy))
            {
                return ScoreDiff + score;
            }

            return ScoreDiff;
        }

        // If we find out how many dice results are needed for different goals
        // we can decide which to go for.
        //List<(string property, int missing, int Points)> missingDices = new List<(string, int, int)>();
        List<DiceCandidate> CalculateMissingDices(VMScoreboard Scoreboard, int[] Results, int[] sortedScores, int diceCount, int MostOccurencesIndex)
        {
            int AIPointsDirectLocal((string property, int score) suggestion)
            {
                return AIScorePoints(suggestion, Scoreboard.BonusStatus, diceCount);
            }
            List<DiceCandidate> missingDices = new();

            if (!Scoreboard.Sixes.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Sixes), 3 * 6));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Sixes),
                        Missing = 3 - sortedScores[5],
                        Needed = 3,
                        Score = points,
                        HoldDice = 6,
                    }
                );
            }
            if (!Scoreboard.Fives.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Fives), 3 * 5));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Fives),
                        Missing = 3 - sortedScores[4],
                        Needed = 3,
                        Score = points,
                        HoldDice = 5,
                    }
                );
            }
            if (!Scoreboard.Fours.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Fours), 3 * 4));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Fours),
                        Missing = 3 - sortedScores[3],
                        Needed = 3,
                        Score = points,
                        HoldDice = 4,
                    }
                );
            }
            if (!Scoreboard.FourSame.HasValue)
            {
                for (int i = 0; i < sortedScores.Length; i++)
                {
                    int points = AIPointsDirectLocal((nameof(Scoreboard.FourSame), (i + 1) * 4));

                    missingDices.Add(
                        new DiceCandidate
                        {
                            Property = nameof(Scoreboard.FourSame),
                            Missing = 4 - sortedScores[i],
                            Needed = 4,
                            Score = points,
                            HoldDice = i+1,
                        }
                    );
                }
            }
            if (!Scoreboard.ThreeSame.HasValue)
            {
                for (int i = 0; i < sortedScores.Length; i++)
                {
                    int points = AIPointsDirectLocal((nameof(Scoreboard.ThreeSame), (i + 1) * 3));

                    missingDices.Add(
                        new DiceCandidate
                        {
                            Property = nameof(Scoreboard.ThreeSame),
                            Missing = 3 - sortedScores[i],
                            Needed = 3,
                            Score = points,
                            HoldDice = i+1,
                        }
                    );
                }
            }
            if (!Scoreboard.House.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.House), (MostOccurencesIndex + 1) * 3 + 2 * 3));

                int ThreeOfAKind = 0;
                int TwoOfAKind = 0;
                for (int i = 0; i < sortedScores.Length; i++)
                {
                    if (sortedScores[i] >= 3)
                    {
                        ThreeOfAKind = 3;
                    }
                    else if (sortedScores[i] >= 2)
                    {
                        if (TwoOfAKind == 2)
                        {
                            ThreeOfAKind = 2;
                        }
                        else
                        {
                            TwoOfAKind = 2;
                        }
                    }
                    else if (sortedScores[i] > 0)
                    {
                        if (ThreeOfAKind > 0)
                        {
                            TwoOfAKind = 1;
                        }
                        else
                        {
                            ThreeOfAKind = 1;
                        }
                    }
                }

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.House),
                        Missing = 5 - ThreeOfAKind - TwoOfAKind,
                        Needed = 5,
                        Score = points,
                        HoldDice = -1,
                    }
                );
            }
            if (!Scoreboard.Yatzy.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Yatzy), 50));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Yatzy),
                        Missing = 5 - sortedScores[MostOccurencesIndex],
                        Needed = 5,
                        Score = points,
                        HoldDice = MostOccurencesIndex+1,
                    }
                );
            }
            if (!Scoreboard.Threes.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Threes), 3 * 3));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Threes),
                        Missing = 3 - sortedScores[2],
                        Needed = 3,
                        Score = points,
                        HoldDice = 3,
                    }
                );
            }
            if (!Scoreboard.TwoPairs.HasValue)
            {
                int missing = 2;
                int score = 0;
                int largestSingle = -1;
                for (int i = 0; i < sortedScores.Length; i++)
                {
                    if (sortedScores[i] >= 2)
                    {
                        missing--;
                        score += 2 * (i + 1);
                    }
                    else if (sortedScores[i] == 1)
                    {
                        largestSingle = i;
                    }
                }
                // for every missing partner, add some possible score
                // (Simplified as the largest single dice and one that is one smaller)
                for (int i = 0; i < missing; i++)
                {
                    score += (largestSingle - i) * 2;
                }

                int points = AIPointsDirectLocal((nameof(Scoreboard.TwoPairs), score));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.TwoPairs),
                        Missing = missing,
                        Needed = 4,
                        Score = points,
                        HoldDice = -1,
                    }
                );
            }
            if (!Scoreboard.Twos.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Twos), 3 * 2));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Twos),
                        Missing = 3 - sortedScores[1],
                        Needed = 3,
                        Score = points,
                        HoldDice = 2,
                    }
                );
            }
            if (!Scoreboard.Ones.HasValue)
            {
                int points = AIPointsDirectLocal((nameof(Scoreboard.Ones), 3 * 1));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Ones),
                        Missing = 3 - sortedScores[0],
                        Needed = 3,
                        Score = points,
                        HoldDice = 1,
                    }
                );
            }
            if (!Scoreboard.GreatStraight.HasValue)
            {
                int missing = 5;
                for (int i = 1; i < sortedScores.Length; i++)
                {
                    if (sortedScores[i] > 0)
                    {
                        missing--;
                    }
                }
                int points = AIPointsDirectLocal((nameof(Scoreboard.GreatStraight), 20));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.GreatStraight),
                        Missing = missing,
                        Needed = 5,
                        Score = points,
                        HoldDice = -1,
                    }
                );
            }
            if (!Scoreboard.LittleStraight.HasValue)
            {
                int missing = 5;
                for (int i = 0; i < sortedScores.Length - 1; i++)
                {
                    if (sortedScores[i] > 0)
                    {
                        missing--;
                    }
                }

                int points = AIPointsDirectLocal((nameof(Scoreboard.LittleStraight), 15));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.LittleStraight),
                        Missing = missing,
                        Needed = 5,
                        Score = points,
                        HoldDice = -1,
                    }
                );
            }
            if (!Scoreboard.Pair.HasValue)
            {
                for (int i = 0; i < sortedScores.Length; i++)
                {
                    int points = AIPointsDirectLocal((nameof(Scoreboard.Pair), (i + 1) * 3));

                    missingDices.Add(
                        new DiceCandidate
                        {
                            Property = nameof(Scoreboard.Pair),
                            Missing = 2 - sortedScores[i],
                            Needed = 2,
                            Score = points,
                            HoldDice = i + 1,
                        }
                    );
                }
            }
            if (!Scoreboard.Chance.HasValue)
            {
                int score = Results.Sum();
                int points = AIPointsDirectLocal((nameof(Scoreboard.Chance), score));

                missingDices.Add(
                    new DiceCandidate
                    {
                        Property = nameof(Scoreboard.Chance),
                        Missing = (30 - score) / 4,
                        Needed = 5,
                        Score = points,
                        HoldDice = -1,
                    }
                );
            }

            return missingDices;
        }

        HoldInfo[]? HoldAimProperty(DiceCandidate AimCandidate, int[] Results, int[] sortedScores, int MostOccurencesIndex)
        {
            int DefaultHoldDice = AimCandidate.HoldDice > 0 ? AimCandidate.HoldDice : MostOccurencesIndex + 1;
            if (AimCandidate.Property == nameof(Scoreboard.Sixes))
            {
                return HoldNumbers(Results, 6);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Fives))
            {
                return HoldNumbers(Results, 5);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Fours))
            {
                return HoldNumbers(Results, 4);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Threes))
            {
                return HoldNumbers(Results, 3);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Twos))
            {
                return HoldNumbers(Results, 2);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Ones))
            {
                return HoldNumbers(Results, 1);
            }
            if (AimCandidate.Property == nameof(Scoreboard.TwoPairs))
            {
                return HoldTwoPairs(Results);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Pair))
            {
                return HoldNumbers(Results, DefaultHoldDice);
            }
            if (AimCandidate.Property == nameof(Scoreboard.ThreeSame))
            {
                return HoldNumbers(Results, DefaultHoldDice);
            }
            if (AimCandidate.Property == nameof(Scoreboard.FourSame))
            {
                return HoldNumbers(Results, DefaultHoldDice);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Yatzy))
            {
                return HoldNumbers(Results, DefaultHoldDice);
            }
            if (AimCandidate.Property == nameof(Scoreboard.LittleStraight))
            {
                HoldInfo[] holdInfos = new HoldInfo[5];
                // Save one of each 1-5
                int[] holdCount = new int[5];
                for (int i = 0; i < Results.Length; i++)
                {
                    if (Results[i] < 6 && holdCount[Results[i] - 1] == 0)
                    {
                        holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                        // now we have held one of these
                        holdCount[Results[i] - 1]++;
                    }
                }
                return holdInfos;
            }
            if (AimCandidate.Property == nameof(Scoreboard.GreatStraight))
            {
                HoldInfo[] holdInfos = new HoldInfo[5];
                // Save one of each 1-5
                int[] holdCount = new int[5];
                for (int i = 0; i < Results.Length; i++)
                {
                    if (Results[i] > 1 && holdCount[Results[i] - 2] == 0)
                    {
                        holdInfos[i] = new HoldInfo() { CanHold = false, Hold = true };
                        // now we have held one of these
                        holdCount[Results[i] - 2]++;
                    }
                }
                return holdInfos;
            }
            if (AimCandidate.Property == nameof(Scoreboard.House))
            {
                int MostOccuring = MostOccurencesIndex + 1;
                int SecondCandidate = -1;
                for (int i = sortedScores.Length - 1; i >= 0; i--)
                {
                    // Skip the most occuring
                    if (i == MostOccurencesIndex)
                    {
                        continue;
                    }

                    if (sortedScores[i] > 0 && SecondCandidate == -1)
                    {
                        SecondCandidate = i + 1;
                    }
                    if (sortedScores[i] > 1)
                    {
                        SecondCandidate = i + 1;
                    }
                }
                return HoldHouse(Results, MostOccuring, SecondCandidate);
            }
            if (AimCandidate.Property == nameof(Scoreboard.Chance))
            {
                return HoldGreaterOrEqual(Results, 4);
            }

            return null;
        }

        HoldInfo[]? HoldAimIfGreater(DiceCandidate? AimCandidate, int[] Results, int[] sortedScores, int MostOccurencesIndex, int AcceptValue)
        {
            HoldInfo[]? holdInfo = null;
            if (AimCandidate != null && AimCandidate.CombinedScore >= AcceptValue)
            {
                holdInfo = HoldAimProperty(AimCandidate, Results, sortedScores, MostOccurencesIndex);
            }

            return holdInfo;
        }

        static int BonusDifference((string property, int value) suggestion, VMScoreboard Scoreboard)
        {
            if (suggestion.property == nameof(Scoreboard.Ones))
            {
                return suggestion.value - 3;
            }
            if (suggestion.property == nameof(Scoreboard.Twos))
            {
                return suggestion.value - 3 * 2;
            }
            if (suggestion.property == nameof(Scoreboard.Threes))
            {
                return suggestion.value - 3 * 3;
            }
            if (suggestion.property == nameof(Scoreboard.Fours))
            {
                return suggestion.value - 3 * 4;
            }
            if (suggestion.property == nameof(Scoreboard.Fives))
            {
                return suggestion.value - 3 * 5;
            }
            if (suggestion.property == nameof(Scoreboard.Sixes))
            {
                return suggestion.value - 3 * 6;
            }

            return 0;
        }


        // Returns the name of the property to which the score should be registered
        public override PropertyInfo SelectScore(List<(string property, int value)> suggestions, int[] Results, VMScoreboard Scoreboard, int diceCount)
        {
            PropertyInfo PropertyInfo(string property)
            {
                return Scoreboard.GetType().GetProperty(property);
            }

            int AIPoints(int i)
            {
                return AIScorePoints(suggestions[i], Scoreboard.BonusStatus, diceCount);
            }

            int BonusDiff(int i)
            {
                return BonusDifference(suggestions[i], Scoreboard);
            }

            bool Keeps(int i)
            {
                return Scoreboard.BonusStatus + BonusDiff(i) >= 0;
            }

            // Find the best score that does not jepodize the bonus
            int SomeScoreCandidate = -1;
            for (int i = 0; i < suggestions.Count; i++)
            {
                if ((SomeScoreCandidate < 0 || AIPoints(i) > AIPoints(SomeScoreCandidate)) && Keeps(i))
                {
                    SomeScoreCandidate = i;
                }
            }

            // If an acceptable score is found, return it.
            if (SomeScoreCandidate >= 0)
            {
                return PropertyInfo(suggestions[SomeScoreCandidate].property);
            }

            // No acceptable suggestion found - choose the solution with damage control

            // Straights
            if (Scoreboard.LittleStraight == null)
            {
                return PropertyInfo(nameof(Scoreboard.LittleStraight));
            }
            if (Scoreboard.GreatStraight == null)
            {
                return PropertyInfo(nameof(Scoreboard.GreatStraight));
            }

            // FourSame
            if (Scoreboard.FourSame == null)
            {
                return PropertyInfo(nameof(Scoreboard.FourSame));
            }

            // Yatzy
            if (Scoreboard.Yatzy == null)
            {
                return PropertyInfo(nameof(Scoreboard.Yatzy));
            }

            // ThreeSame
            if (Scoreboard.ThreeSame == null)
            {
                return PropertyInfo(nameof(Scoreboard.ThreeSame));
            }

            // House
            if (Scoreboard.House == null)
            {
                return PropertyInfo(nameof(Scoreboard.House));
            }

            // TwoPairs
            if (Scoreboard.TwoPairs == null)
            {
                return PropertyInfo(nameof(Scoreboard.TwoPairs));
            }

            // Now there are only the Bonus-loosing ones left
            // Find the  score that jepodize bonus the least
            // All differences will be negative
            SomeScoreCandidate = -1;
            for (int i = 0; i < suggestions.Count; i++)
            {
                if ((SomeScoreCandidate < 0 || BonusDiff(i) > BonusDiff(SomeScoreCandidate)) && Keeps(i))
                {
                    SomeScoreCandidate = i;
                }
            }

            if (SomeScoreCandidate >= 0)
            {
                return PropertyInfo(suggestions[SomeScoreCandidate].property);
            }

            // If no suggestions at all

            // Ones
            if (Scoreboard.Ones == null)
            {
                return PropertyInfo(nameof(Scoreboard.Ones));
            }

            // Twos
            if (Scoreboard.Twos == null)
            {
                return PropertyInfo(nameof(Scoreboard.Twos));
            }

            // Threes
            if (Scoreboard.Threes == null)
            {
                return PropertyInfo(nameof(Scoreboard.Threes));
            }

            // Fours
            if (Scoreboard.Fours == null)
            {
                return PropertyInfo(nameof(Scoreboard.Fours));
            }

            // Fives
            if (Scoreboard.Fives == null)
            {
                return PropertyInfo(nameof(Scoreboard.Fives));
            }

            // Sixes
            if (Scoreboard.Sixes == null)
            {
                return PropertyInfo(nameof(Scoreboard.Sixes));
            }

            throw new Exception("No available score to set");
        }

        public override HoldInfo[]? Holds(int[] Results, VMScoreboard Scoreboard, int diceCount)
        {
            HoldInfo[]? ScoreHold = null;
            int AcceptableAIPoints = 10 - 2 * diceCount;
            int[] sortedScores = PlayerScore.CountScores(Results);

            // Find the largest number of equal dices (and which dice)
            int MostOccurencesIndex = -1;
            for (int i = 0; i < sortedScores.Length; i++)
            {
                if (MostOccurencesIndex < 0 || sortedScores[i] >= sortedScores[MostOccurencesIndex])
                {
                    MostOccurencesIndex = i;
                }
            }


            // If we find out how many dice results are needed for different goals
            // we can decide which to go for.
            List<DiceCandidate> missingDices = CalculateMissingDices(Scoreboard, Results, sortedScores, diceCount, MostOccurencesIndex);

            // Now we have to combine the missing with the possible gain to calculate the AI score
            // Hard to calculate the probability of getting the last x dices correct, especially because we do not here take into account
            // what we are aming at.
            for (int i = 0; i < missingDices.Count; i++)
            {
                // Calculate probability of reaching the goal
                double p = Rules.Prop(diceCount, missingDices[i].Needed, missingDices[i].Missing);
                missingDices[i].CombinedScore = missingDices[i].Score * p;
            }

            // Now we have to look for something more difficult
            // Find the choice with the smallest number of missing dices
            DiceCandidate? AimCandidate = null;
            foreach (DiceCandidate diceCandidate in missingDices)
            {
                if (AimCandidate == null || diceCandidate.CompareTo(AimCandidate) > 0)
                {
                    AimCandidate = diceCandidate;
                }
            }

            if (AimCandidate != null)
            {
                ScoreHold = HoldAimIfGreater(AimCandidate, Results, sortedScores, MostOccurencesIndex, 0);

                if (ScoreHold != null)
                {
                    return ScoreHold;
                }
            }

            return HoldNothing();
        }
    }
}
