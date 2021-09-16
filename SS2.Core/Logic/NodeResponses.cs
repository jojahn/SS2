using SS2.Core.Model;
using SS2.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SS2.Core.Logic
{
    public class NodeResponses
    {
        private Random _random;

        public NodeResponses()
        {
            _random = new Random();
        }

        public List<string> GetInitialResponses(Difficulty difficulty, DeviceState deviceState, PlayerState playerState)
        {
            double hackSkillDeduction = (-1) * Math.Round(Difficulty.ScaleHackSkill(deviceState, playerState) * 100);
            double CYBStatDeduction = (-1) * Math.Round(Difficulty.ScaleCYBStat(deviceState, playerState) * 100);
            double finalDifficulty = Math.Round(difficulty.Final);
            string nodeOrNodes = deviceState.ICENodes == 1 ? Resources.Resources.node : Resources.Resources.nodes;
            return new List<string>(new string[] {
                $"{Resources.Resources.InitialDifficulty}: {deviceState.InitialDifficulty * 100}%.",
                $"{Resources.Resources.HackSkill} {playerState.HackSkill}: {hackSkillDeduction}%.",
                $"{Resources.Resources.CYBStat} {playerState.CYBStat}: {CYBStatDeduction}%.",
                $"{Resources.Resources.FinalDifficulty}: {finalDifficulty}%",
                $"{deviceState.ICENodes} ICE {nodeOrNodes}."
            });
        }

        public string GetRandomResponse(bool success)
        {
            if (success)
            {
                return GetRandomSuccessResponse();
            } else
            {
                return GetRandomFailResponse();
            }
        }

        private string GetRandomSuccessResponse()
        {
            int next = _random.Next(0, 2);
            switch(next)
            {
                case 1:
                    return Resources.Resources.DataTransferInitiated;
                case 2:
                    return Resources.Resources.PasswordSpoofSuccessful;
                default:
                    return Resources.Resources.NodeInflitrated;
            }
        }

        private string GetRandomFailResponse()
        {
            int next = _random.Next(0, 2);
            switch (next)
            {
                case 1:
                    return Resources.Resources.BufferOverflow;
                case 2:
                    return Resources.Resources.PasswordSpoofSuccessful;
                default:
                    return Resources.Resources.AccessViolationDetected;
            }
        }
    }
}
