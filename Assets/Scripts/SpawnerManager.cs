using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {
	
	private Dictionary<string, Enemy> EnemiesDictionary = new Dictionary<string, Enemy>();
	public List<Enemy> Enemies;

	public void FillDict() {
		foreach (Enemy e in Enemies) {
			Debug.Log (e.name);
			EnemiesDictionary.Add (e.name, e);
			foreach (string key in EnemiesDictionary.Keys) {
				Debug.Log ("Key: " + key + ", Value: " + EnemiesDictionary [key]);
			}
		}
	}

	public Enemy getEnemyFromName(string enemyName) {
		Debug.Log ("Retrieving " + enemyName);
		return EnemiesDictionary [enemyName];
	}
}
