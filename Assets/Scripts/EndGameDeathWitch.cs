using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDeathWitch : MonoBehaviour {

	public ParticleSystem Explosion;
	public ParticleSystem Spawn;
	public Transform FakePlayer;
	public ParticleSystem DeathEffect;
	public Transform SoulObject;

	public float WaitSeconds = 3f;
	private float startTime;

	public float TimeToRestart = 6f;

	private bool createdFakePlayer = false;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		List<GameObject> toBeDestroyed = new List<GameObject> ();
		toBeDestroyed.AddRange (GameObject.FindGameObjectsWithTag ("Enemy"));
		GameObject player = GameObject.FindWithTag ("Player");
		Instantiate (Explosion, transform.position, Quaternion.identity);
		foreach (GameObject go in toBeDestroyed) {
			go.GetComponent<IDamageable> ().Damage (100000);
		}
		Instantiate (DeathEffect, player.transform.position, Quaternion.identity);
		int numSouls = 50;
		for (int i = 0; i < numSouls; i++) {
			Instantiate (SoulObject, player.transform.position + new Vector3 (Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0), Quaternion.identity);
		}
		Destroy (player);
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= startTime + WaitSeconds && !createdFakePlayer) {
			Instantiate (Spawn, transform.position + new Vector3(5,0,0), Quaternion.identity);
			Instantiate (FakePlayer, transform.position + new Vector3(5,0,0), Quaternion.identity);
			createdFakePlayer = true;
		} else if (Time.time >= startTime + TimeToRestart) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
