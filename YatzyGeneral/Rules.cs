namespace YatzyGeneral
{
    public class Rules
    {
        private static int MaxScore(string propName)
        {
            if (propName == nameof(Scoreboard.Ones))
            {
                return 5 * 1;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Twos))
            {
                return 5 * 2;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Threes))
            {
                return 5 * 3;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Fours))
            {
                return 5 * 4;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Fives))
            {
                return 5 * 5;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Sixes))
            {
                return 5 * 6;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Pair))
            {
                return 6 * 2;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.TwoPairs))
            {
                return 6 * 2 + 5 * 2;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.ThreeSame))
            {
                return 6 * 3;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.FourSame))
            {
                return 6 * 4;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.LittleStraight))
            {
                return 1 + 2 + 3 + 4 + 5;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.GreatStraight))
            {
                return 2 + 3 + 4 + 5 + 6;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.House))
            {
                return 6 * 2 + 5 * 2;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Chance))
            {
                return 6 * 5;
            }
            if (propName == nameof(CurrentPlayerScore.VMScoreboard.Yatzy))
            {
                return 50;
            }

            return -1;
        }

    }
}