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

    // Start is called before the first frame update
    void Start()
    {
        depthSlider.SetLimits(db.getSliderShallowDepth(), db.getSliderDeepDepth());
        depthSlider.SetValues(db.getSliderShallowDepth(), db.getSliderDeepDepth());
        lengthSlider.SetLimits(db.getSliderMinLengthAxis(), db.getSliderMaxLengthAxis());
        lengthSlider.SetValues(db.getSliderMinLengthAxis(), db.getSliderMaxLengthAxis());
        widthSlider.SetLimits(db.getMinWidthAxis(), db.getMaxWidthAxis());
        widthSlider.SetValues(db.getMinWidthAxis(), db.getMaxWidthAxis());
    }

    public void depthSliderChange()
    {
        print(depthSlider.Values);
    }

    public void lengthSliderChange()
    {
        print(lengthSlider.Values);
    }

    public void widthSliderChange()
    {
        print(widthSlider.Values);
    }
}
