using UnityEngine;

public class BallController : Subject
{
    [SerializeField] private Transform _penaltySpot;
    [SerializeField] private float _maxPower = 10f;
    [SerializeField] private float _actualPower;
    [SerializeField] private float _speedFactor = 1.5f;

    private Rigidbody _rb;
    private float _timeCorrection;
    private int _ballShotIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _timeCorrection = Time.time;
    }

    private void OnMouseDrag()
    {
        _actualPower = Mathf.Abs(Mathf.Sin((Time.time - _timeCorrection) * _speedFactor)) * _maxPower;
    }

    private void OnMouseUp()
    {
        _rb.useGravity = true;
        _rb.AddForce((_penaltySpot.forward + (_penaltySpot.up * 0.33f)) * _actualPower, ForceMode.Impulse);
        _rb.AddTorque(Random.insideUnitSphere * _actualPower);
        Notify(_ballShotIndex, NotificationType.BallShot);
        _ballShotIndex++;
    }

    public void ResetPosition()
    {
        FreezePosition();
        transform.position = _penaltySpot.position;
        transform.rotation = Quaternion.Euler(Random.insideUnitSphere * 180f);
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
    public void SetActualPower(float power){ _actualPower = power;}
}
