using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;

[RequireComponent(typeof(MeshFilter))]

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
    public DelaunayTriangulation delaunayTriangulation;
    private List<List<ObjData>> batches = new List<List<ObjData>>();
    private Delaunator delaunator;
    private GameObject meshObject;
    private bool renderPointCloud = false;
    [SerializeField] Material meshMaterial;
    Sonar sonarData;
    Mesh mesh;
    Vector3[] vertices;

    int[] triangles;

    void Start()
    {
        string fileName = @"C:\Users\jacob\OneDrive - Danmarks Tekniske Universitet\6. semester\Bachelor Project\7k_data_test_file.json";
        string jsonString = File.ReadAllText(fileName);
        sonarData = JsonConvert.DeserializeObject<Sonar>(jsonString);

        delaunayTriangulation = new DelaunayTriangulation(sonarData.no_counts);
        print(sonarData.no_counts);

        List<Vector3[]> pings = new List<Vector3[]>();

        int index = 0;

        for (int i = 0; i < sonarData.no_pings; i++)
        {
            Vector3[] points = new Vector3[sonarData.pings[i].no_points];

            for (int j = 0; j < sonarData.pings[i].no_points; j++)
            {
                Vector3 coord = new Vector3((float)sonarData.pings[i].coords_x[j] * 100, (float)sonarData.pings[i].coords_z[j], (float)sonarData.pings[i].coords_y[j]);
                delaunayTriangulation.addVertex(new Vertex(coord), index);
                points[j] = coord;
                index++;
            }
            pings.Add(points);
        }

        //createPointCloud();

        // perform delaunay triangulation
       // var delaunay = delaunayTriangulation.delaunayTriangulate();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();


        //CreateMesh();
        /*for (var e = 0; e < delaunay.GetTriangles().Length(); e++)
        {
            if (e > delaunay.halfedges[e])
            {
                const p = points[delaunay.triangles[e]];
                const q = points[delaunay.triangles[nextHalfedge(e)]];
                callback(e, p, q);
            }
        }*/
    }

    void CreateShape()
    {
        //vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        vertices = delaunayTriangulation.getVertices();
        triangles = delaunayTriangulation.delaunayTriangulate();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void createPointCloud()
    {
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

    private void CreateMesh()
    {
        if (meshObject != null)
        {
            Destroy(meshObject);
        }

        var mesh = new Mesh
        {
            vertices = delaunator.Points.ToVectors3(),
            triangles = delaunator.Triangles
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshObject = new GameObject("DelaunatorMesh");
        var meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = meshMaterial ?? new Material(Shader.Find("Standard"));
        var meshFilter = meshObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        if (renderPointCloud)
        {
            RenderBatches();
        }
    }

    private void AddObj(List<ObjData> currBatch, int i)
    {
        Vector3 position = delaunayTriangulation.getVertexPos(i);
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
