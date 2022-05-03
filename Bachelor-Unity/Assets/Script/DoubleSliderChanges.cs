using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;

public class DoubleSliderChanges : MonoBehaviour
{
    Controller cont = Controller.getInstance();
    public MinMaxSlider depthSlider;
    public MinMaxSlider lengthSlider;
    public MinMaxSlider widthSlider;

    // Start is called before the first frame update
    void Start()
    {
        depthSlider.SetLimits(cont.getMaxDepth(), cont.getMinDepth());
        depthSlider.SetValues(cont.getMaxDepth(), cont.getMinDepth());
        lengthSlider.SetLimits(cont.getMinLengthAxis(), cont.getMaxLengthAxis());
        lengthSlider.SetValues(cont.getMinLengthAxis(), cont.getMaxLengthAxis());
        widthSlider.SetLimits(cont.getMinWidthAxis(), cont.getMaxWidthAxis());
        widthSlider.SetValues(cont.getMinWidthAxis(), cont.getMaxWidthAxis());
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
