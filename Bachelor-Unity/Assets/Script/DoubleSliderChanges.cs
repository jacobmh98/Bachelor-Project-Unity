using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;

public class DoubleSliderChanges : MonoBehaviour
{
    Controller cont = Controller.getInstance();
    DataBase db = DataBase.getInstance();

    public MinMaxSlider depthSlider;
    public MinMaxSlider lengthSlider;
    public MinMaxSlider widthSlider;

    // Start is called before the first frame update
    void Start()
    {
        depthSlider.SetLimits(db.getMaxDepth(), db.getMinDepth());
        depthSlider.SetValues(db.getMaxDepth(), db.getMinDepth());
        lengthSlider.SetLimits(db.getMinLengthAxis(), db.getMaxLengthAxis());
        lengthSlider.SetValues(db.getMinLengthAxis(), db.getMaxLengthAxis());
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
