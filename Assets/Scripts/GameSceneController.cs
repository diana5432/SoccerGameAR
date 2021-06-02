using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : Observer
{
    [SerializeField] private GoalController _goal; // Subject
    [SerializeField] private BallController _ball; // Subject
    [SerializeField] private int _trials = 3;
    [SerializeField] private int _points = 0;

    private void Start()
    {
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.GoalHit)
        {
            Debug.Log("GOOOOOAAAL!");
            _trials -= 1;
            _points += 100;
        }
        if (_trials <= 0)
            //EndSeries();
            Debug.Log("series end!");
        else
            Invoke("ResetBall", 3);
    }

    private void ResetBall()
    {
        _ball.ResetPosition();
    }

    private void EndSeries()
    {
        throw new NotImplementedException();
    }
}
