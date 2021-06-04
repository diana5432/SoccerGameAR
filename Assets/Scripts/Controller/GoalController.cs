using UnityEngine;

public class GoalController : Subject
{
    [SerializeField] private Transform _baseLineCenter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 hitPosition = other.ClosestPoint(_baseLineCenter.position);
            float _hitDistanceFromCenter = Vector3.Distance(hitPosition, _baseLineCenter.position);
            Notify(_hitDistanceFromCenter, NotificationType.GoalHit);
        }
    }
}
