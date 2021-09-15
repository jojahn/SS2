using SS2.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.Logic
{
    public class NodeResponses
    {
        private Random _random;

        public NodeResponses()
        {
            _random = new Random();
        }

        // Success
        public static string NodeInflitrated = "Node inflitrated.";
        public static string PasswordSpoofSuccessful = "Password spoof successful.";
        public static string DataTransferInitiated = "Data transfer initiated.";

        // Failed
        public static string IllegalCoreAccess = "Illegal core access!";
        public static string BufferOverflow = "Buffer overflow.";
        public static string AccessViolationDetected = "Access violation detected!";

        public List<string> GetInitialResponses(Difficulty difficulty, DeviceState deviceState, PlayerState playerState)
        {
            double hackSkillDeduction = (-1) * Math.Round(Difficulty.ScaleHackSkill(deviceState, playerState) * 100);
            double CYBStatDeduction = (-1) * Math.Round(Difficulty.ScaleCYBStat(deviceState, playerState) * 100);
            double finalDifficulty = Math.Round(difficulty.Final);
            string nodeOrNodes = deviceState.ICENodes == 1 ? "node" : "nodes";
            return new List<string>(new string[] {
                $"Initial Difficulty: {deviceState.InitialDifficulty * 100}%.",
                $"Hack Skill {playerState.HackSkill}: {hackSkillDeduction}%.",
                $"CYB stat {playerState.CYBStat}: {CYBStatDeduction}%.",
                $"Final Difficulty: {finalDifficulty}%",
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
                    return DataTransferInitiated;
                case 2:
                    return PasswordSpoofSuccessful;
                default:
                    return NodeInflitrated;
            }
        }

        private string GetRandomFailResponse()
        {
            int next = _random.Next(0, 2);
            switch (next)
            {
                case 1:
                    return BufferOverflow;
                case 2:
                    return PasswordSpoofSuccessful;
                default:
                    return AccessViolationDetected;
            }
        }
    }
}
