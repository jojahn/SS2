using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class DeviceState
    {
        public double InitialDifficulty { get; set; }
        public int ICENodes { get; set; }
        public int Cost { get; set; }

        public DeviceState(double initialDifficulty, int ICENodes, int cost)
        {
            InitialDifficulty = initialDifficulty;
            this.ICENodes = ICENodes;
            Cost = cost;
        }
    }
}
