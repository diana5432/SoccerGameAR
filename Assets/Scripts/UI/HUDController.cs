using System.Collections;
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
    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _placingMenu;
    [SerializeField] private GameObject _scalingMenu;
    [SerializeField] private GameObject _powerBar;
    [SerializeField] private GameObject _scanText;
    [SerializeField] private GameObject _scaleText;
    [SerializeField] private GameObject _kickOffText;
    [SerializeField] private GameObject _goalText;
    [SerializeField] private GameObject _missedText;
    [SerializeField] private GameObject _doneText;
    // Parameters
    [SerializeField] float _promptDuration = 3f;

    private GameObject _currentStatusText;
    
    private void Awake()
    {
        if (_series!=null)
            _series.RegisterObserver(this);
        if (_goal!=null)
            _goal.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);
    }
    private void Start() 
    {
        if (!_scanText.activeSelf)
            ShowStatusText(_scanText);
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
        {
            HidePauseMenu();
            _powerBar.SetActive(false);
            _placingMenu.SetActive(true);
            ResetBalls();
            UpdateDistance(0f);
            ShowStatusText(_scanText);
        }
        if (notificationType == NotificationType.SeriesScale)
        {
            _placingMenu.SetActive(false);
            _scalingMenu.SetActive(true);
            ShowStatusText(_scaleText);
        }
        if (notificationType == NotificationType.SeriesPlay)
        {
            HidePauseMenu();
            _scalingMenu.SetActive(false);
            _powerBar.SetActive(true);
            ResetBalls();
            UpdateDistance(0f);
            StartCoroutine(PromptStatusText(_kickOffText, _promptDuration));
        }
        if (notificationType == NotificationType.BallShot)
        {
            HideBall((int)value % _ballImages.Length);
        }
        if (notificationType == NotificationType.DistanceChange)
        {
            UpdateDistance((float)value);
        }
        if (notificationType == NotificationType.GoalHit)
        {
            StartCoroutine(PromptStatusText(_goalText, _promptDuration));
        }
        if (notificationType == NotificationType.ScoreChange)
        {
            UpdateScore((int) value);
        }
        if (notificationType == NotificationType.SeriesDone)
        {
            ShowStatusText(_doneText);
            _pauseButton.interactable = false;
        }
    }

    public void ShowPauseMenu()
    {
        if (_series.GetPhase() == ((int) SeriesPhase.SCAN))
            _placingMenu.SetActive(false);
        if (_series.GetPhase() == ((int) SeriesPhase.SCALE))
            _scalingMenu.SetActive(false);

        _pauseMenu.SetActive(true);

        if (_series.GetPhase() == ((int) SeriesPhase.DONE))
        {       
            _resumeButton.SetActive(false);
            _restartButton.SetActive(true);
            _pauseButton.interactable = false;
        }
        else
        {
            if (!_resumeButton.activeSelf)
                _resumeButton.SetActive(true);
            
            if (_restartButton.activeSelf)
                _restartButton.SetActive(false);

            _series.PauseGame();
        }
    }

    private void HidePauseMenu()
    {
        _pauseMenu.SetActive(false);

        if (_series.GetPhase() == ((int) SeriesPhase.SCAN))
        {
            _placingMenu.SetActive(true);
            _pauseButton.interactable = true;
        }
        if (_series.GetPhase() == ((int) SeriesPhase.SCALE))
            _scalingMenu.SetActive(true);

        if (_series.GetPhase() == ((int) SeriesPhase.PLAY))
            _pauseButton.interactable = true;

        if (_series.IsPaused())
            _series.PauseGame();
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

    private void UpdateDistance(float distance)
    {
        _distanceText.text = distance.ToString("00.0");
    }

    IEnumerator PromptStatusText(GameObject statusText, float duration)
    {
        HideStatusText();
        _currentStatusText = statusText;
        _currentStatusText.SetActive(true);
        yield return new WaitForSeconds(duration);
        _currentStatusText.SetActive(false);
    }
    

    private void ShowStatusText(GameObject statusText)
    {
        HideStatusText();
        _currentStatusText = statusText;
        _currentStatusText.SetActive(true);
    }

    private void HideStatusText()
    {
        if (_currentStatusText!=null && _currentStatusText.activeSelf)
        {
            StopCoroutine("PromptStatusText");
            _currentStatusText.SetActive(false);
        }
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
}
