using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayController : MonoBehaviour, Observer 
{
    // Observed subject
    [SerializeField] private SeriesController _series;
    [SerializeField] private BallController _ball;

    [SerializeField] private GameObject _scoreDisplay; // parent object
    [SerializeField] private GameObject[] _ticks;
    [SerializeField] private GameObject[] _crosses;
    [SerializeField] private TMP_Text[] _shotScoreTexts;
    [SerializeField] private GameObject[] _scoreLines;
    [SerializeField] private TMP_Text _scoreTextTotal;
    [SerializeField] private GameObject _totalScoreLine;

    private int _shotIndex;
    private int _totalScore;

    private int[] _scores = new int[]{0,0,0};

    private void Awake()
    {
        if (_series!=null)
            _series.RegisterObserver(this);
        if (_ball!=null)
            _ball.RegisterObserver(this);
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan || 
            notificationType == NotificationType.SeriesPlay)
        {
            _scoreDisplay.SetActive(false);
            ResetDisplay();
        }        

        if (notificationType == NotificationType.SeriesDone)
        {
            _scoreDisplay.SetActive(true);
            StartCoroutine(DisplayScore());
        }

        if (notificationType == NotificationType.BallShot)
            _shotIndex = (int)value % _scores.Length;
        
        if (notificationType == NotificationType.ShotScore)
        {
            _scores[_shotIndex] = (int)value;
            _totalScore += (int)value;
        }   
    }

    private void ResetDisplay() 
    {
        for (int i=0; i < _scores.Length; i++)
        {
            _scores[i] = 0;
            _ticks[i].SetActive(false);
            _crosses[i].SetActive(false);
            _scoreLines[i].SetActive(false);
        }

        _totalScore = 0;
        _totalScoreLine.SetActive(false);
    }

    private void UpdateScoreTexts()
    {
        for (int i=0; i < _scores.Length; i++)
        {
            _shotScoreTexts[i].text = _scores[i].ToString();
        }
        
        _scoreTextTotal.text = _totalScore.ToString();
    }

    private void UpdatePictogramPanel()
    {
        for (int i=0; i < _scores.Length; i++)
        {
            if (_scores[i] > 0)
                _ticks[i].SetActive(true);
            else
                _crosses[i].SetActive(true);
        }
    }

    private IEnumerator DisplayScore()
    {
        UpdateScoreTexts();
        UpdatePictogramPanel();

        foreach (GameObject line in _scoreLines)
        {
            line.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        _totalScoreLine.SetActive(true);
    }
}
