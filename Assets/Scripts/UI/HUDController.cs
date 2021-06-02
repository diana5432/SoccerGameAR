using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : Observer
{
    [SerializeField] private GoalController _goal; // Subject
    [SerializeField] private BallController _ball; // Subject

    [SerializeField] private Image[] _ballImages;
    [SerializeField] private GameObject _goalText;

    private GameSceneController gameSceneController;


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
            Debug.Log("HUDController received GoalHit");
            ShowGoalText(2f);
        }

        if (notificationType == NotificationType.BallShot)
        {
            Debug.Log("HUDController received BallShot with value " + value);
            HideBall((int)value % _ballImages.Length);
        }
        //if (notificationType == NotificationType.SeriesEnd)
            //show final points and menu quit/restart
        //if (notificationType == NotificationType.SeriesRestart)
            //ResetBalls();

    }

    private void ShowGoalText(float duration)
    {
        _goalText.gameObject.SetActive(true);
        Invoke("HideGoalText", duration);
    }

    private void HideGoalText()
    {
        _goalText.gameObject.SetActive(false);
    }

    private void HideBall(int imageIndex)
    {
        _ballImages[imageIndex].gameObject.SetActive(false);
    }

    private void ResetBalls()
    {
        foreach (Image ball in _ballImages)
            ball.gameObject.SetActive(true);
    }
}
