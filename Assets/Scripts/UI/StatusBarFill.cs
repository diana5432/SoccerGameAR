using UnityEngine;
using UnityEngine.UI;

public class StatusBarFill : MonoBehaviour
{
    [SerializeField] private KickOff kickOff;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = kickOff.GetActualPower() / kickOff.GetMaxPower();
    }
}
