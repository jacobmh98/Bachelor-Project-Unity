using UnityEngine;
using UnityEngine.UI;
using Min_Max_Slider;
using TMPro;

public class OptionsButtons : MonoBehaviour
{
    DataBase db = DataBase.getInstance();

    public MinMaxSlider DepthSlider;
    public MinMaxSlider LengthSlider;
    public MinMaxSlider WidthSlider;
    public Toggle nnFilter;
    public Toggle odFilter;
    public Toggle tgFilter;
    public TMP_Dropdown triangulation;
    public Toggle edgeTriangleRemoval;
    public TMP_InputField noOfNeighbourInputField;
    public TMP_InputField neighbourDistanceInputField;
    public TMP_InputField outlierHeightThresholdInputField;

    public Image panel;
    public Sprite sprite;

    public void changeBackground()
    {
        panel.sprite = sprite;
        runButton();
    }
    public void runButton()
    {
        resetPointCloudControllerVariables();
        setOptionsVariables();

        print("Saved settings!");
    }

    public void backButton()
    {
        db.setFromPoints(false);
    }

    public void resetOptions()
    {
        //Retting values for sliders
        DepthSlider.SetValues(db.getSliderLimitShallowDepth(), db.getSliderValueDeepDepth());
        LengthSlider.SetValues(db.getSliderLimitMinLength(), db.getSliderLimitMaxLength());
        WidthSlider.SetValues(db.getSliderLimitMinWidth(), db.getSliderLimitMaxWidth());

        //Setting toggles to un checked
        nnFilter.isOn = false;
        odFilter.isOn = false;
        tgFilter.isOn = false;
        triangulation.value = 1;
        edgeTriangleRemoval.isOn = false;

        //Retting values for textfields
        noOfNeighbourInputField.text = db.getDefaultNumberOfNeighbours().ToString();
        neighbourDistanceInputField.text = db.getDefaultNeighbourDistance().ToString();
        outlierHeightThresholdInputField.text = db.getDefaultOutlierHeightThreshold().ToString();

    }

    public void setOptionsVariables()
    {
        //Setting values in the database to the values chosen in the sliders
        db.setSliderValueShallowDepth((int)DepthSlider.Values.minValue);
        db.setSliderValueDeepDepth((int)DepthSlider.Values.maxValue);
        db.setSliderValueMinLength((int)LengthSlider.Values.minValue);
        db.setSliderValueMaxLength((int)LengthSlider.Values.maxValue);
        db.setSliderValueMinWidth((int)WidthSlider.Values.minValue);
        db.setSliderValueMaxWidth((int)WidthSlider.Values.maxValue);

        //Setting values in the database to the values picked in the checkboxes
        db.setOutlierHeightEnabled(odFilter.isOn);
        db.setNearestNeighbourEnabled(nnFilter.isOn);
        db.setTriangulationEnabled(tgFilter.isOn);
        db.setTriangulationType(triangulation.value);
        db.setEdgeTrianglesRemoved(edgeTriangleRemoval.isOn);

        //Setting values in the database to the values in written in the textfields
        // if the values written is not valid, the values is set to the default ones.
        if (int.TryParse(noOfNeighbourInputField.text, out int intResult))
        {
            db.setNumberOfNeighbours(intResult);
        }
        else
        {
            db.setNumberOfNeighbours(db.getDefaultNumberOfNeighbours());
        }

        if (float.TryParse(neighbourDistanceInputField.text, out float neighbourDistance))
        {
            db.setNeighbourDistance(neighbourDistance);
        }
        else
        {
            db.setNeighbourDistance(db.getDefaultNeighbourDistance());
        }

        if (double.TryParse(outlierHeightThresholdInputField.text, out double outlierHeightThreshold))
        {
            db.setOutlierHeightThreshold(outlierHeightThreshold);
        }
        else
        {
            db.setOutlierHeightThreshold(db.getDefaultOutlierHeightThreshold());
        }

        if (db.getTriangulationEnabled())
            db.setShowMesh(true);

    }

    // Resetting the variables in the controller box when running the point cloud
    // to avoid all values when running new pointcloud
    public void resetPointCloudControllerVariables()
    {
        db.setShowMesh(false);
        db.setHeightMapEnabled(false);
        db.setShowHeightMap(false);
        db.setShowPointCloud(true);
        db.setParticleSize(0.05f);
        db.setUpdateHeightMap(false);
        db.setUpdateOceanFloor(false);
        db.setUpdatePointCloud(false);
        db.setUpdatePointSize(false);
        db.setPointCloudGradient(false);
        db.setUpdatePointColor(false);
    }

}
