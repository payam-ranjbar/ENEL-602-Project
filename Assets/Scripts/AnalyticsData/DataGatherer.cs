
using UnityEngine;

namespace AnalyticsData
{
    public class DataGatherer : Singleton<DataGatherer>
    {
        
        private Diagnostics _currentData;

        private float _snapTime;

        private void Awake()
        {
            _currentData = new Diagnostics();
        }

        public void Init()
        {
            _currentData = new Diagnostics {Participant = "Participant"};
        }

        public void FistDecodeStarts()
        {
            _snapTime = Time.unscaledTime;
        }

        public void SecondDecodeStarts()
        {
            var firstDuration = Time.unscaledTime - _snapTime;

            _currentData.FirstCodeDuration = firstDuration;

            _snapTime = Time.unscaledTime;
        }

        public void ThirdDecodeStarts()
        {
            var secondDuration = Time.unscaledTime - _snapTime;

            _currentData.SecondCodeDuration = secondDuration;

            _snapTime = Time.unscaledTime;
        }

        public void Win()
        {
            var thirdDuration = Time.unscaledTime - _snapTime;

            _currentData.ThirdCodeDuration = thirdDuration;

            _snapTime = Time.unscaledTime;
        }

        public void AddFails()
        {
            _currentData.Fails++;
        }

        public void FinilizeRun()
        {
            CSVSerializer.Create(_currentData);
        }
        
    }
}