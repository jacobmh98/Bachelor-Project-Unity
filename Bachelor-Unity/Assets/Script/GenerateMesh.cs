using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

public class GenerateMesh : MonoBehaviour
{
    LoadData loadData = LoadData.getInstance();
    List<Vector3> vertices;
    int[] triangles;

    private void Start()
    {
        RunnerMethod();
    }

    void RunnerMethod()
    {
        Mesh mesh = new Mesh();
        GameObject gameObject = new GameObject("MeshTest", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        CreateShape();
        UpdateMesh(mesh);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    void CreateShape()
    {
        vertices = loadData.getPoints();
        var delaunay = new Delaunator(loadData.getPointsDelaunay());
        triangles = delaunay.Triangles;
    }

    void UpdateMesh(Mesh mesh)
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}