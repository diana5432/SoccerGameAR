using UnityEngine;

public class GoalController : Subject
{
    
    [SerializeField] private Transform _baseLineCenter;
    [SerializeField] private Transform _goalPanel;

    private float _scaleCorrectionFactor;

    private void Start() 
    {
        _scaleCorrectionFactor = 1f / _goalPanel.localScale.x;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 hitPosition = other.ClosestPoint(_baseLineCenter.position);
            float _hitDistanceFromCenter = Vector3.Distance(hitPosition, _baseLineCenter.position);
            _hitDistanceFromCenter *= _scaleCorrectionFactor;
            Notify(_hitDistanceFromCenter, NotificationType.GoalHit);
            Debug.Log(_hitDistanceFromCenter);
        }
    }

}
