using UnityEngine;
using UnityEngine.UI;

public class StatusBarFill : MonoBehaviour
{
    public KickOff kickOff;
    public Image fillImage;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (slider.value < slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
       */
        slider.value = kickOff.actualPower / kickOff.maxPower;
    }
}
