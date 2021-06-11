using UnityEngine;

public class BallController : Subject, Observer
{
    // Observed subject
    [SerializeField] private SeriesController _series;

    [SerializeField] private Transform _penaltySpot;
    [SerializeField] private Transform _baseLineCenter;
    [SerializeField] private float _maxPower = 10f;
    [SerializeField] private float _actualPower;
    [SerializeField] private float _speedFactor = 1.5f;

    private Rigidbody _rb;
    private float _timeCorrection;
    private int _ballShotIndex = 0;
    private float _shotDistance;
    private bool _isReadyToKick;

    void Awake() 
    {
        if (_series!=null)
            _series.RegisterObserver(this);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isReadyToKick = false;
    }

    public void OnNotify(object value, NotificationType notificationType)
    {
        if (notificationType == NotificationType.SeriesScan)
        {
            _isReadyToKick = false;
            HiddenPosition();

        }
        if (notificationType == NotificationType.SeriesPlay)
        {
            _isReadyToKick = true;
            ResetPosition();

        }
        if (notificationType == NotificationType.SeriesDone)
        {
            _isReadyToKick = false;
            FreezePosition();
        }
    }

    private void OnMouseDown()
    {
        if (_isReadyToKick)
            _timeCorrection = Time.time;
    }

    private void OnMouseDrag()
    {
        if (_isReadyToKick)
            _actualPower = Mathf.Abs(Mathf.Sin((Time.time - _timeCorrection) * _speedFactor)) * _maxPower;
    }

    private void OnMouseUp()
    {
        if (_isReadyToKick)
        {
            _rb.useGravity = true;
            _rb.AddForce((_penaltySpot.forward + (_penaltySpot.up * 0.33f)) * _actualPower, ForceMode.Impulse);
            _rb.AddTorque(Random.insideUnitSphere * _actualPower);

            Notify(_ballShotIndex, NotificationType.BallShot);
            _ballShotIndex++;

            _shotDistance = Vector3.Distance(_penaltySpot.position, _baseLineCenter.position);
            Notify(_shotDistance, NotificationType.DistanceChange);
        }
    }

    public void ResetPosition()
    {
        FreezePosition();
        transform.position = _penaltySpot.position;
        _actualPower = 0f;
    }

    private void FreezePosition()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void HiddenPosition()
    {
        transform.position = -_penaltySpot.forward * 2f;
    }

    // Getters
    public float GetMaxPower(){ return _maxPower; }
    public float GetActualPower(){ return _actualPower; }
    //public void SetActualPower(float power){ _actualPower = power;}
}
