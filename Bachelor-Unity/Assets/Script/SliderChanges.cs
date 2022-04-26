using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderChanges : MonoBehaviour
{
    public Slider sliderUI;
    private TMP_Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<TMP_Text>();
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        string sliderMessage = "" + sliderUI.value;
        textSliderValue.text = sliderMessage;
    }
}