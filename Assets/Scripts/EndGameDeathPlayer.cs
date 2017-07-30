using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDeathPlayer : MonoBehaviour {

	public ParticleSystem Spawn;
	public Transform FakePlayer;

	public float WaitSeconds = 3f;
	private float startTime;

	public float TimeToRestart = 6f;

	private bool createdFakePlayer = false;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startTime + WaitSeconds && !createdFakePlayer) {
			Instantiate (Spawn, transform.position, Quaternion.identity);
			Instantiate (FakePlayer, transform.position, Quaternion.identity);
			createdFakePlayer = true;
		} else if (Time.time >= startTime + TimeToRestart) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
