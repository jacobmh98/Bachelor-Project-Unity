using DelaunatorSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Accord.Collections;
using Accord.MachineLearning;

public class Controller
{
    public static Controller controller = new Controller();
    Sonar sonarData;
    Ping pingData;

    // List variables for point coordinates
    private List<Vector3> points = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector3> boatPathPoints = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();

    Triangulate t = null;
    public Mesh mesh = null;

    private int minDepth = 0;
    private int maxDepth = 100;
    private int minLengthAxis = 0;
    private int maxLengthAxis = 100;
    private int minWidthAxis = 0;
    private int maxWidthAxis = 100;

    string fileName;

    public int n_neighbours;
    public double neighbourDistance;

    public double Z_scoreThreshold;

    public bool nearestNeighbourEnabled = false;
    public bool outlierHeightEnabled = false;
    public bool heightMap = false;
    public bool showHeightmap = false;
    public bool showPointCloud = true;
    public bool showMesh = false;
    public bool triangulate = false;
    public int triangulation;
    public bool generateHeightmap;
    public bool generateMesh;

    public GameObject toggleGroup;

    private Controller()
    {

        if (String.IsNullOrEmpty(fileName))
        {
            //fileName = @"C:\Users\kanne\Desktop\7k_data_extracted_rotated.json";
            fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\bachelor_project_teledyne\7k_data_extracted_rotated.json";
        }
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        // setting temporary min and max height in pointcloud
        minDepth = sonarData.minimum_depth;
        maxDepth = sonarData.maximum_depth;
        minLengthAxis = sonarData.min_length_axis;
        maxLengthAxis = sonarData.max_length_axis;
        minWidthAxis = sonarData.min_width_axis;
        maxWidthAxis = sonarData.max_width_axis;

        // tmp line calls remove this remove dis later bitches
        
        PointLoader();
        triangulate = true;
        showHeightmap = true;
    }

    public void Triangulate()
    {
        if(t == null)
            t = new Triangulate(points, pointsDelaunay);
        triangles = t.getTriangles();
    }


    public void PointLoader()
    {
        List<float> heightOutlierDetectionList = new List<float>();
        List<double[]> kDTreeSetupList = new List<double[]>();
        List<Vector3> new_points = new List<Vector3>();
        Vector3 point;
        Vector3 boatPoint;

        //Test values, needs to get the values from the slider here
        minDepth = minDepth;
        maxDepth = maxDepth;
        minLengthAxis = minLengthAxis;
        maxLengthAxis = maxLengthAxis;
        minWidthAxis = minWidthAxis;
        maxWidthAxis = maxWidthAxis;

        outlierHeightEnabled = false;
        nearestNeighbourEnabled = false;

        for (int i = 0; i < sonarData.no_pings; i++)
        {

            //Not quite working
            //boatPoint = new Vector3((float)pingData.ping_boat_coord[0], (float)pingData.ping_boat_coord[2], (float)pingData.ping_boat_coord[1]);
            //boatPathPoints.Add(boatPoint);

            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                if ((point[1] < minDepth && point[1] > maxDepth)
                    && (point[0] > minLengthAxis && point[0] < maxLengthAxis)
                    && (point[2] > minWidthAxis && point[2] < maxWidthAxis))
                {
                    // adding point to pointcloud
                    points.Add(point);
                    pointsDelaunay.Add(new DelaunatorSharp.Point(point[0], point[2]));

                    // Adding point to other lists for outlier removal functions to safe running over all points multiple times
                    heightOutlierDetectionList.Add(point[1]);
                    double[] toKDTreePoint = new double[] { point[0], point[1], point[2] };
                    kDTreeSetupList.Add(toKDTreePoint);
                }

            }

        }

        if (outlierHeightEnabled)
        {
            Z_scoreThreshold = 1;
            float sumMean = 0;
            double sumStd = 0;
            float mean = 0;
            double standardDeviation = 0;
            int n = heightOutlierDetectionList.Count;

            //Summin over all height values to calculate the mean height
            for (int i = 0; i < n; i++)
            {
                sumMean += heightOutlierDetectionList[i];
            }

            //If there exists more than 1 element in the height list, then the mean and standard deviation can be calculated
            if (n > 0)
            {
                mean = sumMean / n;

                for (int i = 0; i < n; i++)
                {
                    sumStd += Math.Pow(Math.Abs(heightOutlierDetectionList[i] - mean), 2);
                }

                standardDeviation = sumStd / n;
            }

            //Checking all points in the height list
            for (int i = 0; i < n; i++)
            {

                //Points with a z score higher than the defined threshold will not be added to the new point list
                if (Math.Abs((heightOutlierDetectionList[i] - mean) / standardDeviation) < Z_scoreThreshold)
                {
                    new_points.Add(points[i]);
                }

            }

            //The pointloader will set the points as the new found points that satisfy the z score threshold
            points = new_points;
        }

        if (nearestNeighbourEnabled)
        {
            n_neighbours = 30; //Get value from options
            neighbourDistance = 0.5; //Get value from options
            new_points = new List<Vector3>();

            double[][] kDTreeSetupArray = kDTreeSetupList.ToArray();

            KDTree<int> kDTree = KDTree.FromData<int>(kDTreeSetupArray);

            for (int i = 0; i < points.Count; i++)
            {

                double[] currPoint = new double[] { points[i].x, points[i].y, points[i].z };
                List<NodeDistance<KDTreeNode<int>>> neighbours = kDTree.Nearest(currPoint, radius: neighbourDistance);

                if (neighbours.Count > n_neighbours)
                {
                    new_points.Add(points[i]);
                }

            }

            points = new_points;
        }

    }

    public static Controller getInstance()
    {
        return controller;
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
        if (String.IsNullOrEmpty(fileName))
        {
            Debug.Log("fileName is null or empty!");
            fileName = @"C:/Users/Max/Desktop/7k_data_extracted.json";

        }
        else
        {
            fileName = newPath;
        }
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
    public bool getNearestNeighbour()
    {
        return nearestNeighbourEnabled;
    }

    public bool getOutlierHeight()
    {
        return outlierHeightEnabled;
    }

    public bool getHeightMap()
    {
        return heightMap;
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

    public void setMinLengthAxis(int minLength)
    {
        minLengthAxis = minLength;
    }

    public void setMaxLengthAxis(int maxLength)
    {
        maxLengthAxis = maxLength;
    }

    public void setMinWidthAxis(int minWidth)
    {
        minWidthAxis = minWidth;
    }

    public void setMaxWidthAxis(int maxWidth)
    {
        maxWidthAxis = maxWidth;
    }
    
    public int getTriangulationType()
    {
        return triangulation;
    }

    public void setTriangulationType(int newTriang)
    {
        triangulation = newTriang;
    }


    public List<int> getTriangles()
    {
        return triangles;
    }

}
