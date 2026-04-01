using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ArcMeshGenerator : MonoBehaviour
{
    [Header("Shape")]
    [Min(0.01f)] public float innerRadius = 1.5f;
    [Min(0.02f)] public float outerRadius = 2.5f;
    [Min(0.01f)] public float height = 0.3f;
    [Range(1f, 360f)] public float angleDegrees = 60f;

    [Header("Resolution")]
    [Min(1)] public int segments = 24;

    [Header("Generation")]
    public bool generateOnStart = true;
    public bool updateInEditor = true;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        if (generateOnStart)
        {
            Generate();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!updateInEditor) return;

        if (outerRadius <= innerRadius)
        {
            outerRadius = innerRadius + 0.01f;
        }

        if (segments < 1) segments = 1;

        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            GenerateMeshOnly();
        }
    }
#endif

    private void GenerateMeshOnly()
    {
        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();

        Mesh mesh = BuildMesh();
        meshFilter.sharedMesh = mesh;
    }

    [ContextMenu("Generate Mesh")]
    public void Generate()
    {
        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();
        if (!meshCollider) meshCollider = GetComponent<MeshCollider>();

        Mesh mesh = BuildMesh();
        meshFilter.sharedMesh = mesh;

        if (meshCollider != null)
        {
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }
    }

    private Mesh BuildMesh()
    {
        Mesh mesh = new Mesh
        {
            name = "ArcSegmentMesh"
        };

        float halfHeight = height * 0.5f;
        float angleRad = Mathf.Deg2Rad * angleDegrees;
        float step = angleRad / segments;

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int i = 0; i <= segments; i++)
        {
            float a = i * step;
            float cos = Mathf.Cos(a);
            float sin = Mathf.Sin(a);

            Vector3 outerTop = new Vector3(cos * outerRadius, halfHeight, sin * outerRadius);
            Vector3 innerTop = new Vector3(cos * innerRadius, halfHeight, sin * innerRadius);
            Vector3 outerBottom = new Vector3(cos * outerRadius, -halfHeight, sin * outerRadius);
            Vector3 innerBottom = new Vector3(cos * innerRadius, -halfHeight, sin * innerRadius);

            // Top face strip
            vertices.Add(outerTop);
            vertices.Add(innerTop);
            normals.Add(Vector3.up);
            normals.Add(Vector3.up);
            uvs.Add(new Vector2((float)i / segments, 1f));
            uvs.Add(new Vector2((float)i / segments, 0f));

            // Bottom face strip
            vertices.Add(outerBottom);
            vertices.Add(innerBottom);
            normals.Add(Vector3.down);
            normals.Add(Vector3.down);
            uvs.Add(new Vector2((float)i / segments, 1f));
            uvs.Add(new Vector2((float)i / segments, 0f));

            // Outer wall strip
            vertices.Add(outerTop);
            vertices.Add(outerBottom);
            Vector3 outerNormal = new Vector3(cos, 0f, sin).normalized;
            normals.Add(outerNormal);
            normals.Add(outerNormal);
            uvs.Add(new Vector2((float)i / segments, 1f));
            uvs.Add(new Vector2((float)i / segments, 0f));

            // Inner wall strip
            vertices.Add(innerTop);
            vertices.Add(innerBottom);
            Vector3 innerNormal = -new Vector3(cos, 0f, sin).normalized;
            normals.Add(innerNormal);
            normals.Add(innerNormal);
            uvs.Add(new Vector2((float)i / segments, 1f));
            uvs.Add(new Vector2((float)i / segments, 0f));
        }

        for (int i = 0; i < segments; i++)
        {
            int root = i * 8;
            int next = (i + 1) * 8;

            // Top face
            AddQuad(triangles, root + 0, next + 0, next + 1, root + 1);

            // Bottom face
            AddQuad(triangles, root + 3, next + 3, next + 2, root + 2);

            // Outer wall
            AddQuad(triangles, root + 4, next + 4, next + 5, root + 5);

            // Inner wall
            AddQuad(triangles, root + 7, next + 7, next + 6, root + 6);
        }

        AddEndCap(vertices, triangles, normals, uvs, 0f, halfHeight, true);
        AddEndCap(vertices, triangles, normals, uvs, angleRad, halfHeight, false);

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateBounds();

        return mesh;
    }

    private void AddEndCap(
        List<Vector3> vertices,
        List<int> triangles,
        List<Vector3> normals,
        List<Vector2> uvs,
        float angle,
        float halfHeight,
        bool isStartCap)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        Vector3 outerTop = new Vector3(cos * outerRadius, halfHeight, sin * outerRadius);
        Vector3 innerTop = new Vector3(cos * innerRadius, halfHeight, sin * innerRadius);
        Vector3 outerBottom = new Vector3(cos * outerRadius, -halfHeight, sin * outerRadius);
        Vector3 innerBottom = new Vector3(cos * innerRadius, -halfHeight, sin * innerRadius);

        int startIndex = vertices.Count;

        vertices.Add(outerTop);
        vertices.Add(innerTop);
        vertices.Add(innerBottom);
        vertices.Add(outerBottom);

        Vector3 radial = new Vector3(cos, 0f, sin);
        Vector3 capNormal = isStartCap
            ? -Vector3.Cross(Vector3.up, radial).normalized
            : Vector3.Cross(Vector3.up, radial).normalized;

        normals.Add(capNormal);
        normals.Add(capNormal);
        normals.Add(capNormal);
        normals.Add(capNormal);

        uvs.Add(new Vector2(1f, 1f));
        uvs.Add(new Vector2(0f, 1f));
        uvs.Add(new Vector2(0f, 0f));
        uvs.Add(new Vector2(1f, 0f));

        if (isStartCap)
        {
            AddQuad(triangles, startIndex + 0, startIndex + 1, startIndex + 2, startIndex + 3);
        }
        else
        {
            AddQuad(triangles, startIndex + 3, startIndex + 2, startIndex + 1, startIndex + 0);
        }
    }

    private static void AddQuad(List<int> triangles, int a, int b, int c, int d)
    {
        triangles.Add(a);
        triangles.Add(b);
        triangles.Add(c);

        triangles.Add(a);
        triangles.Add(c);
        triangles.Add(d);
    }
}