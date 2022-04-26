using DelaunatorSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData
{
    public static LoadData loadData = new LoadData();
    private List<Vector3> points = new List<Vector3>();
    private List<IPoint> pointsDelaunay = new List<IPoint>();

    private List<List<Vector3>> pings = new List<List<Vector3>>();
    private List<List<IPoint>> pingsDelaunay = new List<List<IPoint>>();

    // Filtering variables based on height
    int minHeight = -22;
    int maxHeight = -15;

    private LoadData()
    {
        string fileName = @"C:\Users\Max\Desktop\DTU_Softwareteknologi\6.Semester2021\Bachelorprojekt\bachelor_project_teledyne\7k_data_extracted.json";
        string jsonString = File.ReadAllText(fileName);
        Sonar sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        //points = new Vector3[sonarData.no_counts];
        //pointsDelaunay = new IPoint[sonarData.no_counts];

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            List<Vector3> ping = new List<Vector3>();
            List<IPoint> pingDelaunay = new List<IPoint>();
            
            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                // getting coordinates for single point
                Vector3 point = new Vector3((float)sonarData.pings[i].coords_x[j], (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);

                if (point[1] < maxHeight && point[1] > minHeight)
                {
                    // adding point to pointcloud
                    points.Add(point);
                    pointsDelaunay.Add(new Point(point[0], point[2]));

                    // adding point to individual ping
                    ping.Add(point);
                    pingDelaunay.Add(new Point(point[0], point[2]));
                }

                
            }

            // adding individual pings to group pings
            pings.Add(ping);
            pingsDelaunay.Add(pingDelaunay);
        }
    }

    public static LoadData getInstance()
    {
        return loadData;
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

public class Ping
{
    public int pingID { get; set; }
    public int no_points { get; set; }
    public List<double> ping_coord { get; set; }
    public List<double> coords_x { get; set; }
    public List<double> coords_y { get; set; }
    public List<double> coords_z { get; set; }
}

public class Sonar
{
    public int no_pings { get; set; }
    public int no_counts { get; set; }
    public List<Ping> pings { get; set; }
}
