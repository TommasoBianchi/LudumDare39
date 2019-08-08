using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour {

	public float fadeDuration;

	private float elapsedTime;
	private Material material;
	private static GameObject self;

	void Start () {
		material = GetComponent<Renderer> ().material;	
		self = gameObject;
		self.SetActive (false);
	}

	void Update () {
		Color color = material.color;
		color.a = elapsedTime / fadeDuration;
		elapsedTime += Time.deltaTime;
		material.color = color;
	}

	public static void Init(){
		self.SetActive (true);
	}
}
