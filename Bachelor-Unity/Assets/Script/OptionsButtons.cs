using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;

public class OptionsButtons : MonoBehaviour
{
    Controller cont = Controller.getInstance();
    public MinMaxSlider DepthSlider;
    public MinMaxSlider LengthSlider;
    public MinMaxSlider WidthSlider;
    public Toggle nnFilter;
    public Toggle odFilter;
    public Dropdown triangulation;
    public Toggle heightMap;

    public void saveButton()
    {
        cont.setMaxDepth((int) DepthSlider.Values.minValue);
        cont.setMinDepth((int) DepthSlider.Values.maxValue);

    }



}
