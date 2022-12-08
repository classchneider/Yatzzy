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
        public int? sixes
        {
            get { return result.Sixes; }
        }
        
        public int? fives { get { return result.Fives; } }
        public int? fours { get { return result.Fours; } }
        public int? threes { get { return result.Threes; } }
        public int? twos { get { return result.Twos; } }
        public int? ones { get { return result.Ones; } }

        int Sum
        {
            get
            {
                return sixes!=null?sixes.Value:0 + 
                    fives != null? fives.Value : 0 + 
                    fours != null? fours.Value : 0 + 
                    threes != null? threes.Value : 0 + 
                    twos != null? twos.Value : 0 + 
                    ones != null? ones.Value : 0;
            }
        }
    }
}