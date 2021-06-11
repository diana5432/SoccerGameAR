using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, Observer 
{
    // Observed subject
    [SerializeField] private SeriesController _series;
    [SerializeField] private GoalController _goal; 
    [SerializeField] private BallController _ball; 
    // Referenced objects
    [SerializeField] private Image[] _ballImages;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _scanText;
    [SerializeField] private GameObject _scaleText;
    [SerializeField] private GameObject _kickOffText;
    [SerializeField] private GameObject _goalText;
    [SerializeField] private GameObject _missedText;
    [SerializeField] private GameObject _doneText;
    // Parameters
    [SerializeField] float _promptDuration = 2f;

    private GameObject _currentStatusText;
    
    private void Start()
    {
        if (_series!=null)
            _series.RegisterObserver(this);
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);
        
        if (!_scanText.activeSelf)
            ShowStatusText(_scanText);
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
        {
            HideStatusText();
            HidePauseMenu();
            ResetBalls();
            ShowStatusText(_scanText);
        }
        if (notificationType == NotificationType.SeriesScale)
        {
            HideStatusText();
            ShowStatusText(_scaleText);
        }
        if (notificationType == NotificationType.SeriesPlay)
        {
            HideStatusText();
            ShowStatusText(_kickOffText, _promptDuration);
        }
        if (notificationType == NotificationType.BallShot)
        {
            HideBall((int)value % _ballImages.Length);
        }
        if (notificationType == NotificationType.GoalHit)
        {
            ShowStatusText(_goalText, _promptDuration);
        }
        if (notificationType == NotificationType.ScoreChange)
        {
            UpdateScore((int) value);
        }
        if (notificationType == NotificationType.SeriesDone)
        {
            // TODO show final points and menu quit/restart
            ShowStatusText(_doneText);
            Invoke("ShowPauseMenu", 1f);
        }
    }

    private void ShowPauseMenu()
    {
        _pauseMenu.SetActive(true);
        if (_series.GetPhase() == ((int) SeriesPhase.DONE))
            _resumeButton.SetActive(false);
        else
            if (!_resumeButton.activeSelf)
                _resumeButton.SetActive(true);
    }

    private void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenu.activeSelf)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString("D5");
    }

    private void ShowStatusText(GameObject statusText, float duration)
    {
        HideStatusText();
        _currentStatusText = statusText;
        _currentStatusText.SetActive(true);
        Invoke("HideStatusText", duration);
    }

    private void ShowStatusText(GameObject statusText)
    {
        _currentStatusText = statusText;
        _currentStatusText.SetActive(true);
    }

    private void HideStatusText()
    {
        if (_currentStatusText!=null && _currentStatusText.activeSelf)
            _currentStatusText.SetActive(false);
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
