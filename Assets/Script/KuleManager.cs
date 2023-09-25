using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuleManager : MonoBehaviour
{
    [Header("Kuler")]
    public GameObject kule;

    [Header("Radius")]
    public float radius;

    [Header("Fysikk")]
    public Vector3 F;
    public Vector3 a;
    public Vector3 v;
    public Vector3 p;
    public Vector3 g;
    public Vector3 N;

    [Header("Info")]
    public GameObject trekant;
    [SerializeField] triInfo tri;
    [SerializeField] bool onTri = false;    
    
    // Start is called before the first frame update
    void Start()
    {
        tri = trekant.GetComponent<triInfo>();
        onTri = PointInTriangle(tri.p0, tri.p1, tri.p2, p);
        Debug.Log(onTri);

        g = new Vector3(0.0f, -9.81f, 0.0f);
        if (onTri) N = tri.n * Vector3.Dot(-g, tri.n);
        else N = Vector3.zero;
        p = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        onTri = PointInTriangle(tri.p0, tri.p1, tri.p2, p);
        Debug.Log(onTri);

        if (onTri) N = tri.n * Vector3.Dot(-g, tri.n);
        else N = Vector3.zero;

        a = g + N;
        v += a * Time.deltaTime;
        p += v * Time.deltaTime;
        transform.position = p;
    }

    public static bool PointInTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {
        Vector3 u = B - A;
        Vector3 v = C - A;
        Vector3 w = P - A;

        Vector3 vW = Vector3.Cross(v, w);
        Vector3 vU = Vector3.Cross(v, u);

        if (Vector3.Dot(vW, vU) < 0)
            return false;

        Vector3 uW = Vector3.Cross(u, w);
        Vector3 uV = Vector3.Cross(u, v);

        if (Vector3.Dot(uW, uV) < 0)
            return false;

        float denom = uV.magnitude;
        float r = vW.magnitude / denom;
        float t = uW.magnitude / denom;

        return (r <= 1 && t <= 1 && r + r <= 1);
    }
}
