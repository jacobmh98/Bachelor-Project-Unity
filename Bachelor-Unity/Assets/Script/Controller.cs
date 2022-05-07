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
    DataBase db = DataBase.getInstance();

    Sonar sonarData;
    Ping pingData;

    // List variables for point coordinates
    private List<Vector3> points = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector3> boatPathPoints = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();

    string fileName;

    public bool generateHeightmap;
    public bool generateMesh;

    public Mesh mesh = null; //Skal denne variabel i databasen? Bliver kaldt i generateheightmap klassen

    public GameObject toggleGroup;

    private Controller()
    {
        /*
        if (String.IsNullOrEmpty(fileName))
        {
            //fileName = @"C:\Users\kanne\Desktop\7k_data_extracted_rotated.json";
            fileName = @"C:\Users\Max\Desktop\7k_data_extracted_rotated.json";
            //fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\bachelor_project_teledyne\7k_data_extracted_rotated.json";
        }
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        // setting temporary min and max height in pointcloud
        shallowDepth = sonarData.minimum_depth;
        deepDepth = sonarData.maximum_depth;
        minLengthAxis = sonarData.min_length_axis;
        maxLengthAxis = sonarData.max_length_axis;
        minWidthAxis = sonarData.min_width_axis;
        maxWidthAxis = sonarData.max_width_axis;

        // tmp line calls remove this remove dis later bitches
        PointLoader();
        triangulate = true;

        //showMesh = true;
        //showHeightmap = true;

        showHeightmap = true;
    }

        */
    }

    public void LoadController()
    {
        Debug.Log(fileName);
        if (String.IsNullOrEmpty(fileName))
        {
            fileName = @"C:\Users\kanne\Desktop\7k_data_extracted_rotated.json";
            //fileName = @"C:\Users\Max\Desktop\7k_data_extracted_rotated.json";
            //fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\bachelor_project_teledyne\7k_data_extracted_rotated.json";
        }
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        //setting min and max values from pointcloud in database
        db.setShallowDepth(sonarData.minimum_depth);
        db.setDeepDepth(sonarData.maximum_depth);
        db.setMinLengthAxis(sonarData.min_length_axis);
        db.setMaxLengthAxis(sonarData.max_length_axis);
        db.setMinWidthAxis(sonarData.min_width_axis);
        db.setMaxWidthAxis(sonarData.max_width_axis);

        //Changing values for sliders to be more user friendly
        db.setSliderShallowDepth(Math.Abs(db.getShallowDepth()));
        db.setSliderDeepDepth(Math.Abs(db.getDeepDepth()));
        db.setSliderMinLengthAxis(0);
        if (db.getMinLengthAxis() < 0)
        {
            db.setSliderMaxLengthAxis(db.getMaxLengthAxis() + Math.Abs(db.getMinLengthAxis()));
        } else
        {
            db.setSliderMaxLengthAxis(db.getMaxLengthAxis() - db.getMinLengthAxis());
        }

    }

    public void PointLoader()
    {
        List<float> heightOutlierDetectionList = new List<float>();
        List<double[]> kDTreeSetupList = new List<double[]>();
        List<Vector3> new_points = new List<Vector3>();
        Vector3 point;
        Vector3 boatPoint;

        //Getting values for pointloader into variables, to avoid excessive calls to the database class
        int finalShallowDepth = -db.getSliderShallowDepth();
        int finalDeepDepth = -db.getSliderDeepDepth();
        int finalMinLengthAxis = db.getSliderMinLengthAxis() + db.getMinLengthAxis();
        int finalMaxLengthAxis = db.getSliderMaxLengthAxis() + db.getMinLengthAxis();
        int finalMinWidthAxis = db.getMinWidthAxis();
        int finalMaxWidthAxis = db.getMaxWidthAxis();

        //Storing new min and max depth for correct colours in the color height map mesh
        int newShallowDepth = int.MinValue;
        int newDeepDepth = int.MaxValue;

        bool outlierHeightEnabled = db.getOutlierHeightEnabled();
        bool nearestNeighbourEnabled = db.getNearestNeighbourEnabled();

        for (int i = 0; i < sonarData.no_pings; i++)
        {

            //Not quite working
            //boatPoint = new Vector3((float)pingData.ping_boat_coord[0], (float)pingData.ping_boat_coord[2], (float)pingData.ping_boat_coord[1]);
            //boatPathPoints.Add(boatPoint);

            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                if ((point[1] < finalShallowDepth && point[1] > finalDeepDepth)
                    && (point[0] > finalMinLengthAxis && point[0] < finalMaxLengthAxis)
                    && (point[2] > finalMinWidthAxis && point[2] < finalMaxWidthAxis))
                {
                    // adding point to pointcloud
                    points.Add(point);
                    
                    if (!outlierHeightEnabled && !nearestNeighbourEnabled)
                    {
                        pointsDelaunay.Add(new DelaunatorSharp.Point(point[0], point[2]));

                        //Finding the new shallow and deep depth values for color height map
                        if (newShallowDepth < point[1])
                        {
                            newShallowDepth = (int)Math.Ceiling(point[1]);

                        }
                        else if (newDeepDepth > point[1])
                        {
                            newDeepDepth = (int)Math.Floor(point[1]);
                        }
                    }
                    // Adding point to other lists for outlier removal functions to safe running over all points multiple times
                    if (outlierHeightEnabled)
                    {
                        heightOutlierDetectionList.Add(point[1]);
                    }

                    if (nearestNeighbourEnabled)
                    {
                        double[] toKDTreePoint = new double[] { point[0], point[1], point[2] };
                        kDTreeSetupList.Add(toKDTreePoint);
                    }
                    
                }

            }

        }

        if (!outlierHeightEnabled && !nearestNeighbourEnabled)
        {
            db.setNewShallowDepth(newShallowDepth);
            db.setNewDeepDepth(newDeepDepth);
        }

        if (outlierHeightEnabled)
        {
            float sumMean = 0;
            double sumStd = 0;
            float mean = 0;
            double standardDeviation = 0;
            int n = heightOutlierDetectionList.Count;
            double outlierHeightThreshold = db.getOutlierHeightThreshold();

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
                if (Math.Abs((heightOutlierDetectionList[i] - mean) / standardDeviation) < outlierHeightThreshold)
                {
                    new_points.Add(points[i]);

                    if (!nearestNeighbourEnabled)
                    {
                        pointsDelaunay.Add(new DelaunatorSharp.Point(points[i].x, points[i].z));

                        //Finding the new shallow and deep depth values for color height map
                        if (newShallowDepth < points[i].y)
                        {
                            newShallowDepth = (int)Math.Ceiling(points[i].y);

                        }
                        else if (newDeepDepth > points[i].y)
                        {
                            newDeepDepth = (int)Math.Floor(points[i].y);
                        }

                    }

                }

            }

            //The pointloader will set the points as the new found points that satisfy the z score threshold
            points = new_points;

            if (!nearestNeighbourEnabled)
            {
                db.setNewShallowDepth(newShallowDepth);
                db.setNewDeepDepth(newDeepDepth);
            }

        }

        if (nearestNeighbourEnabled)
        {
            int numberOfNeighbours = db.getNumberOfNeighbours();
            double neighbourDistance = db.getNeighbourDistance();
            new_points = new List<Vector3>();
            double[][] kDTreeSetupArray = kDTreeSetupList.ToArray();
            KDTree<int> kDTree = KDTree.FromData<int>(kDTreeSetupArray);

            for (int i = 0; i < points.Count; i++)
            {

                double[] currPoint = new double[] { points[i].x, points[i].y, points[i].z };
                List<NodeDistance<KDTreeNode<int>>> neighbours = kDTree.Nearest(currPoint, radius: neighbourDistance);

                if (neighbours.Count > numberOfNeighbours)
                {
                    new_points.Add(points[i]);
                    pointsDelaunay.Add(new DelaunatorSharp.Point(points[i].x, points[i].z));

                    //Finding the new shallow and deep depth values for color height map
                    if (newShallowDepth < points[i].y)
                    {
                        newShallowDepth = (int)Math.Ceiling(points[i].y);

                    }
                    else if (newDeepDepth > points[i].y)
                    {
                        newDeepDepth = (int)Math.Floor(points[i].y);
                    }

                }

            }

            points = new_points;
            db.setNewShallowDepth(newShallowDepth);
            db.setNewDeepDepth(newDeepDepth);

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
        if (String.IsNullOrEmpty(newPath))
        {
            Debug.Log("newPath is null or empty!");
            Debug.Log(newPath);
            fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\bachelor_project_teledyne\7k_data_extracted_rotated.json";

        }
        else
        {
            fileName = newPath;
        }
    }
    public List<int> getTriangles()
    {
        return triangles;
    }

}
