using DelaunatorSharp;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData
{
    public static LoadData loadData = new LoadData();
    public Vector3[] points;
    IPoint[] pointsDelaunay;

    private LoadData()
    {
        string fileName = @"C:\Users\jacob\OneDrive - Danmarks Tekniske Universitet\6. semester\Bachelor Project\7k_data_test_file.json";
        string jsonString = File.ReadAllText(fileName);
        Sonar sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        points = new Vector3[sonarData.no_counts];
        pointsDelaunay = new IPoint[sonarData.no_counts];

        int index = 0;

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                Vector3 point = new Vector3((float)sonarData.pings[i].coords_x[j] * 100, (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);
                points[index] = point;
                pointsDelaunay[index] = new Point(point[0], point[2]);
                index++;
            }
        }
    }

    public static LoadData getInstance()
    {
        return loadData;
    }

    public Vector3[] getPoints()
    {
        return points;
    }

    public IPoint[] getPointsDelaunay()
    {
        return pointsDelaunay;
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
