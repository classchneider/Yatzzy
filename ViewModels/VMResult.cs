using YatzyRepository;

namespace ViewModels
{
    public class VMResult
    {
        Result result;

        public VMResult(Result res)
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
            get { return result.sixes; }
        }
        
        public int fives { get { return result.fives; } }
        public int fours { get { return result.fours; } }
        public int threes { get { return result.threes; } }
        public int twos { get { return result.twos; } }
        public int ones { get { return result.ones; } }

        int Sum
        {
            get
            {
                return sixes + fives + fours + threes + twos + ones;
            }
        }
    }
}