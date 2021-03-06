using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPanelController : MonoBehaviour, Observer
{
    // Observed subject
    [SerializeField] private SeriesController _series;
    

    private void Awake() {
        if (_series!=null)
            _series.RegisterObserver(this);        
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
        {
            Deactivate();
        }
    }

    public void SpawnAtPosition(Vector3 position)
    {
        if (_series.GetPhase() == (int) SeriesPhase.SCAN)
            transform.position = position;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
 }
