using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{           
    internal class Timer
    {
        float time;
        public Timer()
        {
            ResetTimer();
        }
        public void ResetTimer()
        {
            time = Environment.TickCount;
        }
        public bool DurationPassed(float seconds)
        {
            var timePassed = Environment.TickCount - time;

            if (timePassed < seconds * 1000) return false;

            ResetTimer();
            return true;
        }
    }
}
