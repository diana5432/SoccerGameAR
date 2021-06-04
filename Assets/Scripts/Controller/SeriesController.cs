using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesController : Observer
{
    // Observed subject
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 
    // Referenced objects
    [SerializeField] private HUDController _HUD;
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

        _trials = _maxTrials;
        _points = 0;
        _phase = (int) SeriesPhase.SCAN;
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.BallShot)
            _trials -= 1;

        if (notificationType == NotificationType.GoalHit)
        {
            _points += (int)((float)value * 1000);
            _HUD.UpdateScore(_points);
        }
        if (_trials <= 0)
        {
            _phase = (int) SeriesPhase.DONE;
            Invoke("SeriesDone", _ballResetDuration);
        }
        else
            Invoke("ResetBall", _ballResetDuration);
    }

    private void ResetBall()
    {
        _ball.ResetPosition();
    }

    private void SeriesDone()
    {
        _HUD.ShowPauseMenu();
        _ball.FreezePosition();
    }

    public void RestartSeries()
    {
        _HUD.HidePauseMenu();
        _trials = _maxTrials;
        _points = 0;
        _ball.ResetPosition();
        _ball.SetActualPower(0f);
        _HUD.UpdateScore(_points);
        _HUD.ResetBalls();
    }

    // ONLY FOR DEBUGGING
    private void Update() {
        if (Input.GetKeyDown(KeyCode.N))
            if (_phase == (int) SeriesPhase.SCAN)
                _phase = (int) SeriesPhase.SCALE;
        
        if (Input.GetKeyDown(KeyCode.M))
            if (_phase == (int) SeriesPhase.SCALE)
                _phase = (int) SeriesPhase.PLAY;
        
        
    }
}

public enum SeriesPhase
{
    SCAN,   // scan for goal image tracker
    SCALE,  // scale goal 
    PLAY,   // kick off
    DONE    // show rating
}