using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TriangleManager : MonoBehaviour
{
    [Header("Trekanter")]
    public GameObject[] t;

    [Header("Punkt")]
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    public Vector3 p5;

    // Start is called before the first frame update
    void Start()
    {
        triSetup(t[0], p0, p2, p1, t[1], null, null);
        triSetup(t[1], p1, p2, p3, t[0], t[2], null);
        triSetup(t[2], p2, p5, p3, t[1], t[3], null);
        triSetup(t[3], p2, p4, p5, t[2], null, null);
    }

    void triSetup(GameObject tri, Vector3 p_0, Vector3 p_1, Vector3 p_2, GameObject tri_0, GameObject tri_1, GameObject tri_2)
    {
        Mesh m = new Mesh();
        MeshFilter mf = tri.GetComponent<MeshFilter>();
        mf.mesh = m;

        triInfo _tri = tri.GetComponent<triInfo>();
        _tri.p0 = p_0;
        _tri.p1 = p_1;
        _tri.p2 = p_2;

        _tri.t[0] = tri_0;
        _tri.t[1] = tri_1;
        _tri.t[2] = tri_2;

        Vector3[] vArray = new Vector3[3];
        int[] triArray = new int[3];

        vArray[0] = p_0;
        vArray[1] = p_1;
        vArray[2] = p_2;

        triArray[0] = 0;
        triArray[1] = 1;
        triArray[2] = 2;

        m.vertices = vArray;
        m.triangles = triArray;

        setNormal(tri);
    }

    void setNormal(GameObject tri)
    {
        triInfo _tri = tri.GetComponent<triInfo>();

        Vector3 v1 = _tri.p1 - _tri.p0;
        Vector3 v2 = _tri.p2 - _tri.p0;

        _tri.n.x = v1.y * v2.z - v1.z * v2.y;
        _tri.n.y = v1.z * v2.x - v1.x * v2.z;
        _tri.n.z = v1.x * v2.y - v1.y * v2.x;

        _tri.n /= Mathf.Sqrt(Mathf.Pow(_tri.n.x, 2) + Mathf.Pow(_tri.n.y, 2) + Mathf.Pow(_tri.n.z, 2));
    }
}
