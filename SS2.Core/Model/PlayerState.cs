using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class PlayerState
    {
        public int HackSkill { get; set; } = 0;
        public int CYBStat { get; set; } = 0;
        public int Nanites { get; set; } = 0;

        public PlayerState() { }

        public PlayerState(int hackSkill, int CYBStat, int nanites)
        {
            HackSkill = hackSkill;
            this.CYBStat = CYBStat;
            Nanites = nanites;
        }
    }
}
