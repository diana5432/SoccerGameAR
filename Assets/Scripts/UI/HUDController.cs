using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : Observer
{
    // Observed subject
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 
    // Referenced objects
    [SerializeField] private Image[] _ballImages;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _goalText;
    [SerializeField] private GameObject _pauseMenu;

    // Parameters
    [SerializeField] float _goalTextDuration = 2f;

    
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
            ShowGoalText(_goalTextDuration);
        }

        if (notificationType == NotificationType.BallShot)
        {
            HideBall((int)value % _ballImages.Length);
        }
        //if (notificationType == NotificationType.SeriesEnd)
            //show final points and menu quit/restart
        //if (notificationType == NotificationType.SeriesRestart)
            //ResetBalls();

    }

    public void ShowPauseMenu()
    {
        _pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString("D5");
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

    public void ResetBalls()
    {
        foreach (Image ball in _ballImages)
            ball.gameObject.SetActive(true);
    }

    private void ColorizeBall(int imageIndex, Color color)
    {
        _ballImages[imageIndex].color = color;
    }
}
