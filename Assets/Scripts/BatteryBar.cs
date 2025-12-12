using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;  

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    private void Update()
    {
        if (PlayerState.Instance == null)
            return;

        float current = PlayerState.Instance.currentBattery;
        float max     = PlayerState.Instance.maxBattery;

        slider.value = current / max;

        if (valueText != null)
        {
            float percent = (current / max) * 100f;
            valueText.text = Mathf.RoundToInt(percent) + "%";
        }
    }
}

