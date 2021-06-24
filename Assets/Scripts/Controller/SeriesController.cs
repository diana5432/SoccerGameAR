using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesController : Subject, Observer 
{
    // Observed subjects
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 

    [SerializeField] private GoalPanelController _goalPanel;

    // Parameters
    [SerializeField] private float _ballResetDuration = 3f;
    
    private int _maxTrials = 3;
    private int _trials;
    private int _score;
    private float _lastShotDistance = 0f;
    
    private int _phase;
    private bool _isPaused;


    private void Start()
    {
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);
        
        _isPaused = false;

        SeriesStart();
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.BallShot)
            _trials -= 1;

        if (notificationType == NotificationType.DistanceChange)
            _lastShotDistance = (float)value;

        if (notificationType == NotificationType.GoalHit)
        {
            int shotscore = (int)((float)value * _lastShotDistance * 1000);
            Notify(shotscore, NotificationType.ShotScore);
            
            _score += shotscore;
            Notify(_score, NotificationType.ScoreChange);
        }
        if (_trials <= 0)
        {
            Invoke("SeriesDone", _ballResetDuration);
        }
        else
            Invoke("ResetBall", _ballResetDuration);
    }

    public void SeriesStart()
    {
        _phase = (int) SeriesPhase.SCAN;
        Notify(0,NotificationType.SeriesScan);
        PrepareNewSeries();
    }

    public void SeriesScale()
    {
        if (_goalPanel.IsActive())
        {
            _phase = (int) SeriesPhase.SCALE;
            Notify(0,NotificationType.SeriesScale);
        }
    }

    public void SeriesPlay()
    {
        _phase = (int) SeriesPhase.PLAY;
        Notify(0,NotificationType.SeriesPlay);
        PrepareNewSeries();
    }

    private void SeriesDone()
    {
        _phase = (int) SeriesPhase.DONE;
        Notify(0, NotificationType.SeriesDone);
    }

    private void PrepareNewSeries()
    {
        _trials = _maxTrials;
        _score = 0;
        Notify(_score, NotificationType.ScoreChange);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void ResetBall()
    {
        _ball.ResetPosition();
    }

    public int GetPhase()
    {
        return _phase;
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Time.timeScale = 1f;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    // for debugging
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _goalPanel.SpawnAtPosition(new Vector3(0f, 0f, 4f));
        }
    } 
}
