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
    public Toggle tgFilter;
    public TMP_Dropdown triangulation;
    public Toggle heightMap;
    public TMP_InputField neighbourField;
    public TMP_InputField distanceField;
    public TMP_InputField thresholdField;

    public Image panel;
    public Sprite sprite;

    public void changeBackground()
    {
        print("changeBackground in optionsButtons");
        panel.sprite = sprite;
        runButton();
    }
    public void runButton()
    {
        print("runButton in OptionsButtons");
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
        db.setTriangulationEnabled(tgFilter.isOn);

        if (int.TryParse(neighbourField.text,out int intResult))
        {
            db.setNumberOfNeighbours(intResult);
        } else
        {
            db.setNumberOfNeighbours(20);
        }

        if (float.TryParse(distanceField.text, out float neighbourDistance))
        {
            db.setNeighbourDistance(neighbourDistance);
        }
        else
        {
            db.setNeighbourDistance(1.5);
        }

        if(double.TryParse(thresholdField.text, out double outlierHeightThreshold))
        {
            db.setOutlierHeightThreshold(outlierHeightThreshold);
        }
        else
        {
            db.setOutlierHeightThreshold(1);
        }

        if (db.getTriangulationEnabled())
            db.setShowMesh(true);

        print("Saved settings!");

        controller.PointLoader();
        SceneManager.LoadScene(1);

    }

}
