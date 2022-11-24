
using UnityEngine;

namespace AnalyticsData
{
    public class DataGatherer : Singleton<DataGatherer>
    {
        
        private Diagnostics _currentData;

        private float _snapTime;

        private bool _init;
        private void Awake()
        {
            _currentData = new Diagnostics();
        }

        public void Init()
        {
            _init = true;
            _currentData = new Diagnostics {Participant = "Participant"};
            _snapTime = Time.unscaledTime;

        }

        public void FistDecodeStarts()
        {
            if(!_init) return;

            var firstDuration = Time.unscaledTime - _snapTime;

            _currentData.FirstCodeDuration = firstDuration;

            _snapTime = Time.unscaledTime;
        }

        public void SecondDecodeStarts()
        {
            if(!_init) return;

            var secondDuration = Time.unscaledTime - _snapTime;

            _currentData.SecondCodeDuration = secondDuration;

            _snapTime = Time.unscaledTime;
        }


        public void Win()
        {
            if(!_init) return;

            var thirdDuration = Time.unscaledTime - _snapTime;

            _currentData.ThirdCodeDuration = thirdDuration;

            _snapTime = Time.unscaledTime;
        }

        public void AddFails()
        {
            if(!_init) return;
            _currentData.Fails++;
        }

        public void FinilizeRun()
        {
            _init = false;
            CSVSerializer.Create(_currentData);
        }
        
    }
}