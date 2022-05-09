using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsButtons : MonoBehaviour
{
    DataBase db = DataBase.getInstance();
    Controller controller = Controller.getInstance();

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

    public void runButton()
    {
        //Setting the values in the database to the values set in options
        db.setSliderValueShallowDepth((int)DepthSlider.Values.minValue);
        db.setSliderValueDeepDepth((int)DepthSlider.Values.maxValue);
        db.setSliderValueMinLength((int)LengthSlider.Values.minValue);
        db.setSliderValueMaxLength((int)LengthSlider.Values.maxValue);
        db.setSliderValueMinWidth((int)WidthSlider.Values.minValue);
        db.setSliderValueMaxWidth((int)WidthSlider.Values.maxValue);

        db.setOutlierHeightEnabled(odFilter.isOn);
        db.setNearestNeighbourEnabled(nnFilter.isOn);

        db.setTriangulationType(triangulation.value);

        if (int.TryParse(neighbourField.text,out int intResult))
        {
            db.setNumberOfNeighbours(intResult);
        }

        if (float.TryParse(distanceField.text, out float neighbourDistance))
        {
            db.setNeighbourDistance(neighbourDistance);
        }

        if(double.TryParse(thresholdField.text, out double outlierHeightThreshold))
        {
            db.setOutlierHeightThreshold(outlierHeightThreshold);
        }

        if (db.getTriangulationEnabled())
            db.setShowMesh(true);

        print("Saved settings!");
        controller.PointLoader();

    }

}
