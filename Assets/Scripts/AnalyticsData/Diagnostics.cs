using UnityEngine;

namespace AnalyticsData
{
    public class Diagnostics
    {
       private string _participant;
       private float _firstCodeDuration;
       private float _secondCodeDuration;
       private float _thirdCodeDuration;
       private int _failsLevel1;
       private int _failsLevel2;
       private int _failsLevel3;

       public int FailsLevel2
       {
           get => _failsLevel2;
           set => _failsLevel2 = value;
       }

       public int FailsLevel3
       {
           get => _failsLevel3;
           set => _failsLevel3 = value;
       }

       public string Participant
       {
           get => _participant;
           set => _participant = value;
       }

       public float FirstCodeDuration
       {
           get => _firstCodeDuration;
           set => _firstCodeDuration = value;
       }

       public float SecondCodeDuration
       {
           get => _secondCodeDuration;
           set => _secondCodeDuration = value;
       }

       public float ThirdCodeDuration
       {
           get => _thirdCodeDuration;
           set => _thirdCodeDuration = value;
       }

       public int FailsLevel1
       {
           get => _failsLevel1;
           set => _failsLevel1 = value;
       }


       public string SerializeToCSV()
       {
           var totalDuration = _firstCodeDuration + _secondCodeDuration + _thirdCodeDuration;
           var totalFails = _failsLevel1 + _failsLevel2 + _failsLevel3;
            var str = $"{_participant},{totalDuration},\"{totalFails}\",\"{_firstCodeDuration}\",\"{_secondCodeDuration}\",\"{_thirdCodeDuration}\",\"{_failsLevel1}\",\"{_failsLevel2}\",\"{_failsLevel3}\"";
            return str;
        }

    }
}