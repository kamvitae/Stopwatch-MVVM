using System;
using System.Collections.Generic;
using System.Text;

namespace HF_16_MVVM_Stopwatch.Model
{
    public class StopwatchModel
    {
        private DateTime? _started;
        private TimeSpan? _previousElapsedTime;
        public bool Running
        {
            get => _started.HasValue;
        }
        public TimeSpan? Elapsed
        {
            get
            {
                if (_started.HasValue)
                {
                    if (_previousElapsedTime.HasValue)
                        return CalculateTimeElapsedSinceStarted() + _previousElapsedTime;
                    else
                        return CalculateTimeElapsedSinceStarted();
                }
                else
                    return _previousElapsedTime;
            }
        }

        private TimeSpan CalculateTimeElapsedSinceStarted()
        {
            return DateTime.Now - _started.Value;
        }
        public void Start()
        {
            _started = DateTime.Now;
            if (!_previousElapsedTime.HasValue)
                _previousElapsedTime = new TimeSpan(0);
        }
        public void Stop()
        {
            if (_started.HasValue)
                _previousElapsedTime += DateTime.Now - _started.Value;
            _started = null;
        }
        public void Reset()
        {
            _previousElapsedTime = null;
            _started = null;
            LapTime = null;
        }
        public TimeSpan? LapTime { get; private set; }
        public void Lap()
        {
            LapTime = Elapsed;
            OnLapTimeUpdated(LapTime);
        }
        public event EventHandler<LapEvenrArgs> LapTimeUpdated;
        private void OnLapTimeUpdated(TimeSpan? lapTime)
        {
            LapTimeUpdated?.Invoke(this, new LapEvenrArgs(lapTime));

            /* WSPÓŁCZESNY ZAPIS (u góry)
             * 
             * STARSZY ZAPIS:
            
             EventHandler<LapEvenrArgs> lapTimeUpdated = LapTimeUpdated;

            if (lapTimeUpdated != null)
                lapTimeUpdated(this, new LapEvenrArgs(lapTime)); */
        }
        public StopwatchModel()
        {
            Reset(); ;
        }
    }
}
