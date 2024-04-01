namespace YatzyRepository
{
    public class Rules
    {
        // Probability of making a certain dice every round
        public static double Prop(int diceCount, int needed, int missing)
        {
            if (missing == 0)
            {
                return 1;
            }

            if (diceCount == 3)
            {
                return 0;
            }

            double propResult = 1;
            double perRound = Convert.ToDouble(missing) / (3 - diceCount);

            // Every round
            int availableDices = 5 - (needed - missing);
            for (int i = diceCount; i < 3; i++)
            {
                double prop;
                // Throw the availableDices dies
                prop = (Math.Pow(1.0 / 6.0, perRound) * Math.Pow(5.0 / 6.0, availableDices - perRound))* availableDices;
                //for (int j = 0; j < availableDices; j++)
                //{
                //    prop += Math.Pow(1.0 / 6.0, perRound) * Math.Pow(5.0 / 6.0, availableDices - perRound);
                //}
                propResult *= prop;
                availableDices -= Convert.ToInt32(Math.Floor(perRound));
            }
            return propResult;
        }

        public static int GoodScore(string propName)
        {
            if (propName == nameof(Scoreboard.Ones))
            {
                return 3 * 1;
            }
            if (propName == nameof(Scoreboard.Twos))
            {
                return 3 * 2;
            }
            if (propName == nameof(Scoreboard.Threes))
            {
                return 3 * 3;
            }
            if (propName == nameof(Scoreboard.Fours))
            {
                return 3 * 4;
            }
            if (propName == nameof(Scoreboard.Fives))
            {
                return 3 * 5;
            }
            if (propName == nameof(Scoreboard.Sixes))
            {
                return 3 * 6;
            }
            if (propName == nameof(Scoreboard.Pair))
            {
                return 5 * 2;
            }
            if (propName == nameof(Scoreboard.TwoPairs))
            {
                return 4 * 2 + 5 * 2;
            }
            if (propName == nameof(Scoreboard.ThreeSame))
            {
                return 4 * 3;
            }
            if (propName == nameof(Scoreboard.FourSame))
            {
                return 4 * 4;
            }
            if (propName == nameof(Scoreboard.LittleStraight))
            {
                return 1 + 2 + 3 + 4 + 5;
            }
            if (propName == nameof(Scoreboard.GreatStraight))
            {
                return 2 + 3 + 4 + 5 + 6;
            }
            if (propName == nameof(Scoreboard.House))
            {
                return 5 * 3 + 4 * 2;
            }
            if (propName == nameof(Scoreboard.Chance))
            {
                return 5 * 5;
            }
            if (propName == nameof(Scoreboard.Yatzy))
            {
                return 50;
            }

            return -1;
        }

    }
}