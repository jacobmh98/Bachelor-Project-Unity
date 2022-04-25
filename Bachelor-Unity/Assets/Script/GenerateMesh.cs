using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DelaunatorSharp;

public class GenerateMesh : MonoBehaviour
{
    LoadData loadData = LoadData.getInstance();
    public Material material;
    Hashtable map;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    List<int> sideLengths = new List<int>();
    bool removeEdges = true;
    int medianLength = 4;
    int removedTriangles = -1;
    int removedTriIterations = 0;
    public Gradient gradient;

    private void Start()
    {
        RunnerMethod();
    }

    void RunnerMethod()
    {
        int start = 0;
        int maxPings = loadData.getPings().Count;
        int meshIndex = 0;
        int noPingsTri = 100;

        while(start < maxPings)
        {
            Mesh mesh = new Mesh();
            GameObject gameObject = new GameObject("Mesh" + meshIndex, typeof(MeshFilter), typeof(MeshRenderer));
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            CreateShape(mesh, start - 1 < 0 ? 0 : start - 1, (start + noPingsTri) > maxPings ? maxPings : start + noPingsTri);

            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            gameObject.GetComponent<MeshRenderer>().materials[0] = material;
            
            start += noPingsTri;
            meshIndex++;
            //break;
        }
    }

    void CreateShape(Mesh mesh, int start, int end)
    {
        removedTriangles = -1;
        removedTriIterations = 0;

        vertices = new List<Vector3>();
        colors = new List<Color>();
        List<IPoint> delaunayPoints = new List<IPoint>();

        for(int i = start; i < end; i++)
        {
            List<Vector3> ping = loadData.getPings()[i];
            List<IPoint> pingDelaunay = loadData.getPingsDelaunay()[i];

            foreach(Vector3 p in ping)
            {
                vertices.Add(p);
                float height = Mathf.InverseLerp(loadData.getMinHeight(), loadData.getMaxHeight(), p[1]);
                colors.Add(gradient.Evaluate(height));
            }

            foreach (IPoint p in pingDelaunay)
            {
                delaunayPoints.Add(p);
            }
        }

        // Delaunay triangulate points
        var delaunay = new Delaunator(delaunayPoints.ToArray());
        triangles = new List<int>(delaunay.Triangles);

        // Generating median length
        GenerateMedianLength();
        

        // Remove edge borders with edge length greater than median length
        while (removedTriangles != 0 || removedTriIterations < 20)
        {
            RemoveEdgeTriangles();
        }

        // Generate uvs for mesh
        /*Vector2[] uvs = new Vector2[vertices.Count];

        for (int i = 0; i < vertices.Count; i++) {
            uvs[i] = vertices[i];
        }*/

        // Code for measuring time consumption of block
        /*var watch = System.Diagnostics.Stopwatch.StartNew();
        watch.Stop();
        var elapsedTime = watch.ElapsedMilliseconds;
        print("time = " + elapsedTime);*/

        // Set the mesh variables for unity mesh
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    /**
     * Compute median length of triangle side
     */
     public void GenerateMedianLength()
    {
        for(int i = 0; i < triangles.Count; i+=3)
        {
            int length1 = (int) Norm(vertices[triangles[i]], vertices[triangles[i + 1]]);
            int length2 = (int) Norm(vertices[triangles[i + 1]], vertices[triangles[i + 2]]);
            int length3 = (int) Norm(vertices[triangles[i + 2]], vertices[triangles[i]]);

            if (length1 != 0)
                sideLengths.Add(length1);
            if(length2 != 0)
                sideLengths.Add(length2);
            if(length3 != 0)
                sideLengths.Add(length3);
        }

        sideLengths.Sort();

        

        int n = sideLengths.Count;
        print("l(s) = " + n);
        int m = n % 2 == 0 ? (sideLengths[n / 2 - 1] + sideLengths[n / 2]) / 2 : sideLengths[n - 1];
        print("m = " + m);
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
        removedTriangles = 0;

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
                    removedTriangles++;
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
        removedTriIterations++;
        print("removed " + removedTriangles + " triangles");
    }

    /**
     * Function to create new map of edges from current triangles
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