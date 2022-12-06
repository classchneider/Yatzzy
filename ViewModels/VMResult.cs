using YatzyRepository;

namespace ViewModels
{
    public class VMResult
    {
        Scoreboard result;

        public VMResult(Scoreboard res)
        {
            result = res;
        }

        public int id
        {
            get
            {
                return result.Id;
            }
        }
        public int sixes
        {
            get { return result.Sixes; }
        }
        
        public int fives { get { return result.Fives; } }
        public int fours { get { return result.Fours; } }
        public int threes { get { return result.Threes; } }
        public int twos { get { return result.Twos; } }
        public int ones { get { return result.Ones; } }

        int Sum
        {
            get
            {
                return sixes + fives + fours + threes + twos + ones;
            }
        }
    }
}