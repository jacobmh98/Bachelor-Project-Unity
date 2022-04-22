using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

public class GenerateMesh : MonoBehaviour
{
    LoadData loadData = LoadData.getInstance();
    public Material material;
    Hashtable map;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles;
    bool removeEdges = true;
    int medianLength = 4;

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
            gameObject.GetComponent<MeshRenderer>().materials[0] = material;

            
            start += 100;
            meshIndex++;
        }  
    }

    void CreateShape(Mesh mesh, int start, int end)
    {
        vertices = new List<Vector3>();
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

        // Delaunay triangulate points
        var delaunay = new Delaunator(delaunayPoints.ToArray());
        triangles = new List<int>(delaunay.Triangles);

        // fix error triangles by looking at boundary edges
        // TODO HERE
        /**
         * FIX MEDIAN LENGTHS
         * SET STOP KRITERIA FOR RemoveEdgeTriangles
         * USE ANGLE + LENGTH TO FILTRATE EDGES
         * LOOK INTO TRIANGULATE ONLY ONCE
         * DO DIS MAN LAZY BUGGER
         */
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();
        RemoveEdgeTriangles();



        // Set the mesh variables for unity mesh
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    /**
     * Compute euclidean distance between to vertices
    */
    public float Norm(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow(v1[0] - v2[0], 2) + Mathf.Pow(v1[1] - v2[1], 2) + Mathf.Pow(v1[2] - v2[2], 2));
    }

    /**
     * Function that removes edge triangles from triangle index set
     */
    void RemoveEdgeTriangles()
    {
        CreateMap();
        ICollection keys = map.Keys;

        // Loop through all vertices
        foreach (int v0 in keys)
        {
            // Loop through all vertices connected to v0
            foreach (int v1 in (List<int>)map[v0])
            {

                // If there is an edge from v0 to v1 check if it is also from v1 to v0
                bool borderEdge = true;
                foreach (int v2 in (List<int>)map[v1])
                {
                    if (v2 == v0)
                    {
                        borderEdge = false;
                        break;
                    }
                }

                // Removing triangle where edge v0-v1 appears and length greater than median
                if (borderEdge && Norm(vertices[v0], vertices[v1]) > medianLength)
                {
                    foreach (int v2 in (List<int>)map[v1])
                    {

                        // Find 3rd vertice from that constitutes triangle where v0-v1 appears and remove it
                        foreach (int v3 in (List<int>)map[v2])
                        {
                            if (v3 == v0)
                            {
                                for (int i = 0; i < triangles.Count; i += 3)
                                {
                                    if (triangles[i] == v0 && triangles[i + 1] == v1 && triangles[i + 2] == v2 ||
                                       triangles[i] == v2 && triangles[i + 1] == v0 && triangles[i + 2] == v1 ||
                                       triangles[i] == v1 && triangles[i + 1] == v2 && triangles[i + 2] == v0)
                                    {
                                        triangles.RemoveAt(i);
                                        triangles.RemoveAt(i);
                                        triangles.RemoveAt(i);
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }

    /**
     * Function to create new map from current triangles
     */
    void CreateMap()
    {
        map = new Hashtable();

        // Define the keys in the map from unique vertices in triangle
        foreach (int v in triangles)
        {
            if (!map.ContainsKey(v))
                map.Add(v, new List<int>());
        }

        // Add values for each key as the vertices it connects to
        for (int i = 0; i < triangles.Count; i += 3)
        {
            int v0 = triangles[i];
            int v1 = triangles[i + 1];
            int v2 = triangles[i + 2];

            if (!map.ContainsKey(v0))
                map.Add(v0, new List<int>());
            if (!map.ContainsKey(v1))
                map.Add(v1, new List<int>());
            if (!map.ContainsKey(v2))
                map.Add(v2, new List<int>());

            ((List<int>)map[v0]).Add(v1);
            ((List<int>)map[v1]).Add(v2);
            ((List<int>)map[v2]).Add(v0);
        }
    }
}