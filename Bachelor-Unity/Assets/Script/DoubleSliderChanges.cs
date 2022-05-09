using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;

public class DoubleSliderChanges : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public MinMaxSlider depthSlider;
    public MinMaxSlider lengthSlider;
    public MinMaxSlider widthSlider;

    void Start()
    {
        print("Slider start");

        depthSlider.SetLimits(db.getSliderShallowDepth(), db.getSliderDeepDepth());
        depthSlider.SetValues(db.getSliderShallowDepth(), db.getSliderDeepDepth());
        lengthSlider.SetLimits(db.getSliderMinLengthAxis(), db.getSliderMaxLengthAxis());
        lengthSlider.SetValues(db.getSliderMinLengthAxis(), db.getSliderMaxLengthAxis());
        widthSlider.SetLimits(db.getMinWidthAxis(), db.getMaxWidthAxis());
        widthSlider.SetValues(db.getMinWidthAxis(), db.getMaxWidthAxis());

    }

}
