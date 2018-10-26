using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fog : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
