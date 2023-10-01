using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [Serializable]
    public class logHolder
    {
        public Vector3 normal;
        public Vector3 akselerasjon;
        public Vector3 fart;
        public Vector3 posisjon;
        public float tid;
    }

public class KuleLogg : MonoBehaviour
{
    public KuleManager kule;
    public logHolder[] log;

    private int bilde;

    // Start is called before the first frame update
    void Start()
    {
        kule = this.GetComponent<KuleManager>();
        InvokeRepeating("logBall", 0.0f, 1.0f);
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void logBall()
    {
        log[bilde].normal = kule.n;
        log[bilde].akselerasjon = kule.a;
        log[bilde].fart = kule.v;
        log[bilde].posisjon = kule.p;
        log[bilde].tid = Time.time;
        bilde++;
    }
}
