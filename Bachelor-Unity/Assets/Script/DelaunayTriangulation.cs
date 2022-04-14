
using DelaunatorSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaunayTriangulation
{
    List<Vertex> projectedVertices = new List<Vertex>();
    IPoint[] points;
    Vector3[] verticesPos;
    List<Vertex> vertices = new List<Vertex>();

    public DelaunayTriangulation(int no_points)
    {
        points = new IPoint[no_points];
        verticesPos = new Vector3[no_points];
    }

    // adding normal and projected vertex to class
    public void addVertex(Vertex v, int i)
    {
        vertices.Add(v);
        points[i] = new Point(v.position[0], v.position[2]);
        verticesPos[i] = new Vector3(v.position[0], v.position[1], v.position[2]);
    }

    public Vector3[] getVertices()
    {
        return verticesPos;
    }

    // getter method for vertex by index
    public Vector3 getVertexPos(int i)
    {
        return vertices[i].position;
    }

    // getter method for vertex by index
    public Vector3 getProjectedVertexPos(int i)
    {
        return projectedVertices[i].position;
    }

    // perform delaunay triangulation
    public int[] delaunayTriangulate()
    {
        var delaunay = new Delaunator(points);

        return delaunay.Triangles ;
    }
} 
