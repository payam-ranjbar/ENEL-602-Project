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

        return decodeData[_decodedDigits];


    }

    public void Win()
    {
       onWin?.Invoke();
    }

    public void Error()
    {
        onError?.Invoke();
    }
}