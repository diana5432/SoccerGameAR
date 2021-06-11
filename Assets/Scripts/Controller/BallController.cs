using UnityEngine;

public class BallController : Subject, Observer
{
    // Observed subject
    [SerializeField] private SeriesController _series;

    [SerializeField] private Transform _penaltySpot;
    [SerializeField] private float _maxPower = 10f;
    [SerializeField] private float _actualPower;
    [SerializeField] private float _speedFactor = 1.5f;

    private Rigidbody _rb;
    private float _timeCorrection;
    private int _ballShotIndex = 0;
    private bool _isReadyToKick;

    void Awake() {
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
        if (notificationType == NotificationType.SeriesPlay)
        {
            _isReadyToKick = true;
        }
        if (notificationType == NotificationType.SeriesDone)
        {
            _isReadyToKick = false;
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
        }
    }

    public void ResetPosition()
    {
        FreezePosition();
        transform.position = _penaltySpot.position;
        //transform.rotation = Quaternion.Euler(Random.insideUnitSphere * 180f);
        _actualPower = 0f;
    }

    public void FreezePosition()
    {
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    // Getters
    public float GetMaxPower(){ return _maxPower; }
    public float GetActualPower(){ return _actualPower; }
    //public void SetActualPower(float power){ _actualPower = power;}
}
