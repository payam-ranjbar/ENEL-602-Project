
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnalyticsData
{
    public class DataGatherer : Singleton<DataGatherer>
    {
        
        private Diagnostics _currentData;

        private float _snapTime;

        private bool _init;

        private int _fails;
        private void Awake()
        {
            _currentData = new Diagnostics();
        }

        public void Init()
        {
            if(_init) return;
            _init = true;
            _currentData = new Diagnostics {Participant = "Unknown Participant"};
            _snapTime = Time.unscaledTime;

        }

        public void FistDecodeStarts()
        {
            if(!_init) return;

            var firstDuration = Time.unscaledTime - _snapTime;

            _currentData.FirstCodeDuration = firstDuration;

            _currentData.FailsLevel1 = _fails;
            _fails = 0;
            
            _snapTime = Time.unscaledTime;
        }

        public void SecondDecodeStarts()
        {
            if(!_init) return;

            var secondDuration = Time.unscaledTime - _snapTime;

            _currentData.SecondCodeDuration = secondDuration;
            _currentData.FailsLevel2 = _fails;
            _fails = 0;
            _snapTime = Time.unscaledTime;
        }


        public void Win()
        {
            if(!_init) return;

            var thirdDuration = Time.unscaledTime - _snapTime;

            _currentData.ThirdCodeDuration = thirdDuration;
            _currentData.FailsLevel3 = _fails;
            _fails = 0;
            _snapTime = Time.unscaledTime;
        }

        public void AddFails()
        {
            if(!_init) return;
            _fails++;
        }

        public void FinilizeRun()
        {
            _init = false;
            CSVSerializer.Create(_currentData);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2)) Restart();
            if(Input.GetKeyDown(KeyCode.F3)) Init();
        }

        private void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}