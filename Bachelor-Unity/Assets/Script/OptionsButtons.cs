using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsButtons : MonoBehaviour
{
    Controller cont = Controller.getInstance();
    public MinMaxSlider DepthSlider;
    public MinMaxSlider LengthSlider;
    public MinMaxSlider WidthSlider;
    public Toggle nnFilter;
    public Toggle odFilter;
    public TMP_Dropdown triangulation;
    public Toggle heightMap;
    public TMP_InputField neighbourField;
    public TMP_InputField distanceField;
    public TMP_InputField thresholdField;


    public void saveButton()
    {
        cont.setMaxDepth((int) DepthSlider.Values.minValue);
        cont.setMinDepth((int) DepthSlider.Values.maxValue);

        cont.setMinLengthAxis((int) LengthSlider.Values.minLimit);
        cont.setMaxLengthAxis((int) LengthSlider.Values.maxLimit);

        cont.setMinWidthAxis((int) WidthSlider.Values.minLimit);
        cont.setMaxWidthAxis((int) WidthSlider.Values.maxLimit);

        cont.triangulation = triangulation.value;

        if(int.TryParse(neighbourField.text,out int intResult))
        {
            cont.n_neighbours = intResult;
        }

        print("Neighbours: " + intResult);

        if (float.TryParse(distanceField.text, out float floatResult))
        {
            cont.neighbour_distance = floatResult;
        }
        print("Distance: " + floatResult);

        if(double.TryParse(thresholdField.text, out double doubleResult))
        {
            //something like:
            //cont.threshold_value = doubleResult;
        }

        print("Saved settings!");
    }

    public void runButton()
    {
        cont.PointLoader();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
