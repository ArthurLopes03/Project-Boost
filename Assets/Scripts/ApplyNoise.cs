using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyNoise : MonoBehaviour
{
    public float scale = 1.0f;
    public float speed = 1.0f;
    private bool recalculateNormals = false;
    private Vector3[] baseVertices;
    private Vector3[] vertices;
    // Update is called once per frame

    private void Start()
    {
        speed = speed * Random.Range(0.8f, 1.3f);
    }
    void Update()
    {
        CalcNoise();
    }
    void CalcNoise()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if (baseVertices == null)
            baseVertices = mesh.vertices;
        vertices = new Vector3[baseVertices.Length];
        float timex = Time.time * speed + 2.5564f;
        float timey = Time.time * speed + 1.21688f;
        float timez = Time.time * speed + 0.1365143f;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            vertex.x += Mathf.PerlinNoise(timex + vertex.x, timex + vertex.y) * scale;
            vertex.y += Mathf.PerlinNoise(timey + vertex.x, timey + vertex.y) * scale;
            vertex.z += Mathf.PerlinNoise(timez + vertex.x, timez + vertex.y) * scale;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        if (recalculateNormals)
        {
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}
