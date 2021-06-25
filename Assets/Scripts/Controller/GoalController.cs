using UnityEngine;

public class GoalController : Subject
{
    
    [SerializeField] private Transform _baseLineCenter;
    [SerializeField] private Transform _goalPanel;
    [SerializeField] private BallController _ball;


    private float _scaleCorrectionFactor;
    private int _lastBallHitIndex;

    private void Start() 
    {
        _scaleCorrectionFactor = 1f / _goalPanel.localScale.x;
        _lastBallHitIndex = -1;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") &&
            _ball.GetBallShotIndex() != _lastBallHitIndex)
        {
            _lastBallHitIndex = _ball.GetBallShotIndex();
            Vector3 hitPosition = other.ClosestPoint(_baseLineCenter.position);
            float _hitDistanceFromCenter = Vector3.Distance(hitPosition, _baseLineCenter.position);
            _hitDistanceFromCenter *= _scaleCorrectionFactor;
            Notify(_hitDistanceFromCenter, NotificationType.GoalHit);
        }
    }
}
