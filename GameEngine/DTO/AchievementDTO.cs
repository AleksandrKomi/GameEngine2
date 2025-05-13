using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.DTO
{
    public class AchievementDTO
    {
        public string Type { get; init; }

        public int Goal { get; init; }

        public string Text { get; init; }

        public int Score { get; init; }

        public string Name { get; init; }
    }
}
