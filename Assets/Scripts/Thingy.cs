using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thingy : MonoBehaviour
{
    public float range;
    // Set up width and height variables
    // These are required to define our vertices
    public float meshWidth = 10f;
    public float meshHeight = 10f;
    public float meshDepth = 10f;
    // Use this for initialisation
    void Start()
    {
        // Create mesh filter using GetComponent<meshfilter>
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;
        // Vertices
        Vector3[] vertices = new Vector3[26] {
        new Vector3(0,0,0),                               new Vector3(meshWidth/2,0,0),                              new Vector3(meshWidth,0,0),
        new Vector3(0,meshHeight/2,0),                    new Vector3(meshWidth/2,meshHeight/2,0),                   new Vector3(meshWidth,meshHeight/2,0),
        new Vector3(0,meshHeight,0),                      new Vector3(meshWidth/2,meshHeight,0),                     new Vector3(meshWidth,meshHeight,0),

        new Vector3(0,0,meshDepth/2),                     new Vector3(meshWidth/2,0,meshDepth/2),                    new Vector3(meshWidth,0,meshDepth/2),
        new Vector3(0,meshHeight/2,meshDepth/2),                                                                     new Vector3(meshWidth,meshHeight/2,meshDepth/2),
        new Vector3(0,meshHeight,meshDepth/2),            new Vector3(meshWidth/2,meshHeight,meshDepth/2),           new Vector3(meshWidth,meshHeight,meshDepth/2),

        new Vector3(0,0,meshDepth),                       new Vector3(meshWidth/2,0,meshDepth),                      new Vector3(meshWidth,0,meshDepth),
        new Vector3(0,meshHeight/2,meshDepth),            new Vector3(meshWidth/2,meshHeight/2,meshDepth),           new Vector3(meshWidth,meshHeight/2,meshDepth),
        new Vector3(0,meshHeight,meshDepth),              new Vector3(meshWidth/2,meshHeight,meshDepth),             new Vector3(meshWidth,meshHeight,meshDepth),
        };

        // Triangles
        int[] triangles =
        {
            0,3,4,
            0,4,1,
            1,4,5,
            1,5,2,
            3,6,7,
            3,7,4,
            4,7,8,
            4,8,5,

            2,5,13,
            2,13,11,
            11,13,22,
            11,22,19,
            5,8,16,
            5,16,13,
            13,16,25,
            13,25,22,

            17,20,12,
            17,12,9,
            9,12,3,
            9,3,0,
            20,23,14,
            20,14,12,
            12,14,6,
            12,6,3,

            6,14,15,
            6,15,7,
            7,15,16,
            7,16,8,
            14,23,24,
            14,24,15,
            15,24,25,
            15,25,16,

            19,22,21,
            19,21,18,
            18,21,20,
            18,20,17,
            22,25,24,
            22,24,21,
            21,24,23,
            21,23,20,

            17,9,10,
            17,10,18,
            18,10,11,
            18,11,19,
            9,0,1,
            9,1,10,
            10,1,2,
            10,2,11,
        };

        //Randomises the appearence of the rock
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.x += Mathf.PerlinNoise(vertex.x, vertex.y) * Random.Range(range, -range);
            vertex.y += Mathf.PerlinNoise(vertex.x, vertex.y) * Random.Range(range, -range);
            vertex.z += Mathf.PerlinNoise(vertex.x, vertex.y) * Random.Range(range, -range);
            vertices[i] = vertex;
        }


        // Update mesh with vertices, triangles and normals
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        //Adds rigidbody and meshcollider 
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        mc.convex = true;
    }
}