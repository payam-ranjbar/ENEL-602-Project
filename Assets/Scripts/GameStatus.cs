using AnalyticsData;
using UnityEngine;
using UnityEngine.Events;

public class GameStatus : MonoBehaviour
{
    [SerializeField] private int totalDigitCode = 3;
    [SerializeField] private UI ui;
    [SerializeField] private DecodeData[] decodeData;
    private int _decodedDigits;
    
    public UnityEvent onDecode;
    public UnityEvent onError;
    public UnityEvent onWin;

    public DecodeData CurrentData => decodeData[_decodedDigits];
    public DecodeData Decode()
    {
        onDecode?.Invoke();
        ui.Decode(_decodedDigits);
        _decodedDigits++;
        var won = totalDigitCode <= _decodedDigits;


        if (won)
        {
            Win();
            return null;
        }
        
        if (_decodedDigits <= 1)
        {
            DataGatherer.Instance.FistDecodeStarts();
        }
        else if(_decodedDigits >= 2)
        {
            DataGatherer.Instance.SecondDecodeStarts();
        }
        return decodeData[_decodedDigits];


    }

    public void Win()
    {
       onWin?.Invoke();
       DataGatherer.Instance.Win();
       DataGatherer.Instance.FinilizeRun();
       
    }

    public void Error()
    {
        DataGatherer.Instance.AddFails();
        onError?.Invoke();
    }
}