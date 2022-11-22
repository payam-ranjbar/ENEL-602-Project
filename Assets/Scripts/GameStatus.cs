using UnityEngine;
using UnityEngine.Events;

public class GameStatus : MonoBehaviour
{
    [SerializeField] private int totalDigitCode = 3;
    [SerializeField] private UI ui;
    private int _decodedDigits;
    
    public UnityEvent onDecode;
    public UnityEvent onError;
    public UnityEvent onWin;

    public void Decode()
    {
        onDecode?.Invoke();
        ui.Decode(_decodedDigits);
        _decodedDigits++;
        var won = totalDigitCode <= _decodedDigits;
        if (won) Win();
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