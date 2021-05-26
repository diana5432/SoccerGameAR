using UnityEngine;

public class KickOff : MonoBehaviour
{
    public Vector3 kickDirection;
    public float maxPower;
    public float actualPower;

    private Rigidbody rb;
    private float timeCorrection;
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            timeCorrection = Time.time;
        }
        if (Input.GetButton("Jump"))
        {
            actualPower = Mathf.Abs(Mathf.Sin(Time.time - timeCorrection)) * maxPower;
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.AddForce(kickDirection * actualPower, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * actualPower);
        }
        // for debugging
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }
}
