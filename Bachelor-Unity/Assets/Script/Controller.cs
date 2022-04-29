using DelaunatorSharp;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Controller
{
    public static Controller controller = new Controller();
    private List<Vector3> points = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();

    private int minDepth = 0;
    private int maxDepth = 100;
    private int minLengthAxis = 0;
    private int maxLengthAxis = 100;
    private int minWidthAxis = 0;
    private int maxWidthAxis = 100;

    private bool depth_filter_passed;
    private bool axis_filter_passed;

    public bool nearestNeighbour = false;
    public bool z_score_outlier_detection = false;

    public int n_neighbours;
    public float neighbour_distance;

    public double z_score_threshold;


    string fileName;
    Sonar sonarData;

    public bool heightMap = true;

    public bool generateHeightmap = false;
    public bool generateMesh = false;
    public bool triangulate = false;

    private Controller()
    {
        string fileName = @"C:\Users\Max\Desktop\7k_data_extracted_rotated.json";
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        //points = new Vector3[sonarData.no_counts];
        //pointsDelaunay = new IPoint[sonarData.no_counts];

        minDepth = sonarData.minimum_depth;
        maxDepth = sonarData.maximum_depth;
        minLengthAxis = sonarData.min_length_axis;
        maxLengthAxis = sonarData.max_length_axis;
        minWidthAxis = sonarData.min_width_axis;
        maxWidthAxis = sonarData.max_width_axis;
    }
    public void PointLoader()
    {
        List<float> y = new List<float>();
        List<Vector3> new_points = new List<Vector3>();

        //Test values, needs to get the values from the slider here
        minDepth = minDepth;
        maxDepth = maxDepth;
        minLengthAxis = minLengthAxis;
        maxLengthAxis = maxLengthAxis;
        minWidthAxis = minWidthAxis;
        maxWidthAxis = maxWidthAxis;

        nearestNeighbour = false;
        z_score_outlier_detection = false;

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            List<Vector3> ping = new List<Vector3>();
            List<IPoint> pingDelaunay = new List<IPoint>();
            
            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                Vector3 point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                depth_filter_passed = false;
                axis_filter_passed = false;

                if (point[1] < minDepth && point[1] > maxDepth)
                {
                    depth_filter_passed = true;
                }

                if ((point[0] > minLengthAxis && point[0] < maxLengthAxis) && (point[2] > minWidthAxis && point[2] < maxWidthAxis))
                {
                    axis_filter_passed = true;
                }

                if (depth_filter_passed && axis_filter_passed)
                {
                    // adding point to pointcloud
                    points.Add(point);
                    pointsDelaunay.Add(new Point(point[0], point[2]));


                    y.Add(point[1]);
                }

            }
            
        }
        if (z_score_outlier_detection)
        {
            z_score_threshold = 0.1;
            float delta = 0;
            float mean = 0;
            float sum = 0;
            double standardDeviation = 0;
            int n = 0;

            for (int i = 0; i < y.Count; i++)
            {
                n++;
                delta = y[i] - mean;
                mean += delta / n;
                sum += delta * (y[i] - mean);
            }

            if (n > 1)
            {
                standardDeviation = Math.Sqrt(sum - (n - 1));
            }

            for (int i = 0; i < y.Count; i++)
            {

                if (Math.Abs((y[i] - mean) / standardDeviation) < z_score_threshold)
                {
                    new_points.Add(points[i]);
                }

            }

            points = new_points;
        }
        if (nearestNeighbour)
        {
            n_neighbours = 20; //Get value from options
            neighbour_distance = 2; //Get value from options
            new_points = new List<Vector3>();
            int neighbours;
            Debug.Log(points.Count);


            for (int i = 0; i < points.Count; i++)
            {
                neighbours = 0;

                for (int j = 0; j < points.Count; j++)
                {
                    if (EuclideanDistance(points[i], points[j]) <= neighbour_distance)
                    {
                        neighbours += 1;
                    }

                }

                if (neighbours - 1 >= n_neighbours) //Subtract 1 since the point itself will be included in the list
                {
                    new_points.Add(points[i]);
                }

            }

            points = new_points;

        }
    }

    public static double EuclideanDistance(Vector3 current_point, Vector3 neighbour)
    {
        return Math.Sqrt(Math.Pow(neighbour[0] - current_point[0], 2)
            + Math.Pow(neighbour[1] - current_point[1], 2)
            + Math.Pow(neighbour[2] - current_point[2], 2));
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
        fileName = newPath;
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
        return nearestNeighbour;
    }

    public bool getOutlierHeight()
    {
        return z_score_outlier_detection;
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



}
