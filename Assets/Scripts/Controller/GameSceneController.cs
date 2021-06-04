using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : Observer
{
    // Observed subject
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 
    // Referenced objects
    [SerializeField] private HUDController _HUD;
    // Parameters
    [SerializeField] private int _maxTrials = 3;
    [SerializeField] private float _ballResetDuration = 3f;
    
    private int _trials;
    private int _points;

    private void Start()
    {
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);

        _trials = _maxTrials;
        _points = 0;
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
            Invoke("EndSeries", _ballResetDuration);
        else
            Invoke("ResetBall", _ballResetDuration);
    }

    private void ResetBall()
    {
        _ball.ResetPosition();
    }

    private void EndSeries()
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
