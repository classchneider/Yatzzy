using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzyRepository
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int NextPlayerScoreIndex { get; set; } = 0;
        public List<PlayerScore> PlayerScores { get; set; } = new List<PlayerScore>();
    }

}
