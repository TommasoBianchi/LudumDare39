using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour {

	public AudioClip zombie1;
	public AudioClip zombie2;
	public AudioClip zombie3;
	public AudioClip zombie4;
	public AudioClip zombie5;

	private List<AudioClip> clips;

	private float timeLastSound = 0f;
	private float timeBetweenSounds = 5f;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		clips = new List<AudioClip> ();
		clips.Add (zombie1);
		clips.Add (zombie2);
		clips.Add (zombie3);
		clips.Add (zombie4);
		clips.Add (zombie5);

		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timeLastSound + timeBetweenSounds * Random.Range (0.5f, 1.5f)) {
			timeLastSound = Time.time;
			AudioClip clip = clips [Random.Range (0, clips.Count)];
			audioSource.clip = clip;
			audioSource.Play ();
		}
	}
}
