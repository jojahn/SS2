using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model.State
{

    public enum Game {
        IDLE = 0,
        STARTED = 1,
        FAILED = 2,
        WON = 3
    }

    public class Device
    {
        public double InitialDifficulty { get; set; }
        public int ICENodes { get; set; }
    }

    public class Player
    {
        public int HackingSkill { get; set; }
        public int CYBStat { get; set; }
        public int Nanites { get; set; }
    }
}
