using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase
{
    public static DataBase db = new DataBase();
    Sonar sonarData;
    Ping pingData;


    //Setting initial variables 
    private int minDepth = 0;
    private int maxDepth = 100;
    private int minLengthAxis = 0;
    private int maxLengthAxis = 100;
    private int minWidthAxis = 0;
    private int maxWidthAxis = 100;

    private int numberOfNeighbours = 20;
    private double neighbourDistance = 1.5;
    private double outlierHeigthThreshold = 1.0;

    private bool nearestNeighboursEnabled = false;
    private bool outlierHeightEnabled = false;

    private bool showMesh = false;
    private bool triangulationEnabled = false;
    private int triangulationType = 0;

    private bool heightMapEnabled = false;
    private bool showHeightMap = false;
    private bool showPointCloud = true;

    // Variables set if game object should update
    private bool updateHeightMap = false;
    private bool updateOceanFloor = false;
    private bool updatePointCloud = false;
    private bool updatePointSize = false;

    private float particleSize = 0.05f;

    private DataBase() {}
    public static DataBase getInstance()
    {
        return db;
    }
    //Set variables methods
    public void setMinDepth(int newMinDepth)
    {
        minDepth = newMinDepth;
    }
    public void setMaxDepth(int newMaxDepth)
    {
        maxDepth = newMaxDepth;
    }
    public void setMinLengthAxis(int newMinLength)
    {
        minLengthAxis = newMinLength;
    }
    public void setMaxLengthAxis(int newMaxLength)
    {
        maxLengthAxis = newMaxLength;
    }
    public void setMinWidthAxis(int newMinWidth)
    {
        minWidthAxis = newMinWidth;
    }
    public void setMaxWidthAxis(int newMaxWidth)
    {
        maxWidthAxis = newMaxWidth;
    }
    public void setNearestNeighbourEnabled(bool newNearestNeighbour)
    {
        nearestNeighboursEnabled = newNearestNeighbour;
    }
    public void setOutlierHeightEnabled(bool newOutlierHeigth)
    {
        outlierHeightEnabled = newOutlierHeigth;
    }
    public void setNumberOfNeighbours(int newNoOfNeighbours)
    {
        numberOfNeighbours = newNoOfNeighbours;
    }
    public void setNeighbourDistance(double newNeighbourDist)
    {
        neighbourDistance = newNeighbourDist;
    }
    public void setOutlierHeightThreshold(double newOutlierThreshold)
    {
        outlierHeigthThreshold = newOutlierThreshold;
    }
    public void setShowMesh(bool newShowMesh)
    {
        showMesh = newShowMesh;
    }
    public void setTriangulationEnabled(bool newTriangulation)
    {
        triangulationEnabled = newTriangulation;
    }
    public void setTriangulationType(int newTriangulationType)
    {
        triangulationType = newTriangulationType;
    }
    public void setHeightMapEnabled(bool newHeightMapEnabled)
    {
        heightMapEnabled = newHeightMapEnabled;
    }
    public void setShowHeightMap(bool newShowHeightMap)
    {
        showHeightMap = newShowHeightMap;
    }
    public void setShowPointCloud(bool newShowPointCloud)
    {
        showPointCloud = newShowPointCloud;
    }
    public void setUpdateHeightMap(bool newUpdateHeightMap)
    {
        updateHeightMap = newUpdateHeightMap;
    }
    public void setUpdateOceanFloor(bool newUpdateOceanFloor)
    {
        updateOceanFloor = newUpdateOceanFloor;
    }
    public void setUpdatePointCloud(bool newUpdatePointCloud)
    {
        updatePointCloud = newUpdatePointCloud;
    }
    public void setUpdatePointSize(bool newUpdatePointSize)
    {
        updatePointSize = newUpdatePointSize;
    }

    public void setParticleSize(float newParticleSize)
    {
        particleSize = newParticleSize;
    }


    //Get variables methods
    public int getMinDepth()
    {
        return minDepth;
    }
    public int getMaxDepth()
    {
        return maxDepth;
    }
    public int getMinLengthAxis()
    {
        return minLengthAxis;
    }
    public int getMaxLengthAxis()
    {
        return maxLengthAxis;
    }
    public int getMinWidthAxis()
    {
        return minWidthAxis;
    }
    public int getMaxWidthAxis()
    {
        return maxWidthAxis;
    }
    public bool getNearestNeighbourEnabled()
    {
        return nearestNeighboursEnabled;
    }
    public bool getOutlierHeightEnabled()
    {
        return outlierHeightEnabled;
    }
    public int getNumberOfNeighbours()
    {
        return numberOfNeighbours;
    }
    public double getNeighbourDistance()
    {
        return neighbourDistance;
    }
    public double getOutlierHeightThreshold()
    {
        return outlierHeigthThreshold;
    }
    public bool getShowMesh()
    {
        return showMesh;
    }
    public bool getTriangulationEnabled()
    {
        return triangulationEnabled;
    }
    public int getTriangulationType()
    {
        return triangulationType;
    }
    public bool getHeightMapEnabled()
    {
        return heightMapEnabled;
    }
    public bool getShowHeightMap()
    {
        return showHeightMap;
    }
    public bool getShowPointCloud()
    {
        return showPointCloud;
    }

    public bool getUpdateHeightMap()
    {
        return updateHeightMap;
    }
    public bool getUpdateOceanFloor()
    {
        return updateOceanFloor;
    }
    public bool getUpdatePointCloud()
    {
        return updatePointCloud;
    }
    public bool getUpdatePointSize()
    {
        return updatePointSize;
    }

    public float getParticleSize()
    {
        return particleSize;
    }

}
