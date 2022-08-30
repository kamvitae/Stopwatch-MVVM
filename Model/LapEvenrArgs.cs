using System;
using System.Collections.Generic;
using System.Text;

namespace HF_16_MVVM_Stopwatch.Model
{
    public class LapEvenrArgs:EventArgs
    {
        public TimeSpan? LapTime { get; private set; }
        public LapEvenrArgs (TimeSpan? lapTime)
        {
            LapTime = lapTime;
        }
    }
}
