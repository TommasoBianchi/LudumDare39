using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public Transform revealer1;
    public Transform revealer2;
	public Transform revealer3;

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
			fogOfWarMaterial.SetVector("_Player2_Pos", revealer2.position);

		if (revealer3 != null)
			fogOfWarMaterial.SetVector("_Player3_Pos", revealer3.position);
    }

    public void SetRadius(float radius)
    {
        if (radius > 0)
            fogOfWarMaterial.SetFloat("_FogRadius", radius);
    }
}
