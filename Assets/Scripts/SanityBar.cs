using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SanityBar : MonoBehaviour
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

        float current = PlayerState.Instance.currentSanity;
        float max     = PlayerState.Instance.maxSanity;

        slider.value = current / max;

        if (valueText != null)
            valueText.text = Mathf.RoundToInt(current).ToString();
    }
}
