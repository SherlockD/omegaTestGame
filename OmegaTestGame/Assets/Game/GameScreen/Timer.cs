using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action CallOnTimerEnd;

    public enum Mode
    {
        Once,
        Сontinued
    }
    
    private enum Status
    {
        Idle,
        Start,
        InProgress,
        End
    }

    private int _seconds;
    private Status _status;
    private Mode _mode;
    private DateTime _endTime;
    
    private void FixedUpdate()
    {
        switch (_status)
        {
                case Status.Idle:
                    return;
                case Status.Start:
                    _endTime = DateTime.Now.AddSeconds(_seconds);
                    _status = Status.InProgress;
                    break;
                case Status.InProgress:
                    if (DateTime.Now >= _endTime)
                    {                       
                        _status = Status.End;
                    }
                    break;
                case Status.End:
                    CallOnTimerEnd?.Invoke();
                    switch (_mode)
                    {
                            case Mode.Once:
                                _status = Status.Idle;
                                break;
                            case Mode.Сontinued:
                                _status = Status.Start;
                                break;
                    }
                    break;
        }
    }
    
    public void StartTimer(int seconds,Mode mode)
    {
        _seconds = seconds;
        _mode = mode;
        _status = Status.Start;
    }
}
