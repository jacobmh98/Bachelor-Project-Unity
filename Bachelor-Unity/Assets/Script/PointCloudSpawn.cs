using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class ObjData
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;

    public Matrix4x4 matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);
        }
    }

    public ObjData(Vector3 pos, Vector3 scale, Quaternion rot)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
    }
}

public class PointCloudSpawn : MonoBehaviour
{
    public Vector3 maxPos;
    public Mesh objMesh;
    public Material objMat;
    public Vector3[] positions;
    public DelaunayTriangulation delaunayTriangulation = new DelaunayTriangulation();
    private List<List<ObjData>> batches = new List<List<ObjData>>();

    void Start()
    {
        string fileName = @"C:\Users\jacob\OneDrive\Dokumenter\GitHub\Bachelor-Project-Unity\Bachelor-Unity\Assets\Script\7k_data_test_file.json";
        string jsonString = File.ReadAllText(fileName);
        Sonar sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        List<Vector3[]> pings = new List<Vector3[]>();

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            Vector3[] points = new Vector3[sonarData.pings[i].no_points];

            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                Vector3 coord = new Vector3((float)sonarData.pings[i].coords_x[j] * 100, (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);
                delaunayTriangulation.addVertex(new Vertex(coord));
                points[j] = coord;
            }
            pings.Add(points);
        }

        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();
        for (int i = 0; i < sonarData.no_counts; i++)
        {
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= 1000)
            {
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
    }

    void Update()
    {
        RenderBatches();
    }

    private void AddObj(List<ObjData> currBatch, int i)
    {
        Vector3 position = delaunayTriangulation.getProjectedVertexPos(i);
        currBatch.Add(new ObjData(position, new Vector3(1f, 1f, 1f), Quaternion.identity));
    }

    private List<ObjData> BuildNewBatch()
    {
        return new List<ObjData>();
    }

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
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
}
