using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : Observer
{
    [SerializeField] private GoalController _goal;
    [SerializeField] private int _trials = 3;
    [SerializeField] private int _points = 0;

    private void Start()
    {
        if (_goal!=null)
            _goal.RegisterObserver(this);
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.GoalHit)
        {
            Debug.Log("GOOOOOAAAL!");
            _trials -= 1;
            _points += 100;
        }
        if (_trials < 1)
            //EndSeries();
            Debug.Log("series end!");
        //else
            //ResetBall();
    }

    private void ResetBall()
    {
        throw new NotImplementedException();
    }

    private void EndSeries()
    {
        throw new NotImplementedException();
    }
}

public enum NotificationType
{
    GoalHit
}
