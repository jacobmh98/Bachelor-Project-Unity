using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

public class GenerateMesh : MonoBehaviour
{
    LoadData loadData = LoadData.getInstance();
    public Material material;

    private void Start()
    {
        RunnerMethod();
    }

    void RunnerMethod()
    {

        int start = 0;
        int maxPings = loadData.getPings().Count;
        int meshIndex = 0;
        
        while(start < maxPings)
        {
            Mesh mesh = new Mesh();
            GameObject gameObject = new GameObject("Mesh" + meshIndex, typeof(MeshFilter), typeof(MeshRenderer));
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            CreateShape(mesh, start - 1 < 0 ? 0 : start - 1, (start + 100) > maxPings ? maxPings : start + 100 );

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().material = material;
            start += 100;
            meshIndex++;
        }

        
    }

    void CreateShape(Mesh mesh, int start, int end)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<IPoint> delaunayPoints = new List<IPoint>();

        for(int i = start; i < end; i++)
        {
            List<Vector3> ping = loadData.getPings()[i];
            List<IPoint> pingDelaunay = loadData.getPingsDelaunay()[i];

            foreach(Vector3 p in ping)
            {
                vertices.Add(p);
            }

            foreach (IPoint p in pingDelaunay)
            {
                delaunayPoints.Add(p);
            }
        }

        var delaunay = new Delaunator(delaunayPoints.ToArray());
        int[] triangles = delaunay.Triangles;

        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}