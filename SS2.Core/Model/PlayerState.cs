using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class PlayerState
    {
        public int HackSkill { get; set; }
        public int CYBStat { get; set; }
        public int Nanites { get; set; }

        public PlayerState() { }

        public PlayerState(int hackSkill, int CYBStat, int nanites)
        {
            HackSkill = hackSkill;
            this.CYBStat = CYBStat;
            Nanites = nanites;
        }
    }
}
