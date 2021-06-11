using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesController : Subject, Observer 
{
    // Observed subjects
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 

    // Parameters
    [SerializeField] private float _ballResetDuration = 3f;
    
    private int _maxTrials = 3;
    private int _trials;
    private int _points;
    private int _phase;


    private void Start()
    {
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);

        SeriesStart();
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.BallShot)
            _trials -= 1;

        if (notificationType == NotificationType.GoalHit)
        {
            _points += (int)((float)value * 1000);
            Notify(_points, NotificationType.ScoreChange);
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
        _trials = _maxTrials;
        _points = 0;
        Notify(_points, NotificationType.ScoreChange);
    }

    public void SeriesScale()
    {
        _phase = (int) SeriesPhase.SCALE;
        Notify(0,NotificationType.SeriesScale);
    }

    public void SeriesPlay()
    {
        _phase = (int) SeriesPhase.PLAY;
        Notify(0,NotificationType.SeriesPlay);
    }

    private void SeriesDone()
    {
        _phase = (int) SeriesPhase.DONE;
        Notify(0, NotificationType.SeriesDone);
    }

    private void ResetBall()
    {
        _ball.ResetPosition();
    }

    public int GetPhase()
    {
        return _phase;
    }

    // ONLY FOR DEBUGGING
    private void Update() {
        // print goal distance with J
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_phase == (int) SeriesPhase.SCAN)
            {
                SeriesScale();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_phase == (int) SeriesPhase.SCALE)
            {
                SeriesPlay();
            }
        }
    
        if (Input.GetKeyDown(KeyCode.P))
            Debug.Log("Phase: " +_phase);
                
    }
}
