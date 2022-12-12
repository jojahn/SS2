using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Model
{
    public class DeviceState
    {
        public double InitialDifficulty { get; set; } = 0;
        public int ICENodes { get; set; } = 0;
        public int Cost { get; set; } = 0;

        public DeviceState() { }

        public DeviceState(double initialDifficulty, int ICENodes, int cost)
        {
            InitialDifficulty = initialDifficulty;
            this.ICENodes = ICENodes;
            Cost = cost;
        }
    }
}
