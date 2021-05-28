using UnityEngine;
using UnityEngine.UI;

public class StatusBarFill : MonoBehaviour
{
    [SerializeField] private BallController _ball;

    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _ball.GetActualPower() / _ball.GetMaxPower();
    }
}
