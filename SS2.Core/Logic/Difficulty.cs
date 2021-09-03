using SS2.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Logic
{
    public class Difficulty
    {
        public static double ScaleCYBStat(DeviceState deviceState, PlayerState playerState)
        {
            return playerState.CYBStat * 0.05;
        }

        public static double ScaleHackSkill(DeviceState deviceState, PlayerState playerState)
        {
            return playerState.HackSkill * 0.1;
        }

        public static double GetFinalDifficulty(DeviceState deviceState, PlayerState playerState)
        {
            double difficulty = deviceState.InitialDifficulty;
            difficulty -= ScaleCYBStat(deviceState, playerState);
            difficulty -= ScaleHackSkill(deviceState, playerState);
            return difficulty;
        }
    }
}
