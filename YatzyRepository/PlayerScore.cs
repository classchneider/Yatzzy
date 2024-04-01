using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzyRepository
{
    // Bridge "table"
    // Implements many-to-many relation between Player and Scoreboard
    public class PlayerScore
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public Scoreboard Scoreboard { get; set; }

        public static int[] CountScores(int[] scores)
        {
            int[] candidates = new int[] { 0, 0, 0, 0, 0, 0 };
            foreach (int roll in scores)
            {
                candidates[roll - 1]++;
            }
            return candidates;
        }

    }

}
