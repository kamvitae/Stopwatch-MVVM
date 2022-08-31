using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;
using HF_16_MVVM_Stopwatch.Model;

namespace HF_16_MVVM_Stopwatch.ViewModel
{
    public class StopwatchViewModel : INotifyPropertyChanged
    {
        private StopwatchModel _stopwatchModel = new StopwatchModel();
        private DispatcherTimer _timer = new DispatcherTimer();

        // obiekt modelu ma podobną właściwość dzięki której ta pobiera zawartość prywatnego pola DateTime? _started
        public bool Running { get => _stopwatchModel.Running; }
        public StopwatchViewModel()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += TimerTick;
            _timer.Start();

            _stopwatchModel.LapTimeUpdated += LapTimeUpdatedEventHandler;
        }
        //poniższe metody widoku modelu jedynie wywołują analogiczne medoty swojego obiektu modelu
        //w modelu jest logika tych metod. Metody modelu widoku służą za pośrednika dla analogicznych obiektów przycisków widoku
        // które mają wywoływać metody modelu
        public void Start()
        {
            _stopwatchModel.Start();
        }
        public void Stop()
        {
            _stopwatchModel.Stop();
        }
        public void Lap()
        {
            _stopwatchModel.Lap();
        }
        public void Reset()
        {
            bool running = Running;
            _stopwatchModel.Reset();
            if (running)
                _stopwatchModel.Start();
        }

        int _lastHours;
        int _lastMinutes;
        decimal _lastSeconds;
        bool _lastRunning;
        private void TimerTick(object sender, EventArgs e)
        {
            if (_lastRunning!= Running)
            {
                _lastRunning = Running;

                OnPropertyChanged("Running");
            }
            if (_lastHours != Hours)
            {
                _lastHours = Hours;
                OnPropertyChanged("Hours");
            }
            if (_lastMinutes != Minutes)
            {
                _lastMinutes = Minutes;
                OnPropertyChanged("Minutes");
            }
            if (_lastSeconds != Seconds)
            {
                _lastSeconds = Seconds;
                OnPropertyChanged("Seconds");
            }
        }
        //publiczne właściwości widoku modelu,  pobierają dane z właściwości obiektu modelu sprawdzając najpierw czy te nie są null
        public int Hours
        {
            get => _stopwatchModel.Elapsed.HasValue ? _stopwatchModel.Elapsed.Value.Hours : 0;
        }
        public int Minutes
        {
            get => _stopwatchModel.Elapsed.HasValue ? _stopwatchModel.Elapsed.Value.Minutes : 0;
        }
        public decimal Seconds
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return (decimal)_stopwatchModel.Elapsed.Value.Seconds +
                                    (_stopwatchModel.Elapsed.Value.Milliseconds * .001M);
                else
                    return 0.0M;
            }
        }

        int _lastLapHours;
        int _lastLapMinutes;
        decimal _lastLapSeconds;
        private void LapTimeUpdatedEventHandler(object sender, EventArgs e)
        {
            if (_lastLapHours != Hours)
            {
                _lastLapHours = Hours;
                OnPropertyChanged("LapHours");
            }
            if (_lastLapMinutes != Minutes)
            {
                _lastLapMinutes = Minutes;
                OnPropertyChanged("LapMinutes");
            }
            if (_lastLapSeconds != Seconds)
            {
                _lastLapSeconds = Seconds;
                OnPropertyChanged("LapSeconds");
            }
        }
        //publiczne właściwości widoku modelu,  pobierają dane z właściwości obiektu modelu sprawdzając najpierw czy te nie są null
        public int LapHours
        {
            get => _stopwatchModel.LapTime.HasValue ? _stopwatchModel.LapTime.Value.Hours : 0;
        }
        public int LapMinutes
        {
            get => _stopwatchModel.LapTime.HasValue ? _stopwatchModel.LapTime.Value.Minutes : 0;
        }
        public decimal LapSeconds
        {
            get
            {
                if (_stopwatchModel.LapTime.HasValue)
                    return (decimal)_stopwatchModel.LapTime.Value.Seconds +
                                    (_stopwatchModel.LapTime.Value.Milliseconds * .001M);
                else
                    return 0.0M;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
