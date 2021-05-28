using UnityEngine;

public class KickOff : MonoBehaviour
{
    [SerializeField] private float maxPower;
    [SerializeField] private float actualPower;
    [SerializeField] private float speed = 1f;

    private Rigidbody rb;
    private float timeCorrection;
    private Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
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
            actualPower = Mathf.Abs(Mathf.Sin((Time.time - timeCorrection) * speed)) * maxPower;
        }
        if (Input.GetButtonUp("Jump"))
        {
            startPos = transform.position; // for debugging
            rb.useGravity = true;
            rb.AddForce((transform.forward + (transform.up * 0.33f)) * actualPower, ForceMode.Impulse); // schieße in Richtung Forward mit voller Power 
                                                                                                        // und in Richtung Up mit Drittel-Power
            rb.AddTorque(Random.insideUnitSphere * actualPower);
        }
        // for debugging
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.useGravity = false;
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Getters
    public float GetMaxPower(){ return maxPower; }
    public float GetActualPower(){ return actualPower; }
}
