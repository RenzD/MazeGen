using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{

    public float ambient = 1f;

    // Use this for initialization
    void Start()
    {
        //incr = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {

        Renderer rend = GetComponent<Renderer>();
        float ambientIntensity = rend.material.GetFloat("_AmbientLighIntensity");
        //ambientIntensity = .25f;
        //ambientIntensity = ambient;
        //ambientIntensity += incr;
        //if ((ambientIntensity > 1) || (ambientIntensity < 0))
        //    incr = -incr;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ambientIntensity == 1f)
            {
                rend.material.SetFloat("_AmbientLighIntensity", .25f);
            }
            else
            {
                rend.material.SetFloat("_AmbientLighIntensity", 1f);
            }
        }
    }
    /*
    Vector4 dld = rend.material.GetVector("_DiffuseDirection");
    dld.x += 2 * incr;
    rend.material.SetVector("_DiffuseDirection", dld);
    */
}

