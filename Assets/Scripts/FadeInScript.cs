using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour {

	private AudioSource audioClip;

	[SerializeField]
	private float maxAudioVolume;


	void Start () {

		audioClip = gameObject.GetComponent<AudioSource> ();
		audioClip.volume = 0;
		
	}

	void Update () {
		if (audioClip.volume < maxAudioVolume) {
			audioClip.volume += 0.05f * Time.deltaTime;
		}
	}
		
}
