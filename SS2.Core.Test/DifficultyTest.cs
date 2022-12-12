using SS2.Core.Logic;
using SS2.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SS2.Core.Test
{
    public class DifficultyTest
    {
        Difficulty Difficulty = new Difficulty(new DeviceState(), new PlayerState());
        
        [Fact]
        public void TestDifficultyFinalIsSameAsInitialWithNoSkills()
        {
            Difficulty Difficulty = new Difficulty(new DeviceState(0, 0, 0), new PlayerState());
            Assert.Equal(0, Difficulty.Final);
        }

        [Fact]
        public void TestFinalDifficultyIsLessThanInitialWithSomeSkills()
        {
            Difficulty Difficulty = new Difficulty(new DeviceState(0.75, 0, 0), new PlayerState(1, 1, 0));
            Assert.True(Difficulty.Final < Difficulty.Initial);
        }

        [Fact]
        public void TestFinalDifficultyIsEqualOrGreaterZeroWithExtremeSkills()
        {
            Difficulty Difficulty = new Difficulty(new DeviceState(0.1, 0, 0), new PlayerState(Int32.MaxValue, Int32.MaxValue, 0));
            Assert.True(Difficulty.Final >= 0);
        }
    }
}
