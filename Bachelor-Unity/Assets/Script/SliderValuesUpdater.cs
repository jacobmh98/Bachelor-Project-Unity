using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;

public class SliderValuesUpdater : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public MinMaxSlider depthSlider;
    public MinMaxSlider lengthSlider;
    public MinMaxSlider widthSlider;

    public void updateSliderValues()
    {
        depthSlider.SetLimits(db.getSliderLimitShallowDepth(), db.getSliderLimitDeepDepth());
        depthSlider.SetValues(db.getSliderValueShallowDepth(), db.getSliderValueDeepDepth());
        lengthSlider.SetLimits(db.getSliderLimitMinLength(), db.getSliderLimitMaxLength());
        lengthSlider.SetValues(db.getSliderValueMinLength(), db.getSliderValueMaxLength());
        widthSlider.SetLimits(db.getSliderLimitMinWidth(), db.getSliderLimitMaxWidth());
        widthSlider.SetValues(db.getSliderValueMinWidth(), db.getSliderValueMaxWidth());

    }

}
