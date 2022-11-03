using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    [Range(0.01f, 0.2f)]
    public float radius = 0.1f;

    [Range(0.01f, 0.2f)]
    public float deformationStrenght = 0.1f;
    private Mesh mesh;
    private Vector3[] verticies, modifiedVerticies;
    public bool upDown = false;
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        verticies = mesh.vertices;
        modifiedVerticies = new Vector3[verticies.Length];
        for(int i = 0; i<verticies.Length; i++)
        {
            modifiedVerticies[i] = new Vector3(mesh.vertices[i].x, mesh.vertices[i].y, mesh.vertices[i].z);
        }
    }

    void RecalculateMesh()
    {
        mesh.vertices = modifiedVerticies;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int i = 0; i< modifiedVerticies.Length; i++)
            {
                Vector3 point = transform.InverseTransformPoint(hit.point);
                Vector3 distance = modifiedVerticies[i] - point;
                float smoothingFactor = 2f;
                float force = deformationStrenght / (1 + hit.point.sqrMagnitude);
                if(distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (!upDown)
                        {
                            modifiedVerticies[i] = modifiedVerticies[i] + (Vector3.up * force) / smoothingFactor;
                        }
                        else
                            modifiedVerticies[i] = modifiedVerticies[i] + (Vector3.down * force) / smoothingFactor;
                    }
                }
            }
        }
        RecalculateMesh();
    }
    public void UpDown()
    {
        upDown = !upDown;
    }
    public void ResetMesh()
    {
        modifiedVerticies = new Vector3[verticies.Length];
        for (int i = 0; i < verticies.Length; i++)
        {
            modifiedVerticies[i] = new Vector3(verticies[i].x, verticies[i].y, verticies[i].z);
        }
        RecalculateMesh();
    }
}
