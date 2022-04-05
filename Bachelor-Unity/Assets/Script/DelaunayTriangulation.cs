using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaunayTriangulation
{
    List<Vertex> projectedVertices = new List<Vertex>();
    List<Vertex> vertices = new List<Vertex>();

    // adding normal and projected vertex to class
    public void addVertex(Vertex v)
    {
        vertices.Add(v);
        Vertex projectedV = new Vertex(new Vector3(v.position[0], 0, v.position[2]));
        projectedVertices.Add(projectedV);
    }
}
