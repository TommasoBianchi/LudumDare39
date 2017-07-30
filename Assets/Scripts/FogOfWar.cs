using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public Transform revealer1;
    public Transform revealer2;

    public Shader fogOfWarShader;

    private Material fogOfWarMaterial;

    void Start()
    {
        fogOfWarMaterial = GetComponent<Renderer>().sharedMaterial;
        fogOfWarMaterial.shader = fogOfWarShader;
    }

    void Update()
    {
        if (revealer1 != null)
            fogOfWarMaterial.SetVector("_Player1_Pos", revealer1.position);

        if (revealer2 != null)
            fogOfWarMaterial.SetVector("_Player1_Pos", revealer2.position);
    }

    public void SetRadius(float radius)
    {
        if (radius > 0)
            fogOfWarMaterial.SetFloat("_FogRadius", radius);
    }
}
