using DelaunatorSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Controller
{
    public static Controller loadData = new Controller();

    // List variables for point coordinates
    private List<Vector3> points = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();
    private List<List<Vector3>> pings = new List<List<Vector3>>();
    private List<List<IPoint>> pingsDelaunay = new List<List<IPoint>>();

    // Filtering variables based on height
    int minFilteringHeight = -22;
    int maxFilteringHeight = -15;

    float minHeight;
    float maxHeight;

    private Controller()
    {
        string fileName = @"C:\Users\jacob\OneDrive - Danmarks Tekniske Universitet\6. semester\Bachelor Project\7k_data_extracted_rotated.json";
        string jsonString = File.ReadAllText(fileName);
        Sonar sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        //points = new Vector3[sonarData.no_counts];
        //pointsDelaunay = new IPoint[sonarData.no_counts];

        // setting temporary min and max height in pointcloud
        minHeight = (float)sonarData.pings[0].coords_z[0];
        maxHeight = (float)sonarData.pings[0].coords_z[0];

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            List<Vector3> ping = new List<Vector3>();
            List<IPoint> pingDelaunay = new List<IPoint>();
            
            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                Vector3 point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                if (point[1] < maxFilteringHeight && point[1] > minFilteringHeight)
                {
                    // adding point to pointcloud
                    points.Add(point);
                    pointsDelaunay.Add(new Point(point[0], point[2]));

                    // adding point to individual ping
                    ping.Add(point);
                    pingDelaunay.Add(new Point(point[0], point[2]));

                    // updating min and max height in pointcloud
                    if (point[1] < minHeight)
                        minHeight = point[1];

                    if (point[1] > maxHeight)
                        maxHeight = point[1];
                }
            }

            // adding individual pings to group pings
            pings.Add(ping);
            pingsDelaunay.Add(pingDelaunay);
        }
    }

    public static Controller getInstance()
    {
        return loadData;
    }

    public float getMinHeight()
    {
        return minHeight;
    }

    public float getMaxHeight()
    {
        return maxHeight;
    }

    public List<Vector3> getPoints()
    {
        return points;
    }

    public IPoint[] getPointsDelaunay()
    {
        return pointsDelaunay.ToArray();
    }

    public List<List<Vector3>> getPings()
    {
        return pings;
    }

    public List<List<IPoint>> getPingsDelaunay()
    {
        return pingsDelaunay;
    }
}