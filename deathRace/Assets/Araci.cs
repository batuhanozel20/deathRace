using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Araci : MonoBehaviour
{
    public CarDamage CD0;

    public CarDamage1 CD1;

    public CarDamage2 CD2;

    public CarDamage3 CD3;

    public float x0;
    public float x1;
    public float x2;
    public float x3;


    // Start is called before the first frame update
    void Start()
    {
        x0 = 0.0f;
        x1 = 0.0f;
        x2 = 0.0f;
        x3 = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        x0 -= 1.0f*Time.deltaTime;
        x1 -= 1.0f*Time.deltaTime;
        x2 -= 1.0f*Time.deltaTime;
        x3 -= 1.0f*Time.deltaTime;
        CD0.secondsForTakeDamage = x0;
    }
}
