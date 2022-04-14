using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

[RequireComponent(typeof(MeshFilter))]

public class GenerateMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;

    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        //vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        vertices = new Vector3[] {
            new Vector3((float)0.0, (float) 0.0, (float) 0.0),
            new Vector3((float)0.0, (float) 0.0, (float) 2.0),
            new Vector3((float)2.0, (float) 0.0, (float) 0.0),
            new Vector3((float)2.0, (float) 0.0, (float) 2.0),
            new Vector3((float)1.0, (float) 0.0, (float) 1.0),
            new Vector3((float)-1.0, (float) 0.0, (float) 1.0)
        };

        IPoint[] points = new IPoint[]
        {
            new Point(0.0,0.0),
            new Point(0.0, 2.0),
            new Point(2.0, 0.0),
            new Point(2.0, 2.0),
            new Point(1.0, 1.0),
            new Point(-1.0, 1.0)
        };

        var delaunator = new Delaunator(points);
        triangles = delaunator.Triangles;
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}