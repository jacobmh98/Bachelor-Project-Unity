using DelaunatorSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class Controller
{
    public static Controller controller = new Controller();

    // List variables for point coordinates
    private List<Vector3> points = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();

    private int minDepth;
    private int maxDepth;

    string fileName;
    Sonar sonarData;

    public bool depthFilter = false;
    public bool axisFilter = false;
    public bool nearestNeighbour = false;
    public bool outlierHeight = false;

    public bool heightMap = true;

    public bool triangulate = false;

    public GameObject toggleGroup;

    private Controller()
    {

        if (String.IsNullOrEmpty(fileName))
        {
            //fileName = @"C:\Users\Max\Desktop\7k_data_extracted_rotated.json";
            fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\bachelor_project_teledyne\7k_data_extracted_rotated.json";
        }
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);
        
        // Setting temporary min and max height in pointcloud
        minDepth = sonarData.minimum_depth;
        maxDepth = sonarData.maximum_depth;

        // tmp call to PointLoader remove dis later bitches
        minDepth = -12;
        maxDepth = -21;
        triangulate = true;
        PointLoader();

        
    }
    public void PointLoader()
    {
        for (int i = 0; i < sonarData.no_pings; i++)
        {

            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                Vector3 point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                if (point[1] < minDepth && point[1] > maxDepth)
                {
                    // adding point to pointcloud
                    points.Add(point);
                    pointsDelaunay.Add(new DelaunatorSharp.Point(point[0], point[2]));

                }
               
            }
        }
    }

    public static Controller getInstance()
    {
        return controller;
    }

    public int getMinDepth()
    {
        return minDepth;
    }

    public int getMaxDepth()
    {
        return maxDepth;
    }

    public void setMinDepth(int newMin)
    {
        minDepth = newMin;
    }

    public void setMaxDepth(int newMax)
    {
        maxDepth = newMax;
    }

    public List<Vector3> getPoints()
    {
        return points;
    }

    public List<IPoint> getPointsDelaunay()
    {
        return pointsDelaunay;
    }

    public void setPath(string newPath)
    {
        fileName = newPath;
    }

    public bool getDepthFilter()
    {
        return depthFilter;
    }

    public bool getAxisFilter()
    {
        return axisFilter;
    }

    public bool getNearestNeighbour()
    {
        return nearestNeighbour;
    }

    public bool getOutlierHeight()
    {
        return outlierHeight;
    }

    public bool getHeightMap()
    {
        return heightMap;
    }
}