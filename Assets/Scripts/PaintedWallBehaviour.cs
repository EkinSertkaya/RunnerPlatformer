using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintedWallBehaviour : MonoBehaviour
{
    
    Manager gameManagerScript;

    public Mesh wallMesh;

    public Color[] vertexColors;
    Color[] meshDefaultColors;

    int[] triangles;
    int vertexIndex;
    int vertexIndex1;
    int vertexIndex2;
    int triangleMaxEdgeLimitOnVertical = 124;
    int triangleMinEdgeLimitOnHorizontal = 125;
    int triangleMaxEdgeLimitOnHorizontal = 243;

    private void Start()
    {
        ComponentGetter();
        SetPaintedWall();
    }

    private void Update()
    {
        WallPaintMehanic();
    }

    void ComponentGetter()
    {
        wallMesh = GetComponent<MeshFilter>().mesh;
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<Manager>();
        triangles = wallMesh.triangles;
        vertexColors = new Color[wallMesh.vertexCount];
        meshDefaultColors = new Color[wallMesh.vertexCount];
    }

    void SetPaintedWall()
    {
        for (int i = 0; i < wallMesh.vertexCount; ++i)
        {
            meshDefaultColors[i] = Color.white;
        }

        wallMesh.colors = meshDefaultColors;

        for (int i = 0; i < wallMesh.vertexCount; ++i)
        {
            vertexColors[i] = wallMesh.colors[i];
        }
    }

    void WallPaintMehanic()
    {
        if (gameManagerScript.raycastHitInfo.transform && gameManagerScript.raycastHitInfo.transform.tag == "Painted Wall" && Input.GetMouseButton(0))
        {
            vertexIndex = triangles[gameManagerScript.raycastHitInfo.triangleIndex * 3];

            if (gameManagerScript.raycastHitInfo.triangleIndex <= triangleMaxEdgeLimitOnVertical)
            {
                vertexIndex1 = triangles[gameManagerScript.raycastHitInfo.triangleIndex * 3 + 1];
                vertexIndex2 = triangles[gameManagerScript.raycastHitInfo.triangleIndex * 3 + 2];

                vertexColors[vertexIndex1] = Color.red;
                vertexColors[vertexIndex2] = Color.red;
            }

            if (gameManagerScript.raycastHitInfo.triangleIndex >= triangleMinEdgeLimitOnHorizontal && gameManagerScript.raycastHitInfo.triangleIndex <= triangleMaxEdgeLimitOnHorizontal)
            {
                vertexIndex1 = triangles[gameManagerScript.raycastHitInfo.triangleIndex * 3 + 1];
                vertexIndex2 = triangles[gameManagerScript.raycastHitInfo.triangleIndex * 3 + 2];

                vertexColors[vertexIndex1] = Color.red;
                vertexColors[vertexIndex2] = Color.red;
            }

            vertexColors[vertexIndex] = Color.red;

            wallMesh.colors = vertexColors;
        }
    }




}
