using UnityEngine;

namespace AnalyticsData
{
    public class Diagnostics
    {
       private string _participant;
       private float _firstCodeDuration;
       private float _secondCodeDuration;
       private float _thirdCodeDuration;
       private int _fails;


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

       public int Fails
       {
           get => _fails;
           set => _fails = value;
       }


       public string SerializeToCSV()
        {
            var str = $"{_participant},{_firstCodeDuration},\"{_secondCodeDuration}\",\"{_thirdCodeDuration}\",\"{_fails}\"";
            return SerializeToCSV();
        }

    }
}