using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : Observer
{
    [SerializeField] private Image[] _ballImages;
    [SerializeField] private GameObject _goalText;

    private int _ballsGone = 0;

    private GameSceneController gameSceneController;
    [SerializeField] private GoalController _goal;


    private void Start()
    {
        if (_goal != null)
            _goal.RegisterObserver(this);
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.GoalHit)
        {
            ShowGoalText(2f);
            _ballsGone += 1;
            int i = _ballImages.Length - _ballsGone;
            HideBall(i);
        }
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
