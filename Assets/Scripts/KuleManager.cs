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
    public Vector3 n;

    [Header("Info")]
    public GameObject trekant;
    [SerializeField] triInfo tri;
    [SerializeField] bool onTri = false;    
    
    // Start is called before the first frame update
    void Start()
    {
        tri = trekant.GetComponent<triInfo>();
        onTri = CheckOnTriangle(tri.p0, tri.p1, tri.p2, p);
        Debug.Log(onTri);

        g = new Vector3(0.0f, -9.81f, 0.0f);
        if (onTri || FindTri(tri.t)) n = tri.n * Vector3.Dot(-g, tri.n);
        else n = Vector3.zero;
        p = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        onTri = CheckOnTriangle(tri.p0, tri.p1, tri.p2, p);
        Debug.Log(onTri);

        if (onTri || FindTri(tri.t)) n = tri.n * Vector3.Dot(-g, tri.n);    
        else n = Vector3.zero;

        a = g + n;
        v += a * Time.deltaTime;
        p += v * Time.deltaTime;        
        transform.position = p;
    }

    public static bool CheckOnTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
    {
        Vector3 u = B - A;
        Vector3 v = C - A;
        Vector3 w = P - A;
        
        Vector3 vW = Vector3.Cross(v, w);
        Vector3 vU = Vector3.Cross(v, u);

        if (Vector3.Dot(vW, vU) < 0)
        {
            //Debug.Log("vW*vU '" + Vector3.Dot(vW, vU) + "' er mindre enn 0");
            return false;
        }
        //Debug.Log("vW*vU '" + Vector3.Dot(vW, vU) + "' er større enn 0");

        Vector3 uW = Vector3.Cross(u, w);
        Vector3 uV = Vector3.Cross(u, v);

        if (Vector3.Dot(uW, uV) < 0)
        {
            //Debug.Log("uW*uV '" + Vector3.Dot(uW, uV) + "' er mindre enn 0");
            return false;
        }
        //Debug.Log("uW*uV '" + Vector3.Dot(uW, uV) + "' er større enn 0");

        float denom = uV.magnitude;
        float r = vW.magnitude / denom;
        float t = uW.magnitude / denom;

        //if (r <= 1) Debug.Log("r '" + r + "' er mindre enn eller lik 1");
        //else Debug.Log("r '" + r + "' er større enn 1");

        //if (t <= 1) Debug.Log("t '" + t + "' er mindre enn eller lik 1");
        //else Debug.Log("t '" + t + "' er større enn 1");

        //if (r + t <= 1) Debug.Log("r + t '" + (r + t) + "' er mindre enn eller lik 1");
        //else Debug.Log("r + t '" + (r + t) + "' er større enn 1");

        return (r <= 1 && t <= 1 && r + t <= 1);
    }

    bool FindTri(GameObject[] nabo)
    {
        triInfo triI;
        for (int i = 0; i < nabo.Length; i++)
        {
            if (nabo[i] == null) continue;

            triI = nabo[i].GetComponent<triInfo>();
            onTri = CheckOnTriangle(triI.p0, triI.p1, triI.p2, p);
            if (onTri)
            {
                trekant = nabo[i];
                tri = triI;
                v = Vector3.ProjectOnPlane(v, tri.n);
                Debug.Log("On triangle '" + trekant.name + "'");
                return true;
            }
        }

        Debug.Log("Not on a triangle");
        return false;
    }
}
